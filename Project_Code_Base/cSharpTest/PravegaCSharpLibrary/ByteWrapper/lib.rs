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
use pravega_client::{byte::ByteReader,byte::ByteWriter};
use pravega_client_shared::{ScopedStream};
use pravega_client::{client_factory::ClientFactory};
use tokio;


#[no_mangle]
pub async extern "C" fn CreateByteReaderHelper(source_client: &mut ClientFactory) -> *const ByteReader{
    println!("Creating ScopedStream");
    // Create default ScopedSegment
    let default_Scoped_Stream: ScopedStream = ScopedStream::from("temp_A/temp_B");
   
    // Create new bytereader
    println!("CreatingByteWriter");
    let new_byte_reader = source_client.create_byte_reader(default_Scoped_Stream).await;

    // Box and return client factory
    println!("Boxing and returning");
    let byte_reader_box: Box<ByteReader> = Box::new(new_byte_reader);
    let box_pointer: *const ByteReader = Box::into_raw(byte_reader_box);
    return box_pointer;
}

#[no_mangle]
pub async extern "C" fn CreateByteWriterHelper(source_client: &mut ClientFactory) -> *const ByteWriter{

    // Create default ScopedSegment
    let default_Scoped_Stream: ScopedStream = ScopedStream::from("temp_A/temp_B");
   
    // Create new bytereader
    let new_byte_writer  = source_client.create_byte_writer(default_Scoped_Stream).await;

    // Box and return client factory
    let byte_writer_box: Box<ByteWriter> = Box::new(new_byte_writer);
    let box_pointer: *const ByteWriter = Box::into_raw(byte_writer_box);
    return box_pointer;
}
