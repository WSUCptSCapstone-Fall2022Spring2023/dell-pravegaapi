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
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "EventWriter";
        }

        public EventWriter(ScopedStream s)
        {
            this.RustStructPointer = IntPtr.Zero;
        }
    }


    // ***** Wrapper for EventReader *****

    public class EventReader : RustStructWrapper{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "EventReader";
        }

        public EventReader(ScopedStream s)
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
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ReaderGroup";
        }

        public ReaderGroup(ScopedStream s)
        {
            this._rustStructPointer = IntPtr.Zero;
        }
    }    

    // ***** Wrapper for ReaderGroupConfig *****
    public class ReaderGroupConfig : RustStructWrapper{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ReaderGroupConfig";
        }

        public ReaderGroupConfig(ScopedStream s)
        {
            this._rustStructPointer = IntPtr.Zero;
        }
    } 

}