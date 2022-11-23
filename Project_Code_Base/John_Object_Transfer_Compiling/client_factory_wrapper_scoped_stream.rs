use core::ffi;

use interoptopus::{callback, ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::slice::{FFISlice};
use interoptopus::patterns::option::FFIOption;


// Holds two int values. Used in place of tuples or two data points
#[ffi_type]
#[repr(C)]
pub struct U16Tuple{
    value1: u16,
    value2: u16,
}

// Custom string used for transferring between C# and Rust
#[ffi_type]
#[repr(C)]
pub struct CustomRustString {
    pub capacity: u32,
    pub string_slice: U8Slice,
}
#[ffi_type]
#[repr(C)]
pub struct CustomCSharpString{
    pub capacity: u32,
    pub string_slice: U16Slice,
}
// Stores a pointer to a slice of data and its length
#[ffi_type]
#[repr(C)]
pub struct U8Slice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}
#[ffi_type]
#[repr(C)]
pub struct U16Slice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}



// Scope in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopeWrapper{
    pub name: CustomCSharpString,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs 
    as:
    #[derive(From, Shrinkwrap, Debug, Display, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Scope {
        pub name: String,
    }
*/



// Stream in palatable C#
#[ffi_type]
#[repr(C)]
pub struct StreamWrapper {
    pub name: CustomCSharpString,
}
/*
    Originally from 
    as: 
    #[derive(From, Shrinkwrap, Debug, Display, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Stream {
        pub name: String,
    }
*/



// ScopedStream in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopedStreamWrapper{
    pub scope: ScopeWrapper,
    pub stream: StreamWrapper,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs 
    as: 
    #[derive(new, Debug, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct ScopedStream {
        pub scope: Scope,
        pub stream: Stream,
    }   
*/



// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(U8Slice))
            .register(extra_type!(U16Slice))
            .register(extra_type!(CustomCSharpString))
            .register(extra_type!(CustomRustString))
            .register(extra_type!(ScopeWrapper))
            .register(extra_type!(StreamWrapper))
            .register(extra_type!(ScopedStreamWrapper))
        .inventory()
    }
}