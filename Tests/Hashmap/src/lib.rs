use std::{ffi::CString, collections::HashMap};

use interoptopus::{ffi_function, ffi_type, Inventory, InventoryBuilder, function, patterns::primitives::FFICChar};
use libc::c_char;


#[repr(C)]
pub struct MyHash {
    pub x: HashMap<u32,bool>,

}

#[ffi_function]
#[no_mangle]
pub extern "C" fn hello_tuple(){
   
}



pub fn my_inventory() -> Inventory {
    InventoryBuilder::new()
        .register(function!(hello_tuple))
        .inventory()
}
