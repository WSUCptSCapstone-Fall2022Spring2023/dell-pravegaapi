///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the Index module. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///

// Holder structs for IndexReader. To be implemented on a later data when the Index module is our focus
#[ffi_type]
#[repr(C)]
pub struct ScopedStreamHolder{
    inner: u8,
}
#[ffi_type]
#[repr(C)]
pub struct ClientFactoryAsyncHolder{
    inner: u8,
}
#[ffi_type]
#[repr(C)]
pub struct SegmentMetadataClientHolder{
    inner: u8,
}
#[ffi_type]
#[repr(C)]
pub struct AsyncSegmentReaderImplHolder{
    inner: u8,
}



// Holder structs for IndexWriter. To be implemented on a later data when the Index module is our focus
#[ffi_type]
#[repr(C)]
pub struct ByteWriterHolder{
    inner: u8,
}



// IndexReader in palatable C
#[ffi_type]
#[repr(C)]
pub struct IndexReaderWrapper{
    stream: ScopedStreamHolder,
    factory: ClientFactoryAsyncHolder,
    meta: SegmentMetadataClientHolder,
    segment_reader: AsyncSegmentReaderImplHolder,
}
/*
    Originally from pravega-client-rust/src/index/reader.rs 
    as:
    pub struct IndexReader {
        stream: ScopedStream,
        factory: ClientFactoryAsync,
        meta: SegmentMetadataClient,
        segment_reader: AsyncSegmentReaderImpl,
    }
*/



// IndexReader in palatable C
#[ffi_type]
#[repr(C)]
pub struct IndexWriterWrapper<T>
    where
    T: CTypeInfo,
{
    byte_writer: ByteWriterHolder,
    hashed_fields: FFIOption<U128U64TupleSlice>,
    fields: FFIOption<T>,

    // Non transferrable and no purpose in doing so.
    //_fields_type: PhantomData<T>,
}
/*
    Originally from
    as: 
    pub struct IndexWriter<T: Fields + PartialOrd + PartialEq + Debug> {
        byte_writer: ByteWriterHolder,
        hashed_fields: FFIOption<Vec<(u128, u64)>>,
        fields: Option<T>,
        _fields_type: PhantomData<T>,
    }
*/
