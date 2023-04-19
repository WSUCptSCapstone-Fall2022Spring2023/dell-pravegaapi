#![allow(
    non_snake_case,
    unused_imports
)]
///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the ClientFactory area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{Inventory, InventoryBuilder};
use pravega_client::{client_factory::{ClientFactory, ClientFactoryAsync}, byte::ByteReader, byte::ByteWriter};
use std::{time::Instant, string};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use pravega_controller_client::{ControllerClient, ControllerClientImpl, mock_controller::MockController};
use utility_wrapper::{CustomRustStringSlice, CustomRustString};
use tokio::runtime::{Runtime, Handle};
use once_cell::sync::OnceCell;
use debugless_unwrap::*;
use pravega_client_config::{connection_type};
use pravega_client_shared::{ScopedStream, Scope, Stream, StreamConfiguration, Scaling, ScaleType, Retention, RetentionType};
use std::io::{Error, ErrorKind, SeekFrom};

static INSTANCE: OnceCell<ClientFactory> = OnceCell::new();
const TESTING_AMOUNT: i32 = 10;

//////////////////////////
// Client Factory Methods
//////////////////////////

// Default Constructor for Client Factory
//  -Creates client factory with default config, generated runtime.
#[no_mangle]
extern "C" fn CreateClientFactory() -> &'static ClientFactory{
    
    // Set and return client factory
    if INSTANCE.get().is_none() == true{
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");

        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

        //Set INSTANCE to being the newly initialized client factory
        INSTANCE.set(new_client_factory).unwrap();
    }
    INSTANCE.get().unwrap()
}

#[no_mangle]
extern "C" fn CreateClientFactoryTime() -> u64
{
    // Start timer
    let mut total_time: u64 = 0;

    for _i in 0..TESTING_AMOUNT {

        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_client_factory: ClientFactory = ClientFactory::new(default_client_config);
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);

}

// Constructor for Client Factory
//  -Creates client factory with inputted config, generated runtime
//  *Consumes ClientConfig
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfig(source_config: *const ClientConfig) ->  &'static ClientFactory{

    unsafe{

        // Set and return client factory if unintialized
        if INSTANCE.get().is_none() == true{
            // Get config from raw pointer
            let source_config_pointer: ClientConfig = std::ptr::read(source_config);

            // Create new client factory
            let new_client_factory: ClientFactory = ClientFactory::new(source_config_pointer);

            // Set INSTANCE to initialized client factory.
            INSTANCE.set(new_client_factory).unwrap();
        }
        INSTANCE.get().unwrap()
    }
}

#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigTime() -> u64
{

    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_client_factory: ClientFactory = ClientFactory::new(default_client_config);
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}

// Constructor for Client Factory
//  -Creates client factory with inputted config, generated runtime
//  *Consumes ClientConfig
//  *Consumes Runtime
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigAndRuntime(source_config_pointer: *const ClientConfig, source_runtime_pointer: *const Runtime) ->  &'static ClientFactory{

    unsafe{
        
        if INSTANCE.get().is_none() == true{
            // Get config from raw pointer
            let source_config: ClientConfig = std::ptr::read(source_config_pointer);

            // Get runtime from raw poitner
            let source_runtime: Runtime = std::ptr::read(source_runtime_pointer);

            // Create new client factory
            let new_client_factory: ClientFactory = ClientFactory::new_with_runtime(source_config, source_runtime);
            
            // Set client factory after initializing.
            INSTANCE.set(new_client_factory).unwrap();
        }
        INSTANCE.get().unwrap()
    }
}
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigAndRuntimeTime() -> u64
{
    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Create test runtime
        let rt: Runtime = tokio::runtime::Runtime::new().expect("create runtime");

        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_client_factory: ClientFactory = ClientFactory::new_with_runtime(default_client_config, rt);
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}

// Other Methods for ClientFactory

// ClientFactory.runtime
#[no_mangle]
extern "C" fn GetClientFactoryRuntime(source_client_factory: &mut ClientFactory) -> *const Runtime{

    // Retrieve runtime from client factory
    let factory_runtime: &Runtime = source_client_factory.runtime();

    // Return runtime pointer as raw pointer
    return factory_runtime as *const Runtime;
}
#[no_mangle]
extern "C" fn GetClientFactoryRuntimeTime() -> u64
{ 
    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_runtime: &Runtime = new_client_factory.runtime();
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}

