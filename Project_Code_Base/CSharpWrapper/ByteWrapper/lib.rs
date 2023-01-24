use interoptopus::{ffi_type};
use interoptopus::patterns::option::FFIOption;
use utility::{CustomU128, CustomCSharpString,CustomUuid};
use shared_wrapper::{ScopedSegmentWrapper,TxIdWrapper};
use segment_wrapper::{SegmentMetadataClient};

#[ffi_type]
#[repr(C)]
pub struct TempClientFactoryAsyncWrapper
{

temp: u32,
}

#[ffi_type]
#[repr(C)]
pub struct ByteReaderWrapper
{
    pub segment :ScopedSegmentWrapper,
    buffer_size: u64,
    metadata_client: SegmentMetadataClient,
    factory: TempClientFactoryAsyncWrapper,
    reader_id: CustomUuid

    //Also needs reader, will be implemented with reader

}

#[ffi_type]
#[repr(C)]
pub struct ByteWriterWrapper{
    writerId: TxIdWrapper,
    scoped_segment: ScopedSegmentWrapper,
    metadata_client: SegmentMetadataClient,
    factory: TempClientFactoryAsyncWrapper,
    offset: i64,
    //event_handles
    //sender
}