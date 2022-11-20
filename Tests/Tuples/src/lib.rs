use std::ffi::CString;

use interoptopus::{ffi_function, ffi_type, Inventory, InventoryBuilder, function, patterns::primitives::FFICChar};
use libc::c_char;

#[ffi_type]
#[repr(C)]
pub struct Mytuple {
    pub x: u32,
    pub y: bool,
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn hello_tuple()->Mytuple{
    Mytuple { x: (3), y: (true) }
}



pub fn my_inventory() -> Inventory {
    InventoryBuilder::new()
        .register(function!(hello_tuple))
        .inventory()
}
