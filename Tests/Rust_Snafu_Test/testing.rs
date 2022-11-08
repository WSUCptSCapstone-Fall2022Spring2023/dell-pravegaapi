use interoptopus::{ffi_function, function, Inventory, InventoryBuilder, patterns::string};
use snafu::{Snafu, Backtrace, ErrorCompat, ensure};

/// A simple type in our FFI layer.
#[derive(Debug, Snafu)]
pub enum Err{
    #[snafu(display("Conditional check failed: {}", msg))]
    ConditionalCheckFailure { msg: String },

    #[snafu(display("Internal error: {}", msg))]
    InternalFailure { msg: String },

    #[snafu(display("Input is invalid: {}", msg))]
    InvalidInput { msg: String, backtrace: Backtrace },
}

#[ffi_function]
#[no_mangle]
pub fn error_checking_test() -> Result<(), Err>{
    let val = String::from("test");
        ensure!(1 == 0, InternalFailure {msg: val});
        Ok(true);
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(function!(error_checking_test))
        .inventory()
    }
}