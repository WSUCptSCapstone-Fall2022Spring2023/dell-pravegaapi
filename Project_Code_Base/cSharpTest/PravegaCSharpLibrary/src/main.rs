#![allow(
    non_snake_case,
    unused_imports
)]
///
/// Contains methods for running functions from C# in the Client Factory Module.
/// 

// Relevant imports
use interoptopus::{Inventory, InventoryBuilder};
use pravega_controller_client::{self, ControllerClientImpl, ControllerClient};
use tokio::{runtime::{Runtime, Handle, EnterGuard}, task::JoinHandle};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use utility_wrapper::CustomRustString;
use pravega_client_shared::Scope;
use std::{thread, time::{self, Duration}, mem::size_of, mem};
use pravega_client::{client_factory::ClientFactory};
use pravega_client::client_factory::ClientFactoryAsync;
use once_cell::sync::OnceCell;
use debugless_unwrap::*;

static INSTANCE: OnceCell<ClientFactory> = OnceCell::new();

fn main() -> Result<(), Box<dyn std::error::Error>> { 

    let config = ClientConfigBuilder::default()
    .controller_uri("localhost:9090")
    .build()
    .unwrap();

    
    let factory = ClientFactory::new(config);
    INSTANCE.set(factory).debugless_unwrap();
    println!("client factory created");

    let handle = INSTANCE.get().unwrap().runtime().spawn(async move {
        let controller_client = INSTANCE.get().unwrap().controller_client();

        // create a scope
        let scope = Scope::from("fooScope".to_owned());
        controller_client
            .create_scope(&scope)
            .await
            .expect("create scope");
        println!("scope created");
    });

    while handle.is_finished() == false {}

    Ok(())
}

