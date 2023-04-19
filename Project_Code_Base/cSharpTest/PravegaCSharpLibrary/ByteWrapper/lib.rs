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
use pravega_client_config::{connection_type, ClientConfig, ClientConfigBuilder};
use pravega_client_shared::{ScopedStream, Scope, Stream, StreamConfiguration, Scaling, ScaleType, Retention, RetentionType};
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
use tracing_subscriber;
use tracing;

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

// ByteReader.read
#[no_mangle]
pub extern "C" fn ByteReaderRead(
    client_factory_ptr: &'static ClientFactory,
    source_byte_reader: &mut ByteReader, 
    bytes_requested: u32,
    key: u64,
    callback: unsafe extern "C" fn(u64, *mut i32, u32)
) -> ()
{
    // Block on and read
    client_factory_ptr.runtime_handle().block_on(async move{

        // Create a buffer in Rust to use as storage for the read. Create at capacity
        let mut buffer: Vec<u8> = vec![0; bytes_requested as usize];

        // Read into buffer.
        let result: usize = source_byte_reader.read(&mut buffer).await.unwrap();
        
        // Box and then return.
        let buffer_slice = U8Slice::from_rust_u8_slice_mut(buffer.as_mut_slice(), &result);
        let buffer_slice_array_pointer = buffer_slice.slice_pointer;
        let _buffer_box: Box<U8Slice> = Box::new(buffer_slice);
        unsafe { callback(key, buffer_slice_array_pointer, result as u32); }
    })
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
    key: u64, 
    callback: unsafe extern "C" fn(u64, u64)
)
{
    // Initialize the buffer from the inputs
    let buffer_slice: U8Slice = U8Slice { slice_pointer: buffer as *mut i32, length: buffer_size };
    let buffer_array: &mut [u8] = buffer_slice.as_rust_u8_slice_mut();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        let result: usize = byte_writer_ptr.write(buffer_array).await.unwrap();
        unsafe { callback(key, result as u64); }
    });
}

// ByteWriter.flush
#[no_mangle]
pub extern "C" fn ByteWriterFlush(
    client_factory_ptr: &'static ClientFactory,
    byte_writer_ptr: &mut ByteWriter,
    key: u64,
    callback: unsafe extern "C" fn (u64, u64)
)
{
    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        byte_writer_ptr.flush().await.unwrap();
        unsafe { callback(key, 1); }
    });
}

// ByteWriter.truncate_data_before
#[no_mangle]
pub extern "C" fn ByteWriterTruncateDataBefore(
    client_factory_ptr: &'static ClientFactory,
    byte_writer_ptr: &mut ByteWriter,
    offset: i64,
    key: u64,
    callback: unsafe extern "C" fn (u64, u64)
)
{
    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        byte_writer_ptr.truncate_data_before(offset).await.unwrap();
        unsafe { callback(key, 1); }
    });
}

// ByteWriter.reset
#[no_mangle]
pub extern "C" fn ByteWriterReset(
    client_factory_ptr: &'static ClientFactory,
    byte_writer_ptr: &mut ByteWriter,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64)
)
{
    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        byte_writer_ptr.reset().await.unwrap();
        unsafe { callback(key, 1); }
    });
}