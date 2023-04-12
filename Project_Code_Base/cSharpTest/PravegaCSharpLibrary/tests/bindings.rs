#![allow(
    non_snake_case,
    unused_imports
)]
use interoptopus::util::NamespaceMappings;
use interoptopus::{Error, Interop, Inventory, InventoryBuilder};
use PravegaCSharpWrapper;

/* 
#[test]
fn bindings_csharp() -> Result<(), Error> {
    use interoptopus_backend_csharp::{Config, Generator};
    use interoptopus_backend_csharp::overloads::{DotNet};

    let config = Config {
        dll_name: "PravegaCSharp".to_string(),
        namespace_mappings: NamespaceMappings::new("Pravega"),
        ..Config::default()
    };

    //let mut thing: InventoryBuilder = InventoryBuilder::new();
    // Auth
    //thing = PravegaCSharp::auth_inventory(thing);


    Generator::new(config, PravegaCSharp::my_inventory())
        .add_overload_writer(DotNet::new())
        .write_file("./csharpBindings/PravegaCSharp.cs")?;

    Ok(())
}
*/