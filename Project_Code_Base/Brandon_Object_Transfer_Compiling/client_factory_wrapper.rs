use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;

//use pravega_client_channel::{create_channel, ChannelSender};
//use pravega_client_shared::{ScopedStream, WriterId};
// use std::collections::VecDeque;
// use tokio::sync::oneshot;
// use tokio::sync::oneshot::error::TryRecvError;
// use std::net::Incoming;

pub mod synchronizer;
pub mod table;

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(table::U8Slice))
            .register(extra_type!(table::U16Slice))
            .register(extra_type!(table::CustomCSharpString))
            .register(extra_type!(table::CustomRustString))
            .register(extra_type!(table::CustomCSharpStringSlice))
            .register(extra_type!(table::CustomRustStringSlice))
            .register(extra_type!(table::DelegationTokenProviderWrapper))
            .register(extra_type!(table::PravegaNodeUriWrapper))
            .register(extra_type!(table::TableWrapper))
            .register(extra_type!(synchronizer::Synchronizer))
        .inventory()
    }
}