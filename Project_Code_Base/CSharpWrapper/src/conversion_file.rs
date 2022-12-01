use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder, ffi_function, function, lang::rust};
use utility::{CustomRustStringSlice, CustomRustString};
use std::io::{stdout, Write};
use utility::U8Slice;

#[ffi_function]
#[no_mangle]
pub extern "C" fn test(input: CustomRustString) -> CustomRustString{
    return internal_test(input);
}
pub fn internal_test(input: CustomRustString) -> CustomRustString{

    // Convert to string
    let mut i: u32 = 0;
    let testing_slice: &[u8] = input.string_slice.as_rust_u8_slice_mut();
    while i < (input.capacity as u32) {
        print!("{0}", testing_slice[i as usize] as char);
        i = i+1;
    }

    let input_string_in_rust: String = input.as_string();
    println!("output::");
    println!("Converted input into {0} .", input_string_in_rust);

    // Convert back and return
    let rust_string: CustomRustString = CustomRustString::from_string(input_string_in_rust);
    return rust_string;
}

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .register(function!(test))
        .inventory()
    }
}