#![allow(
    non_snake_case,
    unused_imports
)]
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the Shared area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{Inventory, InventoryBuilder};
use utility::{CustomU128, CustomRustString};




// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}