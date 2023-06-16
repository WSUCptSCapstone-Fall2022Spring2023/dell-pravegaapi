#![allow(
    non_snake_case,
    unused_imports
)]
/// File: controller_client.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the ControllerClient area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.

// Global Imports
use crate::LIBRARY_CLIENT_FACTORY;

// Crate Imports
use crate::utility::{CustomRustString, CustomRustStringSlice};

// Library Imports
use pravega_controller_client::{self, ControllerClientImpl, ControllerClient, paginator::list_scopes};
use tokio::{runtime::{Runtime, Handle, EnterGuard}, task::JoinHandle};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use pravega_client_shared::{CToken, Scope, ScopedStream, Stream, Scaling, StreamConfiguration, ScaleType, RetentionType, Retention};
use std::{thread, time::{self, Duration}, mem::size_of, mem, env::join_paths, ptr::null};
use pravega_client::{client_factory::{ClientFactory, self}};
use pravega_client::client_factory::ClientFactoryAsync;
use pravega_controller_client::ResultRetry;
use num::FromPrimitive;
use tracing::error;
use tracing::info;

// ControllerClientImpl.create_scope()
#[no_mangle]
extern "C" fn ControllerClientImplCreateScope(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    source_scope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
    {
        unsafe { 

            let newScope: Scope = Scope::from(source_scope.as_string());
            
            // Create the new scope asynchronously 
            LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

                source_controller_client_impl
                    .create_scope(&newScope)
                    .await
                    .expect("create scope");
                callback(key, 1);
            });        
        }
}

// ControllerClientImpl.check_scope_exists()
#[no_mangle]
extern "C" fn ControllerClientImplCheckScopeExists(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    source_scope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
    {
        unsafe { 

            let newScope: Scope = Scope::from(source_scope.as_string());
            
            // Check if the scope exists asynchronously 
            LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

                let result: bool = source_controller_client_impl
                    .check_scope_exists(&newScope)
                    .await
                    .expect("check scope exists");
                if result == true{
                    callback(key, 1);
                }
                else{
                    callback(key, 0);
                }
            });        
        }
    }

// ControllerClientImpl.list_scopes()
#[no_mangle]
extern "C" fn ControllerClientImplListScopes(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    key: u64,
    callback: unsafe extern "C" fn(u64, *mut i32, u32)
) -> (){

    unsafe{

    
        // Get a list of all the scopes the controller is tied to asynchronously
        LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

            // Attempt to get the list of scopes from the controller client.      
            let token: CToken = CToken::new("".to_owned());
            
        
            // Initial state with an empty Continuation token.
            // execute a request to the controller.
            let res: ResultRetry<Option<(Vec<Scope>, CToken)>> = source_controller_client_impl.list_scopes(&token).await;
            match res {

                // If the result is none, callback and return an empty array pointer.
                Ok(None) => {
                    callback(key, 0 as *mut i32, 0);                
                },
                // If the result isn't none, create a CustomRustString list
                Ok(Some((list, _ct))) => {
                    // Unpack the list and transfer it into arrays
                    let mut rawArray: Vec<CustomRustString> = Vec::<CustomRustString>::new();
                    let mut returnArraySize: u32 = 0;
                    for scope in list
                    {
                        let insertString: CustomRustString = CustomRustString::from_string(scope.name.to_owned());
                        rawArray.push(insertString);
                        returnArraySize = returnArraySize + 1;
                    }

                    // Turn the vector into a mutable slice of pointers
                    let processedArray = rawArray.as_mut_slice();
                    let returnSlice: CustomRustStringSlice = CustomRustStringSlice::from_rust_string_slice_mut(
                        processedArray,
                        &(returnArraySize as usize)
                    ); 
                        
                    // Box the return array and return along with its size
                    let returnArrayBox: Box<CustomRustStringSlice> = Box::new(returnSlice);
                    let returnArrayPointer: *mut i32 = Box::into_raw(returnArrayBox) as *mut i32;
                    callback(key, returnArrayPointer, returnArraySize);           
                }
                // Error case
                Err(e) => {
                    //log an error and return None to indicate end of stream.
                    error!("Error while attempting to list scopes. Error: {:?}", e);
                }
            }
        });    
    } 
}


