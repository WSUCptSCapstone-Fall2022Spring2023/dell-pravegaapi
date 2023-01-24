///
/// File: Index.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the index module. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
#pragma warning restore 0105

namespace Pravega.Index
{
    public class IndexWriter : RustStructWrapper {
        public virtual string Type(){
            return "IndexWriter";
        }
    }

    public class IndexReader : RustStructWrapper{
        public virtual string Type(){
            return "IndexReader";
        }
    }


    

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SegmentMetadataClientHolder
    {
        byte inner;
    }

    
}