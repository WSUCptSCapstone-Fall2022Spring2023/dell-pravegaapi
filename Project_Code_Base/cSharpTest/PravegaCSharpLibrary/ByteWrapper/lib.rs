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
    callback: unsafe extern "C" fn(*const ByteReader)){


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
            unsafe { callback(result_ptr) };
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
pub extern "C" fn ByteReaderSeek(client_factory_ptr: &'static ClientFactory,
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
}

// ByteWriter default constructor
#[no_mangle]
pub extern "C" fn CreateByteWriter(
    client_factory_ptr: &'static ClientFactory,
    scope: CustomRustString,
    stream: CustomRustString,
    callback: unsafe extern "C" fn(*const ByteWriter)){


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
            unsafe { callback(result_ptr) };
        }) ;
        
}


// ByteWriter.current_offset
#[no_mangle]
pub extern "C" fn ByteWriterCurrentOffset(source_byte_writer: &mut ByteWriter) -> u64
{
    return source_byte_writer.current_offset();
}

