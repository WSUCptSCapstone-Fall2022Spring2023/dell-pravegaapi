use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder, ffi_function, function, lang::rust};
use utility::{CustomRustStringSlice, CustomRustString};
use std::io::{stdout, Write};
use utility::U8Slice;

// Functions used for testing the functionality of passing strings between C# and Rust 
#[ffi_function]
#[no_mangle]
pub extern "C" fn test(input: CustomRustString) -> CustomRustString{
    return internal_test(input);
}
pub fn internal_test(input: CustomRustString) -> CustomRustString{

    // Convert to string
    let mut i: u32 = 0;
    let testing_slice: &[u8] = input.string_slice.as_rust_u8_slice_mut();

    // Print bytes as chars
    println!("Pringing recieved chars from input::");
    while i < (input.capacity as u32) {
        print!("{0}", testing_slice[i as usize] as char);
        i = i+1;
    }

    // Convert to regular rust string
    let input_string_in_rust: String = input.as_string();
    println!("\noutput::");
    println!("Converted input into Rust string containing '{0}'.", input_string_in_rust);
    println!("test working");
    
    // Convert back and return
    let rust_string: CustomRustString = CustomRustString::from_string(input_string_in_rust);

    println!("{0} {1}", rust_string.capacity, rust_string.string_slice.length);
    return rust_string;
}

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .register(function!(test))
        .inventory()
    }
}