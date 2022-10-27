use interoptopus::{ffi_function, constant, Inventory, InventoryBuilder, ffi_constant};

// struct 
#[ffi_constant]
pub const TEST_GLOBAL: i32 = 4;
// Inputting it into a function as a parameter to test whether that changes the outcome
#[ffi_function]
#[no_mangle]
pub extern "C" fn func_using_global() -> i32{
    0
}


// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(constant!(TEST_GLOBAL))
            .inventory()
    }
}