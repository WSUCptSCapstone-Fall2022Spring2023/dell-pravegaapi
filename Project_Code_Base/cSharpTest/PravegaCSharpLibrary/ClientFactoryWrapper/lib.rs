///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the client factory module as well as making other files in this folder useable in the cargo package.
///     Provides definitions on the Rust side.
///

use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder, ffi_function, function, lang::rust};
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


// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}