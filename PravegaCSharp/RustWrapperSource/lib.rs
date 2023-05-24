#![allow(
    non_snake_case,
    unused_imports,
    dead_code
)]
///
/// Contains the pub mod architecture needed for using all the categorized rust wrapper files under one name.
/// Also contains globals used throughout the Rust side of the Wrapper Library.
/// 

// Library imports
use pravega_client::{client_factory::{ClientFactory, ClientFactoryAsync}, byte::{ByteReader, ByteWriter}};
use pravega_client_config::{ClientConfig, ClientConfigBuilder};
use pravega_controller_client::{ControllerClient, ControllerClientImpl, mock_controller::MockController};
use once_cell::sync::OnceCell;
use pravega_client_config::{connection_type};
use pravega_client_shared::{ScopedStream, Scope, Stream, StreamConfiguration, Scaling, ScaleType, Retention, RetentionType};
use std::{time::Instant, string};
use std::io::{Error, ErrorKind, SeekFrom};
use tokio::runtime::{Runtime, Handle};

// File imports
pub mod auth;
pub mod byte;
pub mod client_factory;
pub mod client_config;
pub mod connection_pool;
pub mod controller_client;
pub mod event;
pub mod index;
pub mod retry;
pub mod segment;
pub mod shared;
pub mod sync;
pub mod utility;
pub mod wire_protocol;

// Globals
const TESTING_AMOUNT: i32 = 10;
static LIBRARY_CLIENT_FACTORY: OnceCell<ClientFactory> = OnceCell::new();

fn main() -> () { 
}

