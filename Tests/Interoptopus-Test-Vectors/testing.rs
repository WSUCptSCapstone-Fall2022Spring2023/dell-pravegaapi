use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};

/// A simple type in our FFI layer.
#[ffi_type]
#[repr(C)]
pub struct Vec2 {
    pub x: f32,
    pub y: f32,
}

#[ffi_type]
#[repr(C)]
pub struct Inner {
    x: f32,
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn test_errors() -> Vec<i32>{
    return panic!("Something bad happened")
    //println!("Hellow World")
}
// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(my_function)).register(function!(test_vectors))
        .inventory()
    }
}