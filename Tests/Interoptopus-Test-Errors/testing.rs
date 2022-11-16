use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};

#[ffi_function]
#[no_mangle]
pub extern "C" fn test_errors() -> std::error::Error{
    return panic!("Something bad happened")
    //println!("Hellow World")
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(test_errors))
        .inventory()
    }
}