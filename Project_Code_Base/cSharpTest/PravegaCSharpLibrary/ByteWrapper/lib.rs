#![allow(
    non_snake_case,
    unused_imports
)]
///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the ByteWriter area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
/// 
use interoptopus::{Inventory, InventoryBuilder};
use pravega_client::client_factory::ClientFactoryAsync;
use pravega_client::{byte::ByteReader,byte::ByteWriter};
use pravega_client_config::connection_type;
use pravega_client_shared::{ScopedStream, Scope, Stream};
use pravega_client::{client_factory::ClientFactory};
use futures::executor;
use utility_wrapper::CustomRustString;
use utility_wrapper::U8Slice;
use tokio::task::JoinHandle;
use std::{thread, time};
use std::io::{Error, ErrorKind, SeekFrom};
use std::time::Duration;
use once_cell::sync::OnceCell;
use tokio::runtime::{Builder, Runtime, EnterGuard};
use tokio::task;

// ByteReader default constructor
#[no_mangle]
pub extern "C" fn CreateByteReader(
    client_factory_ptr: &'static ClientFactory,
    scope: CustomRustString,
    stream: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, *const ByteReader))
    {


        // Construct scopedstream and clientfactoryasync from function inputs
        let scope_converted = Scope{
            name: scope.as_string()
        };
        let stream_converted = Stream{
            name: stream.as_string()
        };
        let ss: ScopedStream = ScopedStream{
            scope: scope_converted,
            stream: stream_converted  
        };

        // Create the new bytewriter asynchronously with the inputted runtime and the
        client_factory_ptr.runtime().block_on( async move {
            let result: ByteReader = client_factory_ptr.create_byte_reader(ss).await;
            let result_box: Box<ByteReader> = Box::new(result);
            let result_ptr: *const ByteReader = Box::into_raw(result_box);
            unsafe { callback(key, result_ptr) };
        }) ;
        
}

// ByteReader.current_offset
#[no_mangle]
pub extern "C" fn ByteReaderCurrentOffset(source_byte_reader: &mut ByteReader) -> u64
{
    return source_byte_reader.current_offset();
}

// ByteReader.available
#[no_mangle]
pub extern "C" fn ByteReaderAvailable(source_byte_reader: &mut ByteReader) -> u64
{
    return source_byte_reader.available() as u64;
}

// ByteReader.seek
#[no_mangle]
pub extern "C" fn ByteReaderSeek(
    client_factory_ptr: &'static ClientFactory,
    source_byte_reader: &mut ByteReader,
    mode: u64,
    number_of_bytes: u64,
    callback: unsafe extern "C" fn(u64))
{

    // Initialize locals
    let reader_seek_from: SeekFrom;
    if mode == 0{
        reader_seek_from = SeekFrom::Start(number_of_bytes);
    }
    else if mode == 1{
        reader_seek_from = SeekFrom::Current(number_of_bytes as i64);
    }
    else if mode == 2{
        reader_seek_from = SeekFrom::End(number_of_bytes as i64);
    }
    else{
        panic!("Invalid seek mode inputted")
    }

    // Seek from the reader seek from position
    client_factory_ptr.runtime().block_on( async move {
        let result: u64 = source_byte_reader.seek(reader_seek_from).await.unwrap();
        unsafe { callback(result) };
    }) ;
}

// ByteReader.read
#[no_mangle]
pub extern "C" fn ByteReaderRead(
    client_factory_ptr: &'static ClientFactory,
    source_byte_reader: &mut ByteReader, 
    bytes_requested: u32,
    callback: unsafe extern "C" fn(*mut &[u8], u32)
) -> ()
{
    // Block on and read
    client_factory_ptr.runtime_handle().block_on(async move{

        // Create a buffer in Rust to use as storage for the read. Create at capacity
        let mut buffer: Vec<u8> = vec![0; bytes_requested as usize];

        // Read into buffer.
        let result: usize = source_byte_reader.read(&mut buffer).await.unwrap();

        // Box and then return.
        let buffer_box: Box<&[u8]> = Box::new(buffer.as_slice());
        let buffer_box_raw: *mut &[u8] = Box::into_raw(buffer_box);
        unsafe { callback(buffer_box_raw, result as u32); }
    })
}


// ByteWriter default constructor
#[no_mangle]
pub extern "C" fn CreateByteWriter(
    client_factory_ptr: &'static ClientFactory,
    scope: CustomRustString,
    stream: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, *const ByteWriter)){


        // Construct scopedstream and clientfactoryasync from function inputs
        let scope_converted = Scope{
            name: scope.as_string()
        };
        let stream_converted = Stream{
            name: stream.as_string()
        };
        let ss: ScopedStream = ScopedStream{
            scope: scope_converted,
            stream: stream_converted  
        };

        // Create the new bytewriter asynchronously with the inputted runtime and the
        client_factory_ptr.runtime().block_on( async move {
            let result: ByteWriter = client_factory_ptr.create_byte_writer(ss).await;
            let result_box: Box<ByteWriter> = Box::new(result);
            let result_ptr: *const ByteWriter = Box::into_raw(result_box);
            unsafe { callback(key, result_ptr) };
        }) ;
        
}


// ByteWriter.current_offset
#[no_mangle]
pub extern "C" fn ByteWriterCurrentOffset(source_byte_writer: &mut ByteWriter) -> u64
{
    return source_byte_writer.current_offset();
}

// ByteWriter.write
#[no_mangle]
pub extern "C" fn ByteWriterWrite(
    client_factory_ptr: &'static ClientFactory,
    byte_writer_ptr: &mut ByteWriter,
    buffer: *mut u8,
    buffer_size: u32,
    callback: unsafe extern "C" fn(u64)
)
{
    // Initialize the buffer from the inputs
    let buffer_slice: U8Slice = U8Slice { slice_pointer: buffer as *mut i32, length: buffer_size };
    let buffer_array: &mut [u8] = buffer_slice.as_rust_u8_slice_mut();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        let result: usize = byte_writer_ptr.write(buffer_array).await.unwrap();
        unsafe { callback(result as u64); }
    });
}