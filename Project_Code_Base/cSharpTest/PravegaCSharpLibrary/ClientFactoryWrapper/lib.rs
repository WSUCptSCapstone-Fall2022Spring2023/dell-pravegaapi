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
use pravega_client::{client_factory::{ClientFactory, ClientFactoryAsync}};
use std::time::Instant;
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use pravega_controller_client::{ControllerClient};
use utility::{CustomRustStringSlice, CustomRustString};
use tokio::runtime::{Runtime, Handle};

//////////////////////////
// Client Factory Methods
//////////////////////////

// Default Constructor for Client Factory
//  -Creates client factory with default config, generated runtime.
#[no_mangle]
extern "C" fn CreateClientFactory() -> *const ClientFactory{

    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
        .build()
        .expect("create config");
   
    // Create new client factory
    let new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

    // Box and return client factory
    let client_factory_box: Box<ClientFactory> = Box::new(new_client_factory);
    let box_pointer: *const ClientFactory = Box::into_raw(client_factory_box);
    return box_pointer;
}
#[no_mangle]
extern "C" fn CreateClientFactoryTime() -> u64
{
    // Start timer
    let timer = Instant::now();

    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
        .build()
        .expect("create config");
    // Run function
    let _new_client_factory: ClientFactory = ClientFactory::new(default_client_config);

    // End timer
    let time_elapsed = timer.elapsed();

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// Constructor for Client Factory
//  -Creates client factory with inputted config, generated runtime
//  *Consumes ClientConfig
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfig(source_config: *const ClientConfig) -> *const ClientFactory{

    unsafe{
        // Get config from raw pointer
        let source_config_pointer: ClientConfig = std::ptr::read(source_config);

        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(source_config_pointer);

        // Box and return client factory
        let client_factory_box: Box<ClientFactory> = Box::new(new_client_factory);
        let box_pointer: *const ClientFactory = Box::into_raw(client_factory_box);     
        return box_pointer;
    }
}
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigTime() -> u64
{
    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
        .build()
        .expect("create config");
   
    // Start timer
    let timer = Instant::now();
    // Run function
    let _new_client_factory: ClientFactory = ClientFactory::new(default_client_config);
    // End timer
    let time_elapsed = timer.elapsed();

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// Constructor for Client Factory
//  -Creates client factory with inputted config, generated runtime
//  *Consumes ClientConfig
//  *Consumes Runtime
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigAndRuntime(source_config_pointer: *const ClientConfig, source_runtime_pointer: *const Runtime) -> *const ClientFactory{

    unsafe{
        // Get config from raw pointer
        let source_config: ClientConfig = std::ptr::read(source_config_pointer);

        // Get runtime from raw poitner
        let source_runtime: Runtime = std::ptr::read(source_runtime_pointer);

        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new_with_runtime(source_config, source_runtime);

        // Box and return client factory
        let client_factory_box: Box<ClientFactory> = Box::new(new_client_factory);
        let box_pointer: *const ClientFactory = Box::into_raw(client_factory_box);     
        return box_pointer;
    }
}
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfigAndRuntimeTime() -> u64
{
    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
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

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// Getters and Setters for ClientFactory

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
    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
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

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
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
    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
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

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// ClientFactory.config
#[no_mangle]
extern "C" fn GetClientFactoryConfig(source_client_factory: &mut ClientFactory) -> *const ClientConfig{

    // Retrieve handle from client factory
    let factory_config: &ClientConfig = source_client_factory.config();

    // Return client config pointer as raw pointer
    return factory_config as *const ClientConfig;
}
// Runs the associated function and returns the time taken to complete in rust in milliseconds.
#[no_mangle]
extern "C" fn GetClientFactoryConfigTime() -> u64
{
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

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// ClientFactory.controller_client
// *Cannot transfer traits back as raw pointers. (Probably not useful in C# anyways)
/*
#[no_mangle]
extern "C" fn GetClientFactoryControllerClient(source_client_factory: &mut ClientFactory) -> *const ControllerClient{

    // Retrieve handle from client factory
    let factory_controller_client: &dyn ControllerClient = source_client_factory.controller_client();

    // Return client config pointer as raw pointer
    return factory_controller_client as *const ControllerClient;
}
*/

// ClientFactory.to_async
#[no_mangle]
extern "C" fn ClientFactoryToAsync(source_client_factory: &mut ClientFactory) -> *const ClientFactoryAsync{

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
    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
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

    // Return seconds passed
    return time_elapsed.as_millis() as u64;
}

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}