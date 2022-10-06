use interoptopus::util::NamespaceMappings;
use interoptopus::{Error, Interop};
use testing;

#[test]
fn bindings_csharp() -> Result<(), Error> {
    use interoptopus_backend_csharp::{Config, Generator};
    use interoptopus_backend_csharp::overloads::{DotNet};

    let config = Config {
        dll_name: "test_library".to_string(),
        namespace_mappings: NamespaceMappings::new("My.Company"),
        ..Config::default()
    };

    Generator::new(config, testing::my_inventory())
        .add_overload_writer(DotNet::new())
        //.add_overload_writer(Unity::new())
        .write_file("./csharpBindings/Interop.cs")?;

    Ok(())
}