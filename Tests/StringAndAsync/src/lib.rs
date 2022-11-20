use std::ffi::CString;

use interoptopus::{ffi_function, ffi_type, Inventory, InventoryBuilder, function, patterns::primitives::FFICChar};
use libc::c_char;


#[ffi_function]
#[no_mangle]
pub extern "C" fn hello_world()-> *mut c_char{
    println!("Hello from WSU");
    let mut test_string = String::from("Hello from the string");
    let holder = CString::new(test_string).unwrap();
    return holder.into_raw()
}



pub fn my_inventory() -> Inventory {
    InventoryBuilder::new()
        .register(function!(hello_world))
        .inventory()
}
