#![allow(
    non_snake_case,
    unused_imports
)]
///
/// Contains methods for running functions from C# in the Client Factory Module.
/// 

// Relevant imports
use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder, ffi_function, function, lang::rust};
use pravega_client::{client_factory::ClientFactory};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use utility_wrapper::{CustomRustStringSlice, CustomRustString};


// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}