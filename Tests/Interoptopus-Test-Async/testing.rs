use interoptopus::{ffi_function, ffi_type, function, Inventory, InventoryBuilder};
use futures::prelude::*;
use tokio::prelude::*;

#[ffi_function]
#[no_mangle]
#[tokio::main]
pub async extern "C" fn test_async(n: i64) -> (i64){
    let mut num = n;//: i64 = 600851475143;
    let mut prime_factors = Vec::new();
    while num % 2 == 0 {
        prime_factors.push(2);
        num /= 2;
    }
    for i in 3..(num as f64).sqrt() as i64 + 1 {
        while num % i == 0 {
            prime_factors.push(i);
            num /= i;
        }
    }
    if num > 2 {
        prime_factors.push(num);
    }
    prime_factors.sort();
    prime_factors.reverse();
    return future::ok(prime_factors[0]).await
    // println!("{}", prime_factors[0])
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new().register(function!(test_async))
        .inventory()
    }
}