#![allow(
    non_snake_case,
    unused_imports
)]
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the ControllerClient area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{Inventory, InventoryBuilder};
use pravega_controller_client::{self, ControllerClientImpl, ControllerClient};
use tokio::{runtime::{Runtime, Handle, EnterGuard}, task::JoinHandle};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use utility_wrapper::CustomRustString;
use pravega_client_shared::Scope;
use std::{thread, time::{self, Duration}, mem::size_of, mem};

// Default Constructor for ControllerCliImpl
#[no_mangle]
extern "C" fn CreateControllerCliDefault(source_config: *const ClientConfig, source_handle: *const Handle) -> *const ControllerClientImpl
{
    unsafe 
    {
        // Get config from raw pointer
        let source_config_claimed: ClientConfig = std::ptr::read(source_config);

        // Create a new controllerclient
        let new_controller_client: ControllerClientImpl = ControllerClientImpl::new(source_config_claimed, &*source_handle);

        // Box and return
        let new_controller_cli_box: Box<ControllerClientImpl> = Box::new(new_controller_client);
        let return_pointer: *const ControllerClientImpl = Box::into_raw(new_controller_cli_box);
        return return_pointer;
    }
}

// ControllerClientImpl.create_scope()
#[no_mangle]
extern "C" fn ControllerClientImplCreateScope(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    source_scope: CustomRustString,
    callback: unsafe extern "C" fn(*const i32))
    -> ()
    {
        unsafe { 

            let raw_pointer: usize = source_controller_client_impl as *const &dyn ControllerClient as usize;
            let newScope: Scope = Scope::from(source_scope.as_string());
            let runtime: Runtime = Runtime::new().unwrap();
            let _runtime_handle: EnterGuard = runtime.enter();

            // Create the new bytewriter asynchronously with the inputted runtime and the
            runtime.block_on( async {

                // Move a controllerclient reference into memory and clone it so we don't worry about its lifetime.
                let controller_client: &dyn ControllerClient = std::ptr::read(raw_pointer as *const &dyn ControllerClient);
                
                println!("test create scope begin");
                controller_client
                    .create_scope(&newScope)
                    .await
                    .expect("create scope");
                println!("test create scope end");
                callback(1 as *const i32);
            });
    }
}


// Used for interoptopus wrapping
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
        .inventory()
    }
}