// ClientFactory.runtime_handle
#[no_mangle]
extern "C" fn GetClientFactoryRuntimeHandle(source_client_factory: &mut ClientFactory) -> *const Handle{

    // Retrieve handle from client factory
    let factory_runtime_handle: Handle = source_client_factory.runtime_handle();

    // Box and return handle pointer
    let factory_runtime_handle_box: Box<Handle> = Box::new(factory_runtime_handle);
    let box_pointer: *const Handle = Box::into_raw(factory_runtime_handle_box);     
    return box_pointer;
}
// Runs the associated function and returns the time taken to complete in rust in milliseconds.
#[no_mangle]
extern "C" fn GetClientFactoryRuntimeHandleTime() -> u64
{
    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_handle: Handle = new_client_factory.runtime_handle();
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}

// ClientFactory.config
#[no_mangle]
extern "C" fn GetClientFactoryConfig(source_client_factory: &'static ClientFactory) -> *const ClientConfig{

    // Retrieve handle from client factory
    let factory_config: &ClientConfig = source_client_factory.config();

    // Return client config pointer as raw pointer
    return factory_config as *const ClientConfig;
}
// Runs the associated function and returns the time taken to complete in rust in milliseconds.
#[no_mangle]
extern "C" fn GetClientFactoryConfigTime() -> u64
{
    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_async_factory: &ClientConfig = new_client_factory.config();
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}

// ClientFactory.controller_client
#[no_mangle]
extern "C" fn GetClientFactoryControllerClient(source_client_factory: &'static ClientFactory) -> *const &dyn ControllerClient{

    // Retrieve pointer and box
    let factory_controller_client: &dyn ControllerClient = source_client_factory.controller_client();
    let controller_box: Box<&dyn ControllerClient> = Box::new(factory_controller_client);
    let return_pointer: *const &dyn ControllerClient = Box::into_raw(controller_box);
    return return_pointer;
}

// ClientFactory.to_async
#[no_mangle]
extern "C" fn ClientFactoryToAsync(source_client_factory: &'static ClientFactory) -> *const ClientFactoryAsync{

    // Retrieve handle from client factory
    let factory_client_async_clone: ClientFactoryAsync = source_client_factory.to_async();

    // Box and return clientfactoryasync pointer
    let factory_client_factory_async_clone_box: Box<ClientFactoryAsync> = Box::new(factory_client_async_clone);
    let box_pointer: *const ClientFactoryAsync = Box::into_raw(factory_client_factory_async_clone_box);     
    return box_pointer;
}

// Runs the associated function and returns the time taken to complete in rust in milliseconds.
#[no_mangle]
extern "C" fn ClientFactoryToAsyncTime() -> u64
{
    let mut total_time: u64 = 0;
    //Repeat test multiple times to get an average
    for _i in 0..TESTING_AMOUNT {
        // Create default ClientConfig
        let default_client_config: ClientConfig = ClientConfigBuilder::default()
            .controller_uri("localhost:9090")
            .build()
            .expect("create config");
    
        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

        // Start timer
        let timer = Instant::now();
        // Run function
        let _new_async_factory: ClientFactoryAsync = new_client_factory.to_async();
        // End timer
        let time_elapsed = timer.elapsed();
        total_time += time_elapsed.subsec_nanos() as u64
    }
    //Calculate average time taken in nano seconds
    return total_time / (TESTING_AMOUNT as u64);
}


//////////////////////////
/// A special note for this next section:
/// During development, certain functions were found to work in this dll specifically though not part of this dll.
/// This is because of the use of OnceCell and we theorize that because C# allocated memory when a dll is called and 
/// keeps it in memory until the program ends, there could be a dependancy for methods to be in the same memory space 
/// as the dll. 
/// 
/// For example, the ByteReader constructor doesn't work inside of the Byte folder's lib.rs, but works perfectly fine here.
/// 
/// Therefore, for future development, a possible cause of code not working could be stemming from this dependancy. This 
/// could also stem from an internal working of the pravega source code causing unforseen issues with C#'s compatability.
//////////////////////////
 
 
//////////////////////////
// Special Byte  Methods
//////////////////////////
 
