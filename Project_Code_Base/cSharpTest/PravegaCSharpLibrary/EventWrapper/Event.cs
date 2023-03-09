///

/// File: Index.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the index module. Implements the C# equivalent of the Rust wrapper structs

/// File: Event.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the event module. Implements the C# equivalent of the Rust wrapper structs

#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega.Shared;
using Pravega.Utility;
#pragma warning restore 0105

namespace Pravega.Event
{

    // ***** Wrapper for EventWriter *****

    public class EventWriter : RustStructWrapper {

        internal EventWriter(ScopedStream s)
        {
            this.RustStructPointer = IntPtr.Zero;
        }
    }


    // ***** Wrapper for EventReader *****

    public class EventReader : RustStructWrapper{

        internal EventReader(ScopedStream s)
        {
            this.RustStructPointer = IntPtr.Zero;
        }

    }


    

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SegmentMetadataClientHolder
    {
        byte inner;
    }

    

      

    // ***** Wrapper for ReaderGroup *****
    public class ReaderGroup : RustStructWrapper{

        internal ReaderGroup(ScopedStream s)
        {
            this._rustStructPointer = IntPtr.Zero;
        }
    }    

    // ***** Wrapper for ReaderGroupConfig *****
    public class ReaderGroupConfig : RustStructWrapper{

        internal ReaderGroupConfig(ScopedStream s)
        {
            this._rustStructPointer = IntPtr.Zero;
        }
    } 

}