
///
/// File: credentialswrapper.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the PravegaConfig area. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
pub mod connection_type_wrapper;
pub mod credentials_wrapper;


use interoptopus::{ffi_type};
use utility::{CustomCSharpString, U16Tuple};
use shared_wrapper::PravegaNodeUriWrapper;
use retry_wrapper::retry_policy_wrapper::RetryWithBackoffWrapper;


use connection_type_wrapper::ConnectionTypeWrapper;
use credentials_wrapper::CredentialsWrapper;

// Client Config in palatable C# 
#[ffi_type]
#[repr(C)]
pub struct ClientConfigWrapper{

    // No issue transferring
    pub max_controller_connections: u32,

    // Connection Type -> Mock(MockType) + Tokio
    // Mock() imported library X. Instead, "Happy, SegmentIsSealed, SegmentIsTruncated, WrongHost," which is inside MockType
    pub connection_type: ConnectionTypeWrapper,

    // See above
    pub retry_policy: RetryWithBackoffWrapper,

    // See above
    pub controller_uri: PravegaNodeUriWrapper,

    // No issue transferring
    pub transaction_timeout_time: u64,

    // No issue transferring
    pub mock: bool,

    // No issue transferring
    pub is_tls_enabled: bool,

    // No issue transferring
    pub disable_cert_verification: bool,    

    // See above
    pub credentials: CredentialsWrapper,

    // No issue transferring
    pub is_auth_enabled: bool,

    // usize -> u64
    pub reader_wrapper_buffer_size: u64,

    // duration tuple -> array
    pub request_timeout: U16Tuple,

    // 
    pub trustcerts: CustomCSharpString,
}
/*
 Originally from pravega-client-rust/config/src/lib.rs 
  as: 

pub struct ClientConfig{

    // No issue transferring
    #[get_copy = "pub"]
    #[builder(default = "u32::max_value()")]
    pub max_controller_connections: u32,

    // Connection Type -> Mock(MockType) + Tokio
    // Mock() imported library X. Instead, "Happy, SegmentIsSealed, SegmentIsTruncated, WrongHost," which is inside MockType
    #[get_copy = "pub"]
    #[builder(default = "ConnectionType::Tokio")]
    pub connection_type: ConnectionType,

    // See above
    #[get_copy = "pub"]
    #[builder(default = "RetryWithBackoff::default()")]
    pub retry_policy: RetryWithBackoff,

    // See above
    #[get]
    pub controller_uri: PravegaNodeUri,

    #[get_copy = "pub"]
    #[builder(default = "90 * 1000")]
    pub transaction_timeout_time: u64,

    #[get_copy = "pub"]
    #[builder(default = "false")]
    pub mock: bool,

    #[get_copy = "pub"]
    #[builder(default = "self.default_is_tls_enabled()")]
    pub is_tls_enabled: bool,

    #[builder(default = "false")]
    pub disable_cert_verification: bool,

    #[builder(default = "self.extract_trustcerts()")]
    pub trustcerts: Vec<String>,

    #[builder(default = "self.extract_credentials()")]
    pub credentials: Credentials,

    #[get_copy = "pub"]
    #[builder(default = "false")]
    pub is_auth_enabled: bool,

    #[get_copy = "pub"]
    #[builder(default = "1024 * 1024")]
    pub reader_wrapper_buffer_size: usize,

    #[get_copy = "pub"]
    #[builder(default = "self.default_timeout()")]
    pub request_timeout: Duration,
}
*/


