///
/// File: Shared.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the shared area. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
using Pravega.Utility;
#pragma warning restore 0105

namespace Pravega.Shared
{
    //  ***** Wrapper for TxId *****
    public class TxId : CustomU128
    {
        public virtual string Type(){
            return "Shared.TxIdWrapper";
        }
    }


    //  ***** Wrapper for WriterID *****
    public class WriterId
    {
        private CustomU128 value;

        // Setter and Getter for value
        public CustomU128 Value{
            get{return this.value;}
            set{this.value = value;}
        }

        public virtual string Type(){
            return "Shared.WriterID";
        }
    }
    /* Originally from pravega-client-rust/shared/src/lib.rs 
        as:
        #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq)]
        pub struct WriterId(pub u128);
    */


    //  ***** Wrapper for DelegationToken *****
    public class DelegationToken : RustStructWrapper{
        public virtual string Type(){
            return "Shared.DelegationToken";
        }
    }


    //  ***** Wrapper for Scope *****
    public class Scope : CustomCSharpString{
        public virtual string Type(){
            return "Shared.Scope";
        }
    }
    

    //  ***** Wrapper for Stream *****
    public class Stream : CustomCSharpString
    {
        public virtual string Type(){
            return "Shared.Stream";
        }
    }


    //  ***** Wrapper for ScopedStream *****
    public class ScopedStream
    {
        private Scope _scope;
        private Stream _stream;

        // Default Constructor
        public ScopedStream(){
            this._scope = new Scope();
            this._stream = new Stream();
        }

        // Setters and Getters
        public CustomCSharpString Scope{
            get{return (CustomCSharpString)this._scope;}
            set{this._scope = value;}
        }
        public CustomCSharpString Stream{
            get{return (CustomCSharpString)this._stream;}
            set{this._stream = value;}
        }

        public virtual string Type(){
            return "Shared.ScopedStream";
        }
    }


    //  ***** Wrapper for ScopedSegment *****]
    public class ScopedSegment
    {
        private Scope _scope;
        private Stream _stream;
        private Segment _segment;

        // Default Constructor
        public ScopedSegment(){
            this._scope = new Scope();
            this._segment = new Segment();
            this._stream = new Stream();
        }

        // Setters and Getters
        public CustomCSharpString Scope{
            get{return (CustomCSharpString)this._scope;}
            set{this._scope = value;}
        }
        public CustomCSharpString Stream{
            get{return (CustomCSharpString)this._stream;}
            set{this._stream = value;}
        }
        public Segment Segment{
            get{return this._segment;}
            set{this._segment = value;}
        }

        public virtual string Type(){
            return "Shared. ScopedSegment";
        }
    }



    //  ***** Wrapper for Segment *****
    public class Segment
    {
        private long _number;
        private TxId _txId;

        // Constructor
        public Segment(){
            this._number = 0;
        }

        // Setters and Getters
        public long Number{
            get{return this._number;}
            set{this._number = value;}
        }

        public virtual string Type(){
            return "Shared.Segment";
        }
    }



    // ***** Wrapper for PravegaNodeUri *****
    public class PravegaNodeUri : CustomCSharpString
    {
        public virtual string Type(){
            return "Shared.PravegaNodeUri";
        }
    }
    /* 
        Originally from pravega-client-rust/shared/src/lib.rs
        as:
        #[derive(From, Shrinkwrap, Debug, Clone, Hash, PartialEq, Eq)]
        pub struct PravegaNodeUri(pub String);
    */
}
