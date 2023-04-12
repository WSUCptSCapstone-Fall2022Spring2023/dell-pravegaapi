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
use pravega_client_shared::{Scope, ScopedStream, Stream, Scaling, StreamConfiguration, ScaleType, RetentionType, Retention};
use std::{thread, time::{self, Duration}, mem::size_of, mem, env::join_paths, ptr::null};
use pravega_client::{client_factory::{ClientFactory, self}};
use pravega_client::client_factory::ClientFactoryAsync;
use num::FromPrimitive;

// Default Constructor for ControllerCliImpl
#[no_mangle]
extern "C" fn CreateControllerCliDefault(
    client_factory_pointer: &'static ClientFactory,
    source_config: *const ClientConfig) -> *const ControllerClientImpl
{
    unsafe 
    {
        // Get config from raw pointer
        let source_config_claimed: ClientConfig = std::ptr::read(source_config);

        // Enter Client Factory Runtime
        return client_factory_pointer.runtime().block_on(async {

            // Create a new controllerclient
            let new_controller_client: ControllerClientImpl = ControllerClientImpl::new(source_config_claimed, &client_factory_pointer.runtime_handle());

            // Box and return
            let new_controller_cli_box: Box<ControllerClientImpl> = Box::new(new_controller_client);
            let return_pointer: *const ControllerClientImpl = Box::into_raw(new_controller_cli_box);
            return return_pointer;
        });
    }
}

// ControllerClientImpl.create_scope()
#[no_mangle]
extern "C" fn ControllerClientImplCreateScope(
    client_factory_pointer: &'static ClientFactory,
    source_controller_client_impl: &dyn ControllerClient, 
    source_scope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
    {
        unsafe { 

            let newScope: Scope = Scope::from(source_scope.as_string());
            
            // Create the new scope asynchronously 
            client_factory_pointer.runtime().block_on( async move {

                //println!("test create scope begin");
                source_controller_client_impl
                    .create_scope(&newScope)
                    .await
                    .expect("create scope");
                //println!("test create scope end");
                callback(key, 1);
            });        
        }
}

// ControllerClientImpl.create_stream()
#[no_mangle]
extern "C" fn ControllerClientImplCreateStream(
    client_factory_pointer: &'static ClientFactory,
    source_controller_client_impl: &dyn ControllerClient, 
    newStream: CustomRustString,
    targetScope: CustomRustString,
    scaling_type: i32,
    scaling_target_rate: i32,
    scaling_factor: i32,
    scaling_min_num_segments: i32,
    retention_type: i32,
    retention_param: i32,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
{

    unsafe
    {
        // Initialize locals
        // StreamConfig
        //  -ScaleType
        let st = ScaleType::from_i64(scaling_type as i64);
        let st_unwrapped: ScaleType;
        if st == None{
            println!("Invalid Scale Type inputted");
            callback(key, 0);
            return;
        }
        else{
            st_unwrapped = st.unwrap();
        }
        //  -RetentionType
        let rt = RetentionType::from_i64(retention_type as i64);
        let rt_unwrapped: RetentionType;
        if rt == None{
            println!("Invalid Retention Type inputted");
            callback(key, 0);
            return;
        }
        else{
            rt_unwrapped = rt.unwrap();
        }
        //  -StreamConfig
        let stream_config: StreamConfiguration = StreamConfiguration {
            scoped_stream: ScopedStream {
                scope: Scope::from(targetScope.as_string().to_owned()),
                stream: Stream::from(newStream.as_string().to_owned()),
            },
            scaling: Scaling {
                scale_type: st_unwrapped,
                target_rate: scaling_target_rate,
                scale_factor: scaling_factor,
                min_num_segments: scaling_min_num_segments,
            },
            retention: Retention {
                retention_type: rt_unwrapped,
                retention_param: retention_param as i64,
            },
            tags: None,
        };

        // Create the new stream asynchronously
        client_factory_pointer.runtime().block_on( async move {

            source_controller_client_impl
                .create_stream(&stream_config)
                .await
                .expect("create stream");
            callback(key, 1);
        });      
    }
}