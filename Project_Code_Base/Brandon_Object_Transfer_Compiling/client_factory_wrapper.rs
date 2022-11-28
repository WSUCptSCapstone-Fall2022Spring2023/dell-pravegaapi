use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;

use pravega_client_channel::{create_channel, ChannelSender};
//use pravega_client_shared::{ScopedStream, WriterId};
use std::collections::VecDeque;
use tokio::sync::oneshot;
use tokio::sync::oneshot::error::TryRecvError;
use std::net::Incoming;

//Custom U128 because C doesn't support 128 bit unsigned integers
#[ffi_type]
#[repr(C)]
pub struct CustomU128{
    pub first_half: u64,
    pub second_half: u64,
}

//Creates a stuct similar to the original Writer but with CustomU128
#[ffi_type]
#[repr(C)]
pub struct WriterId(pub CustomU128);

#[ffi_type]
#[repr(C)]
pub struct EventWriter<'a> {
    pub writer_id: WriterId,
    pub sender: ChannelSender<Incoming<'a>>,
    pub event_handles: VecDeque<oneshot::Receiver<Result<(), Error>>>,
}

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            // .register(extra_type!(U8Slice))
            // .register(extra_type!(U16Slice))
            // .register(extra_type!(CustomCSharpString))
            // .register(extra_type!(CustomRustString))
            // .register(extra_type!(CustomCSharpStringSlice))
            // .register(extra_type!(CustomRustStringSlice))
            // .register(extra_type!(CredWrapper))
            // .register(extra_type!(CredentialsWrapper))
            // .register(extra_type!(RetryWithBackoffWrapper))
            // .register(extra_type!(PravegaNodeUriWrapper))
            .register(extra_type!(EventWriter))
        .inventory()
    }
}