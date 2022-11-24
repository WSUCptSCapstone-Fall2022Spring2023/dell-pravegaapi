use interoptopus::lang::rust::CTypeInfo;
use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;


// U128 wrapper for sending between C# and Rust
// NOTE: u128 normally is comprised of 1 value, but u128 is not C palatable and as such can't be transferred
//  between C# and Rust. The solution here is to split the two halves of the u128 into two u64 values that
//  are C palatable. 
//  -When sent from one side to another, a u128 value is split into the two halves and bitwise ORed into the
//      two halves of this struct.
//  -When recieved from another wise, the first and second halves are ORed at different points on a u128 value
//      initialized at 0. first_half -> bits 0-63 and second_half -> bits 64-127. This builds the u128 back up
//      from its parts.
//  There isn't an easy way to transfer a value this big between the two sides, but doing so is O(1) each time.
//      For now, this is the fastest way to go between the two without risking using slices.
#[ffi_type]
#[repr(C)]
pub struct CustomU128{
    pub first_half: u64,
    pub second_half: u64,
}
// Holds two values, 1 u64 and 1 u128 representing a tuple
#[ffi_type]
#[repr(C)]
pub struct U128U64Tuple{
    pub value1: CustomU128,
    pub value2: u64,
}
#[ffi_type]
#[repr(C)]
pub struct U128U64TupleSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}



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




// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(IndexReaderWrapper))
            .register(extra_type!(IndexWriterWrapper<u8>))
        .inventory()
    }
}