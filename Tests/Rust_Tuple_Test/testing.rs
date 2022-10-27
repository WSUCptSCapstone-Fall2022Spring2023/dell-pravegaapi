use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};

// struct containing tuple
#[ffi_type]
#[repr(C)]
pub struct TupleStruct{
    val: (i32, bool)
}

// Inputting it into a function as a parameter to test whether that changes the outcome
#[ffi_function]
#[no_mangle]
pub extern "C" fn tuple_func(input: (i32, bool)) -> (bool, i32){
    let (int_param, bool_param) = pair;

    (bool_param, int_param)
}


// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(tuple_func)).inventory()
    }
}