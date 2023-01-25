#![allow(
    non_snake_case,
    unused_imports
)]
///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the PravegaConfig area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
pub mod connection_type_wrapper;
pub mod credentials_wrapper;

use interoptopus::{Inventory, InventoryBuilder};
use utility::{CustomRustString};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};

// Default Constructor for Client Config 
//  -Creates client config with default settings
#[no_mangle]
extern "C" fn CreateClientConfig() -> *const ClientConfig{

    // Create default ClientConfig
    let default_client_config: ClientConfig = ClientConfigBuilder::default()
        .controller_uri("localhost:8000")
        .build()
        .expect("create config");

    // Box and return
    let client_config_box: Box<ClientConfig> = Box::new(default_client_config);
    let box_pointer: *const ClientConfig = Box::into_raw(client_config_box);
    return box_pointer;
}

// Getters and Setters for ClientConfig

// ClientConfig.max_connections_in_pool
#[no_mangle]
extern "C" fn GetClientConfigMaxConnectionsInPool(source_config: &mut ClientConfig) -> u32{

    // Get Max Connections and return
    return source_config.max_connections_in_pool;
}
#[no_mangle]
extern "C" fn SetClientConfigMaxConnectionsInPool(source_config: &mut ClientConfig, new_value: u32) -> (){

    // Set Max Connections
    source_config.max_connections_in_pool = new_value;

    // Return
    return;
}

// ClientConfig.max_controller_connections
#[no_mangle]
extern "C" fn GetClientConfigMaxControllerConnections(source_config: &mut ClientConfig) -> u32{

    // Get Max Connections and return
    return source_config.max_controller_connections;
}
#[no_mangle]
extern "C" fn SetClientConfigMaxControllerConnections(source_config: &mut ClientConfig, new_value: u32) -> (){

    // Set Max Connections
    source_config.max_controller_connections = new_value;

    // Return
    return;
}

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}


