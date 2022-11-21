use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};
use interoptopus::patterns::slice::{FFISlice, FFISliceMut};
use interoptopus::patterns::option::FFIOption;

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

#[ffi_function]
#[no_mangle]
pub extern "C" fn  add_one_v1   (x: u32) -> u32 { x + 1 }
//let add_one_v2 = |x: u32| -> u32 { x + 1 };

#[ffi_type]
#[repr(C)]
pub struct Inner {
    x: f32,
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn test_iterators() -> std::slice::Iter<*mut i32>{
    let a = [1, 2, 3];
    let mut iter = a.iter();
    return iter;
}
// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(my_function)).register(function!(test_iterators))
        .inventory()
    }
}