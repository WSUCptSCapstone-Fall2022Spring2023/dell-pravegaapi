use interoptopus::{Inventory, InventoryBuilder};

pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}