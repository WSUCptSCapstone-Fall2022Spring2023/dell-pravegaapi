use interoptopus::util::NamespaceMappings;
use interoptopus::{Error, Interop};
use testing;
use std::env;

#[test]
fn bindings_csharp() -> Result<(), Error> {
    use interoptopus_backend_csharp::{Config, Generator};
    use interoptopus_backend_csharp::overloads::{DotNet};
    let path = env::current_dir()?;
    //path.parent().unwrap();
    //path.parent().unwrap().display().join(r"\target\debug\deps\testing.dll".to_string()).to_str().unwrap().to_string();
    //"../target/debug/deps/testing.dll".to_string()
    //path.join(r"\target\debug\deps\testing.dll".to_string()).to_str().unwrap().to_string()
    let folder_dir = path.display().to_string();
    let mut dir_string = String::from(r#"@""#.to_owned() + &folder_dir + (r#"\target\debug\deps\testing.dll"#));
    dir_string.trim_start().chars().next();
    // folder_dir = folder_dir.replace(r"\", "/");
    let config = Config {
        dll_name: dir_string,
        namespace_mappings: NamespaceMappings::new("My.Company"),
        ..Config::default()
    };

    Generator::new(config, testing::my_inventory())
        .add_overload_writer(DotNet::new())
        .write_file("./csharpBindings/Interop.cs")?;

    Ok(())
}