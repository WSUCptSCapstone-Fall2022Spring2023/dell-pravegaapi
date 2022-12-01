use interoptopus::patterns::slice::{FFISliceMut, FFISlice};
use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;
use interoptopus::patterns::string::AsciiPointer;
use std::ffi::CString;

//use pravega_client_channel::{create_channel, ChannelSender};
//use pravega_client_shared::{ScopedStream, WriterId};
// use std::collections::VecDeque;
// //use tokio::sync::oneshot;
// //use tokio::sync::oneshot::error::TryRecvError;
// use std::net::Incoming;

// //Custom U128 because C doesn't support 128 bit unsigned integers
// #[ffi_type]
// #[repr(C)]
// pub struct CustomU128{
//     pub first_half: u64,
//     pub second_half: u64,
// }

#[ffi_type]
#[repr(C)]
pub struct UseAsciiStringPattern<'a> {
    pub ascii_string: AsciiPointer<'a>,
}

use crate::table;


#[ffi_type(opaque)]
#[repr(C)]
pub struct KeyValue {
    pub key: i32,
    pub value: CString,
}

pub struct HashMapString<'a> {
    pub string: CString,
    pub hash: FFISliceMut<'a, KeyValue>,
}

#[ffi_type(opaque)]
#[repr(C)]
pub struct Synchronizer<'a> {
    //If CString doesn't work use UseAsciiStringPattern<'a>,
    pub name: CString,

    pub table_map: table::TableWrapper,

    pub in_memory_map: FFISliceMut<'a, KeyValue>,//HashMap<String, HashMap<Key, Value>>,

    pub in_memory_map_version: HashMapString<'a>,//UseAsciiStringPattern<'a>,//HashMap<Key, Value>,

    pub table_segment_offset: i64,

    pubfetch_position: i64,
}
// pub fn my_inventory() -> Inventory {
//     {
//         InventoryBuilder::new()
//             // .register(extra_type!(U8Slice))
//             // .register(extra_type!(U16Slice))
//             // .register(extra_type!(CustomCSharpString))
//             // .register(extra_type!(CustomRustString))
//             // .register(extra_type!(CustomCSharpStringSlice))
//             // .register(extra_type!(CustomRustStringSlice))
//             // .register(extra_type!(CredWrapper))
//             // .register(extra_type!(CredentialsWrapper))
//             // .register(extra_type!(RetryWithBackoffWrapper))
//             // .register(extra_type!(PravegaNodeUriWrapper))
//             .register(extra_type!(Synchronizer))
//         .inventory()
//     }
// }