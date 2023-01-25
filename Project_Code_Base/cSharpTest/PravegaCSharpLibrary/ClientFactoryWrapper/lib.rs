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
use pravega_client::{client_factory::ClientFactory};

use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use utility::{CustomRustStringSlice, CustomRustString};

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

// Constructor for Client Factory
//  -Creates client factory with inputted config, generated runtime
//  *Consumes ClientConfig
#[no_mangle]
extern "C" fn CreateClientFactoryFromConfig(source_client_config: *const ClientConfig) -> *const ClientFactory{

    unsafe{
        // Debug
        println!("test into config constructor");

        // Get config from raw pointer
        let source_config: ClientConfig = std::ptr::read(source_client_config);

        // Create new client factory
        let new_client_factory: ClientFactory = ClientFactory::new(source_config);

        // Box and return client factory
        let client_factory_box: Box<ClientFactory> = Box::new(new_client_factory);
        let box_pointer: *const ClientFactory = Box::into_raw(client_factory_box);

        // Debug
        println!("test out of config constructor");
        
        return box_pointer;
    }
}

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}