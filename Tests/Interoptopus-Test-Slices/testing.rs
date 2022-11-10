use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};
use interoptopus::patterns::slice::{FFISlice, FFISliceMut};

/// A simple type in our FFI layer.
#[ffi_type]
#[repr(C)]
pub struct Vec2 {
    pub x: f32,
    pub y: f32,
}

/// Function using the type.
#[ffi_function]
#[no_mangle]
pub extern "C" fn my_function(input: Vec2) -> Vec2 {
    input
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn hello_world(){
    println!("Hello World");
}

//pub extern "C" fn test_arrays() -> *mut [i32; 6]{
/// Function using the type.
// #[ffi_function]
// #[no_mangle]
// pub extern "C" fn test_arrays() -> *mut [u16; 6]{

//     let mut array: [u16; 6] = [0, 1, 2, 3, 4, 5];
//     // let ptr = array.as_mut_ptr();
//     // std::mem::forget(array);
//     // ptr
//     Box::into_raw(Box::new(array))
// }

#[ffi_function]
#[no_mangle]
pub extern "C" fn pattern_ffi_slice_1(ffi_slice: FFISlice<u32>) -> u32 {
    ffi_slice.as_slice().len() as u32
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn get_value_at_index(ffi_slice: FFISlice<u32>, i: i32) -> u32 {
    let slice = ffi_slice.as_slice();
    slice[i as usize]
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(my_function)).register(function!(pattern_ffi_slice_1))
        .register(function!(get_value_at_index))
        .inventory()
    }
}