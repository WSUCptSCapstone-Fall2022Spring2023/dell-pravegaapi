///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains helper structs that can transfer varying sizes of values. Slices, tuple, rust values, strings, and more
///     are located in here. Provides definitions on the Rust side.
///     
use interoptopus::{ffi_type, patterns::string};
use std::{slice, char};


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
    pub length: u32,
}
impl U8Slice{

    // Constructs a U8 slice of length "newLength"
    pub fn new(new_length: &u32) -> U8Slice {
        let mut slice_vec: Vec<u8> = Vec::with_capacity(*new_length as usize);
        U8Slice {
            length: *new_length,
            slice_pointer: slice_vec.as_mut_slice().as_mut_ptr() as *mut i32,
        }
    }

    // Convert to standard rust u8 slice
    pub fn as_rust_u8_slice_mut(&self) -> &mut [u8] {
        unsafe{
            let self_slice: &mut [u8] = slice::from_raw_parts_mut(
                self.slice_pointer as *mut u8,
                self.length as usize
            );
            return self_slice;
        }           
    }

    // Takes a rust u8 slice and converts it into this custom slice
    pub fn from_rust_u8_slice_mut(
        source: &mut [u8],
        new_length: &usize)
    -> U8Slice{
        U8Slice{
            slice_pointer: source.as_mut_ptr() as *mut i32,
            length: *new_length as u32,
        }
    }
}
#[ffi_type]
#[repr(C)]
pub struct U16Slice{
    pub slice_pointer: *mut i32,
    pub length: u32,
}
impl U16Slice{

    // Constructs a U8 slice of length "newLength"
    pub fn new(new_length: &u32) -> U16Slice {
        let mut slice_vec: Vec<u8> = Vec::with_capacity(*new_length as usize);
        U16Slice {
            length: *new_length,
            slice_pointer: slice_vec.as_mut_slice().as_mut_ptr() as *mut i32,
        }
    }

    // Convert to standard rust u16 slice
    pub fn as_rust_u16_slice_mut(&self) -> &mut [u16] {
        unsafe{
            let self_slice: &mut [u16] = slice::from_raw_parts_mut(
                self.slice_pointer as *mut u16,
                self.length as usize
            );
            return self_slice;
        }           
    }

    // Takes a rust u16 slice and converts it into this custom slice
    pub fn from_rust_u16_slice_mut(
        source: &mut [u16],
        new_length: &usize) 
    -> U16Slice{
        U16Slice{
            slice_pointer: source.as_mut_ptr() as *mut i32,
            length: *new_length as u32,
        }
    }
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
// Functions related to CustomRustString
impl CustomRustString{

    // Default constructor
    pub fn new(new_length: &u32) -> CustomRustString{
        CustomRustString{
            capacity: *new_length,
            string_slice: U8Slice::new(new_length)
        }
    }

    // Construct a string from a normal rust string.
    pub fn from_string(source_string: String) -> CustomRustString{
        let string_size: usize = source_string.len() as usize;
        let mut source_string_clone: String = source_string.clone();
        unsafe{
            return CustomRustString 
            { 
                capacity: string_size as u32,
                string_slice: U8Slice::from_rust_u8_slice_mut(source_string_clone.as_mut_vec().as_mut_slice(), &string_size),
            }
        }
    }

    // Convert the contents of the object into a normal Rust string
    pub fn as_string(&self) -> String{
        return String::from_utf8_lossy(self.string_slice.as_rust_u8_slice_mut()).to_string();
    }
}


// Custom string slice
#[ffi_type]
#[repr(C)]
pub struct CustomRustStringSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}