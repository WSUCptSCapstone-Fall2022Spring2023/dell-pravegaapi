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



#[ffi_type]
#[repr(C)]
pub struct Inner {
    x: f32,
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn pattern_ffi_option_1(ffi_slice: FFIOption<Inner>) -> FFIOption<Inner> {
    ffi_slice
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn pattern_ffi_option_2(ffi_slice: FFIOption<Inner>) -> Inner {
    ffi_slice.into_option().unwrap_or(Inner { x: f32::NAN })
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(my_function)).register(function!(pattern_ffi_option_1))
        .register(function!(pattern_ffi_option_2))
        .inventory()
    }
}