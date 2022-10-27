use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};

// Testing enum. Copying by itself fails
#[ffi_type]
#[repr(C)]
pub enum TestEnum{
    One,
    Two,
    Three
} 

// Inputting it into a function as a parameter to test whether that changes the outcome
#[ffi_function]
#[no_mangle]
pub extern "C" fn enum_func(input: TestEnum) -> TestEnum{
    input
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(enum_func)).inventory()
    }
}