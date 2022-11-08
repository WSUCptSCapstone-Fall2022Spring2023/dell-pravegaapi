use interoptopus::{ffi_function, function, Inventory, InventoryBuilder, ffi_type};

/// A simple type in our FFI layer.
#[ffi_type]
#[repr(C)]
pub struct Memtest{
    thing: u32,
    thing2: u32
}

#[ffi_function]
#[no_mangle]
pub extern "C" fn mem_test_function() -> Memtest{
    let new_value = Memtest{
        thing: 1,
        thing2: 2
    };
    return new_value;
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(function!(mem_test_function))
        .inventory()
    }
}