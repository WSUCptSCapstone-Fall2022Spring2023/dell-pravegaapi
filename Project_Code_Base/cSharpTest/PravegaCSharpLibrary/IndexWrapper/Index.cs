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
using Pravega.Utility;
#pragma warning restore 0105

namespace Pravega.Index
{
    public class IndexWriter : RustStructWrapper {
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "IndexWriter";
        }
    }

    public class IndexReader : RustStructWrapper{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
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