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
use tokio;
use futures::executor;
use utility::CustomRustString;

use std::ffi::c_void;
use std::time::Duration;

use tokio::runtime::{Builder, Runtime};
use tokio::task;


#[no_mangle]
pub extern "C" fn CreateByteWriter(
    client_factory_async_ptr: &mut ClientFactoryAsync,
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

        // Set the execution of this function into the runtime pointer inputted
        let _h = client_factory_async_ptr.runtime_handle().enter();
        let spawn_factory: ClientFactoryAsync = unsafe { std::ptr::read(client_factory_async_ptr) };

        // Create the new bytewriter asynchronously with the inputted runtime and the 
        tokio::spawn( async move {
            let result: ByteWriter = spawn_factory.create_byte_writer(ss).await;
            let result_box: Box<ByteWriter> = Box::new(result);
            let result_ptr: *const ByteWriter = Box::into_raw(result_box);
            unsafe { callback(result_ptr) };
        });
    
}

#[no_mangle]
pub extern "C" fn CreateByteWriterHelper(source_client: &mut ClientFactory) -> *const ByteWriter{
    println!("test");

    // Create default ScopedSegment
    let default_Scoped_Stream: ScopedStream = ScopedStream::from("temp_A/temp_B");
   
    // Create new bytereader
    let new_byte_writer  = source_client.create_byte_writer(default_Scoped_Stream);

    // Box and return client factory
    let byte_writer_box = Box::new(new_byte_writer);
    let box_pointer: *const ByteWriter = Box::into_raw(byte_writer_box) as *const ByteWriter;
    return box_pointer;
}

#[no_mangle]
pub extern "C" fn ByteWriterCurrentOffset(source_byte_writer: &mut ByteWriter) -> u64
{
    return source_byte_writer.current_offset();
}