// ControllerClientImpl.delete_scope()
#[no_mangle]
extern "C" fn ControllerClientImplDeleteScope(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    source_scope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
    {
        unsafe { 

            let newScope: Scope = Scope::from(source_scope.as_string());
            
                // Delete the scope asynchronously 
                LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {
                    
                    let result: bool = source_controller_client_impl
                        .delete_scope(&newScope)
                        .await
                        .expect("delete scope");
                    if result == true{
                        callback(key, 1);
                    }
                    else{
                        callback(key, 0);
                    }                   
                });     
        }
    }

// ControllerClientImpl.create_stream()
#[no_mangle]
extern "C" fn ControllerClientImplCreateStream(
    source_controller_client_impl: &mut &dyn ControllerClient, 
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
        // ControllerClient
        let raw_pointer: usize = source_controller_client_impl as *const &dyn ControllerClient as usize;

        // Create the new stream asynchronously
        LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

            // Move a controllerclient reference into memory and clone it so we don't worry about its lifetime.
            let controller_client: &dyn ControllerClient = std::ptr::read(raw_pointer as *const &dyn ControllerClient);

            controller_client
                .create_stream(&stream_config)
                .await
                .expect("create stream");
            callback(key, 1);
        });      
    }
}

// ControllerClientImpl.check_stream_exists()
#[no_mangle]
extern "C" fn ControllerClientImplCheckStreamExists(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    targetStream: CustomRustString,
    targetScope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
{

    unsafe
    {
        // Initialize locals
        // ScopedStream
        let targetSS: ScopedStream = ScopedStream{
            scope: Scope::from(targetScope.as_string().to_owned()),
            stream: Stream::from(targetStream.as_string().to_owned()),
        };

        // Check for the stream asynchronously
        LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

            let result = source_controller_client_impl
                .check_stream_exists(&targetSS)
                .await
                .expect("check stream exists");
            if result == true{
                callback(key, 1);
            }
            else{
                callback(key, 0);
            }
        });      
    }
}

// ControllerClientImpl.delete_stream()
#[no_mangle]
extern "C" fn ControllerClientImplDeleteStream(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    targetStream: CustomRustString,
    targetScope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
{

    unsafe
    {
        // Initialize locals
        // ScopedStream
        let targetSS: ScopedStream = ScopedStream{
            scope: Scope::from(targetScope.as_string().to_owned()),
            stream: Stream::from(targetStream.as_string().to_owned()),
        };

        // Delete the stream asynchronously
        LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

            let result = source_controller_client_impl
                .delete_stream(&targetSS)
                .await
                .expect("delete stream");
            if result == true{
                callback(key, 1);
            }
            else{
                callback(key, 0);
            }
        });      
    }
}

// ControllerClientImpl.seal_stream()
#[no_mangle]
extern "C" fn ControllerClientImplSealStream(
    source_controller_client_impl: &mut &dyn ControllerClient, 
    targetStream: CustomRustString,
    targetScope: CustomRustString,
    key: u64,
    callback: unsafe extern "C" fn(u64, u64))
    -> ()
{

    unsafe
    {
        // Initialize locals
        // ScopedStream
        let targetSS: ScopedStream = ScopedStream{
            scope: Scope::from(targetScope.as_string().to_owned()),
            stream: Stream::from(targetStream.as_string().to_owned()),
        };

        // Seal the stream asynchronously
        LIBRARY_CLIENT_FACTORY.get().unwrap().runtime().block_on( async move {

            let result = source_controller_client_impl
                .seal_stream(&targetSS)
                .await
                .expect("seal stream");
            if result == true{
                callback(key, 1);
            }
            else{
                callback(key, 0);
            }
        });      
    }
}