/// Byte Writer Methods
// Create Byte Writer
#[no_mangle]
pub extern "C" fn CreateByteWriter(
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

        // Get the reference to the main client factory from instance.
        let factory_instance = INSTANCE.get().unwrap();

        // Create the new bytewriter asynchronously with the inputted runtime and the
        factory_instance.runtime().block_on( async move {
            let result: ByteWriter = factory_instance.create_byte_writer(ss).await;
            let result_box: Box<ByteWriter> = Box::new(result);
            let result_ptr: *const ByteWriter = Box::into_raw(result_box);
            unsafe { callback(key, result_ptr) };
        }) ;
        
}

// ByteWriter.seal
#[no_mangle]
pub extern "C" fn ByteWriterSeal(
    byte_writer_ptr: &mut ByteWriter,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64)
)
{
    let client_factory_ptr = INSTANCE.get().unwrap();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        byte_writer_ptr.seal().await.unwrap();
        unsafe { callback(key, 1); }
    });
}

// ByteWriter.seek_to_tail
#[no_mangle]
pub extern "C" fn ByteWriterSeekToTail(
    byte_writer_ptr: &mut ByteWriter,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64)
)
{
    let client_factory_ptr = INSTANCE.get().unwrap();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        byte_writer_ptr.seek_to_tail().await;
        unsafe { callback(key, 1); }
    });
}

/// Byte Reader Methods
// Create Byte Reader
#[no_mangle]
pub extern "C" fn CreateByteReader(
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

        // Get the reference to the main client factory from instance.
        let factory_instance = INSTANCE.get().unwrap();

        // Create the new bytewriter asynchronously with the inputted runtime and the
        factory_instance.runtime().block_on( async move {
            let result: ByteReader = factory_instance.create_byte_reader(ss).await;
            let result_box: Box<ByteReader> = Box::new(result);
            let result_ptr: *const ByteReader = Box::into_raw(result_box);
            unsafe { callback(key, result_ptr) };
        }) ;
        
}

// ByteReader.current_tail
#[no_mangle]
pub extern "C" fn ByteReaderCurrentTail(
    source_byte_reader: &mut ByteReader,
    key: u64,  
    callback: unsafe extern "C" fn(u64, u64)
) -> ()
{
    let client_factory_ptr: &ClientFactory = INSTANCE.get().unwrap();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        let result = source_byte_reader.current_tail().await.unwrap();
        unsafe { callback(key, result); }
    });
}

// ByteReader.seek
#[no_mangle]
pub extern "C" fn ByteReaderSeek(
    source_byte_reader: &mut ByteReader,
    mode: u64,
    number_of_bytes: i64,
    key: u64, 
    callback: unsafe extern "C" fn(u64, u64))
{

    // Initialize locals
    let reader_seek_from: SeekFrom;
    if mode == 0{
        reader_seek_from = SeekFrom::Start(number_of_bytes as u64);
    }
    else if mode == 1{
        reader_seek_from = SeekFrom::Current(number_of_bytes);
    }
    else if mode == 2{
        reader_seek_from = SeekFrom::End(number_of_bytes);
    }
    else{
        panic!("Invalid seek mode inputted")
    }

    let client_factory_ptr: &ClientFactory = INSTANCE.get().unwrap();

    // Seek from the reader seek from position
    client_factory_ptr.runtime().block_on( async move {
        let result: u64 = source_byte_reader.seek(reader_seek_from).await.unwrap();
        unsafe { callback(key, result) };
    }) ;
}

// ByteReader.current_head
#[no_mangle]
pub extern "C" fn ByteReaderCurrentHead(
    source_byte_reader: &mut ByteReader, 
    key: u64,
    callback: unsafe extern "C" fn(u64, u64)
) -> ()
{
    let client_factory_ptr: &ClientFactory = INSTANCE.get().unwrap();

    // Block on the client factory's runtime
    client_factory_ptr.runtime().block_on( async move {

        // Write data to server
        let result = source_byte_reader.current_head().await.unwrap();
        unsafe { callback(key, result); }
    });
}

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}