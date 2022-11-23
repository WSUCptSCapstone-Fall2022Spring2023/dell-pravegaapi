use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};


// U128 wrapper for sending between C# and Rust
// NOTE: u128 normally is comprised of 1 value, but u128 is not C palatable and as such can't be transferred
//  between C# and Rust. The solution here is to split the two halves of the u128 into two u64 values that
//  are C palatable. 
//  -When sent from one side to another, a u128 value is split into the two halves and bitwise ORed into the
//      two halves of this struct.
//  -When recieved from another wise, the first and second halves are ORed at different points on a u128 value
//      initialized at 0. first_half -> bits 0-63 and second_half -> bits 64-127. This builds the u128 back up
//      from its parts.
//  There isn't an easy way to transfer a value this big between the two sides, but doing so is O(1) each time.
//      For now, this is the fastest way to go between the two without risking using slices.
#[ffi_type]
#[repr(C)]
pub struct CustomU128{
    pub first_half: u64,
    pub second_half: u64,
}



#[ffi_type]
#[repr(C)]
// Wrapper for WriterID
pub struct WriterIdWrapper{
    pub inner: CustomU128,
}
/* Originally from pravega-client-rust/shared/src/lib.rs 
    as:
    #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq)]
    pub struct WriterId(pub u128);
*/



// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(CustomU128))
            .register(extra_type!(WriterIdWrapper))
        .inventory()
    }
}