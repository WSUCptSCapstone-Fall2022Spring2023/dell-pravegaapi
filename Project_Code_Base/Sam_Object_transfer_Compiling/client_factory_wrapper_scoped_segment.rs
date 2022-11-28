use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;


// Holds two int values. Used in place of tuples or two data points
#[ffi_type]
#[repr(C)]
pub struct U16Tuple{
    value1: u16,
    value2: u16,
}


// Imported from previous wrapper. See WriterID
// U128 wrapper for sending between C# and Rust
// NOTE: u128 normally is comprised of 1 value, but u128 is not C palatable and as such can't be transferred
//  between C# and Rust. The solution here is to split the two halves of the u128 into two u64 values that
//  are C palatable. 
//  -When sent from one side to another, a u128 value is split into the two halves and bitwise ORed into the
//      two halves of this struct.
//  -When recieved from another wise, the first and second halves are ORed at different points on a u128 value
//      initialized at 0. first_half -> bits 0-63 and second_half -> bits 64-127. This builds the u128 back up
//      from its parts.
//  There isn't an easy way to transfer a value this big between the two sides, but doing so is O(1) each time.
//      For now, this is the fastest way to go between the two without risking using slices.
#[ffi_type]
#[repr(C)]
pub struct CustomU128{
    pub first_half: u64,
    pub second_half: u64,
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
// Custom string slice
#[ffi_type]
#[repr(C)]
pub struct CustomCSharpStringSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}
#[ffi_type]
#[repr(C)]
pub struct CustomRustStringSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}



// Imported from previous wrapping. See ScopedStream
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



// Segment in palatable C#
#[ffi_type]
#[repr(C)]
pub struct SegmentWrapper{
    pub number: i64,
    pub tx_id: FFIOption<TxIdWrapper>,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs
    as: 
    #[derive(Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Segment {
        pub number: i64,
        pub tx_id: Option<TxId>,
    }
*/
#[ffi_type]
#[repr(C)]
pub struct TxIdWrapper{
    pub inner: CustomU128,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs
    as: 
    #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct TxId(pub u128);
*/



// Imported from previous wrapping. See ScopedStream
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



// ScopedSegment in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopedSegmentWrapper{
    pub scope: ScopeWrapper,
    pub stream: StreamWrapper,
    pub segment: SegmentWrapper,
}
/*
    Originally from
    as: 
    #[derive(new, Debug, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct ScopedSegment {
        pub scope: Scope,
        pub stream: Stream,
        pub segment: Segment,
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
            .register(extra_type!(CustomCSharpStringSlice))
            .register(extra_type!(ScopedSegmentWrapper))
        .inventory()
    }
}