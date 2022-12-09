use interoptopus::{ffi_type};
use interoptopus::patterns::option::FFIOption;
use utility::{CustomU128, CustomCSharpString,CustomUuid};
use shared_wrapper::{ScopedSegmentWrapper,TxIdWrapper,PravegaNodeUriWrapper,DelegationTokenWrapper};

#[ffi_type]
#[repr(C)]
pub struct TempClientFactoryAsyncWrapper
{

temp: u32,
}

#[ffi_type]
#[repr(C)]
pub struct SegmentMetadataClient {
    segment: ScopedSegmentWrapper,
    factory: TempClientFactoryAsyncWrapper,
    delegation_token_provider: DelegationTokenWrapper,
    endpoint: PravegaNodeUriWrapper,
}