///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains helper structs that can transfer varying sizes of values. Slices, tuple, rust values, strings, and more
///     are located in here. Provides definitions on the Rust side.
///     
use interoptopus::{ffi_type};



/////////////////////////////////////////
/// Value Structs
/////////////////////////////////////////
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



/////////////////////////////////////////
/// Slice Structs
/////////////////////////////////////////
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
#[ffi_type]
#[repr(C)]
pub struct U128U64TupleSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}



/////////////////////////////////////////
/// Tuple Structs
/////////////////////////////////////////
// Holds two int values. Used in place of tuples or two data points
#[ffi_type]
#[repr(C)]
pub struct U16Tuple{
    value1: u16,
    value2: u16,
}



// Holds two values, 1 u64 and 1 u128 representing a tuple
#[ffi_type]
#[repr(C)]
pub struct U128U64Tuple{
    pub value1: CustomU128,
    pub value2: u64,
}



/////////////////////////////////////////
/// String Structs
/////////////////////////////////////////
// Custom string used for transferring
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