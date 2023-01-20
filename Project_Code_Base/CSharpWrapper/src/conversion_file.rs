use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder, ffi_function, function, lang::rust};
use pravega_client::{client_factory::ClientFactory};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use config_wrapper::{ClientConfigWrapper};
use utility::{CustomRustStringSlice, CustomRustString};

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .register(function!(CreateClientFactoryTest))
        .register(function!(TestGetRuntime))   
        .inventory()
    }
}