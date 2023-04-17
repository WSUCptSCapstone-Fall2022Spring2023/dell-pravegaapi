#![allow(
    non_snake_case,
    unused_imports
)]
//// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the Index area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{Inventory, InventoryBuilder};
use pravega_client::client_factory::ClientFactoryAsync;
use pravega_client::{byte::ByteReader,byte::ByteWriter};
use pravega_client_config::connection_type;
use pravega_client_shared::{ScopedStream, Scope, Stream};
use pravega_client::{client_factory::ClientFactory};
use pravega_client::event::ReaderGroup;
use pravega_client::event::EventWriter;
use futures::executor;
use utility_wrapper::{CustomRustString, U8Slice};

use tokio::task::JoinHandle;
use std::ops::Deref;
use std::{thread, time};
use std::io::{Error, ErrorKind, SeekFrom};
use std::time::Duration;
use once_cell::sync::OnceCell;
use tokio::runtime::{Builder, Runtime, EnterGuard};
use tokio::task;

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}

// ReaderGroup default constructor


#[no_mangle]
pub extern "C" fn CreateReaderGroup(
    client_factory_ptr: &'static ClientFactory,
    scope: CustomRustString,
    stream: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, *const ReaderGroup)){


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
        println!("above block on");
        // Create the new bytewriter asynchronously with the inputted runtime and the
        client_factory_ptr.runtime().block_on( async move {
            println!("in block on");
            let result: ReaderGroup = client_factory_ptr.create_reader_group("test".to_string(),ss).await;
            println!("below create RG");
            let result_box: Box<ReaderGroup> = Box::new(result);
            let result_ptr: *const ReaderGroup = Box::into_raw(result_box);
            println!("From Rust");
            println!("{:?}",result_ptr);
            unsafe { callback(key,result_ptr) };
        }) ;
        
}

#[no_mangle]
pub extern "C" fn CreateEventWriter(
    client_factory_ptr: &'static ClientFactory,
    scope: CustomRustString,
    stream: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, *const EventWriter)){


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
        println!("above block on");
        // Create the new bytewriter asynchronously with the inputted runtime and the
        client_factory_ptr.runtime().block_on( async move {
            println!("in block on");
            let result: EventWriter = client_factory_ptr.create_event_writer(ss);
            println!("below create EW");
            let result_box: Box<EventWriter> = Box::new(result);
            let result_ptr: *const EventWriter = Box::into_raw(result_box);
            println!("From Rust");
            println!("{:?}",result_ptr);
            unsafe { callback(key,result_ptr) };
        }) ;
        
}


#[no_mangle]
pub extern "C" fn EventWriterDrop(EW: &mut EventWriter)
{
    drop(EW);
}

#[no_mangle]
pub extern "C" fn EventWriterFlush(client_factory_ptr: &'static ClientFactory,
event_writer_ptr: &mut EventWriter,
key: u64,
callback: unsafe extern "C" fn (u64, u64)
)
{
// Block on the client factory's runtime
client_factory_ptr.runtime().block_on( async move {

    // Write data to server
    event_writer_ptr.flush().await.unwrap();
    unsafe { callback(key, 1); }
});
}

#[no_mangle]
pub extern "C" fn ByteWriterWrite(
    client_factory_ptr: &'static ClientFactory,
    event_writer_ptr: &mut EventWriter,
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
        let mut result: tokio::sync::oneshot::Receiver<Result<(), pravega_client::error::Error>> = event_writer_ptr.write_event(buffer_array.to_vec()).await;
        let reviever_value: () = result.try_recv().unwrap().unwrap();
        let mut return_value =-1;
        if reviever_value==()
        {
            return_value =0;
        }

        unsafe { callback(key, return_value as u64); }
    });
}