use interoptopus::util::NamespaceMappings;
use interoptopus::{Error, Interop};
use Pravega;

#[test]
fn bindings_csharp() -> Result<(), Error> {
    use interoptopus_backend_csharp::{Config, Generator};
    use interoptopus_backend_csharp::overloads::{DotNet};

    let config = Config {
        dll_name: "ClientFactoryWrapper".to_string(),
        namespace_mappings: NamespaceMappings::new("Pravega"),
        ..Config::default()
    };

    Generator::new(config, Pravega::my_inventory())
        .add_overload_writer(DotNet::new())
        .write_file("./csharpBindings/ClientFactoryWrapperSegmentMetaData.cs")?;

    Ok(())
}