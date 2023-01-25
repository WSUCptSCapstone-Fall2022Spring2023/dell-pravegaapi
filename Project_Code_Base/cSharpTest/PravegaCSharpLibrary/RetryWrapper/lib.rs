#![allow(
    non_snake_case,
    unused_imports
)]
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the Retry area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
pub mod retry_policy_wrapper;

use interoptopus::{Inventory, InventoryBuilder};

// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}