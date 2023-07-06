#![allow(
    non_snake_case,
    unused_imports
)]
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains methods transferred from the Shared area. Not all methods are transferred, only necessary ones.
///     Provides definitions on the Rust side.

// Global Imports
use crate::LIBRARY_CLIENT_FACTORY;

// Crate Imports
use crate::utility::{CustomRustString, U8Slice};

// Library Imports
use pravega_client_shared::{PravegaNodeUri};

/////////////////////
// PravegaNodeUri Methods
/////////////////////

// Utility function used to assemble UriParts into a PravegaNodeUri
pub fn PravegaNodeUriFromParts(parts: (CustomRustString, CustomRustString, u16)) -> PravegaNodeUri{
    let newUri = format!("{}://{}:{}", parts.0.as_string(), parts.1.as_string(), parts.2.to_string());
    return PravegaNodeUri(newUri);
}


// Create PravegaNodeUri
#[no_mangle]
pub extern "C" fn CreatePravegaNodeUri(
   new_scheme: CustomRustString,
   new_domain_name: CustomRustString,
   new_port: u16,
) -> *mut i32
{
    // Create the node uri from parts
    let new_node_uri: PravegaNodeUri = PravegaNodeUriFromParts((new_scheme, new_domain_name, new_port));

    // Box and return
    let node_box: Box<PravegaNodeUri> = Box::new(new_node_uri);
    let node_box_pointer: *mut i32 = Box::into_raw(node_box) as *mut i32;
    return node_box_pointer;
}

// Get FullAddress
#[no_mangle]
pub extern "C" fn PravegaNodeUriFullAddress(
    box_pointer: &mut PravegaNodeUri
) -> CustomRustString
{
    return CustomRustString::from_string(format!("{}://{}:{}", box_pointer.scheme().unwrap(), box_pointer.domain_name(), box_pointer.port().to_string()));
}

// Get Scheme
#[no_mangle]
pub extern "C" fn PravegaNodeUriGetScheme(
    box_pointer: &mut PravegaNodeUri
) -> CustomRustString
{
    let return_string: CustomRustString = CustomRustString::from_string(box_pointer.scheme().unwrap());
    return return_string;
}

// Get Domain
#[no_mangle]
pub extern "C" fn PravegaNodeUriGetDomain(
    box_pointer: &mut PravegaNodeUri
) -> CustomRustString
{
    let return_string: CustomRustString = CustomRustString::from_string(box_pointer.domain_name());
    return return_string;
}

// Get Port
#[no_mangle]
pub extern "C" fn PravegaNodeUriGetPort(
    box_pointer: &mut PravegaNodeUri
) -> u16
{
    return box_pointer.port();
}
