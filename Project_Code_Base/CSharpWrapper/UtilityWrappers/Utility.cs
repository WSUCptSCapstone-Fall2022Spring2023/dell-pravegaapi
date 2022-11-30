///
/// File: Utility.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the topic of strings, tuples, values, and slices. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
#pragma warning restore 0105

namespace Pravega.Utility
{
    
    /////////////////////////////////////////
    /// Value Structs
    /////////////////////////////////////////
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
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomU128
    {
        public ulong first_half;
        public ulong second_half;
    }
    public partial struct CustomU128{

        // Equals
        public override bool Equals(CustomU128 obj)
        {
            // equals for U128
            if (obj.first_half == this.first_half && obj.second_half == this.second_half){
                return true;
            }
            return false;
        }

        // String
        // No easy implementation. After 2 hours of tinkering, C# stores large numbers calculated as x.xxx...Ey where E represents
        //  its 10^y. Because of this, trying to parse through the number as a string for a character isn't possible as it will
        //  return the exponent y. Furthermore, no tricks like using /10 or %10 are possible since the number is too large and in 
        //  testing the rounding will only go up to a certain value less than the maximum u128 value.
    }


    /////////////////////////////////////////
    /// Slice Structs
    /////////////////////////////////////////
    /// Used to hold a slice of Rust strings (Usually a vectory or array)
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomRustStringSlice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    /// Used to hold a slice of Rust strings (Usually a vectory or array)
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomCSharpStringSlice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    ///A pointer to an array of data someone else owns which may be modified.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct U8Slice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    public partial struct U8Slice : IEnumerable<byte>
    {
        public U8Slice(GCHandle handle, ulong count)
        {
            this.slice_pointer = handle.AddrOfPinnedObject();
            this.length = count;
        }
        public U8Slice(IntPtr handle, ulong count)
        {
            this.slice_pointer = handle;
            this.length = count;
        }
        public byte this[int i]
        {
            get
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(typeof(byte));
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                return Marshal.PtrToStructure<byte>(ptr);
            }
            set
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(typeof(byte));
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                Marshal.StructureToPtr<byte>(value, ptr, false);
            }
        }
        public byte[] Copied
        {
            get
            {
                var rval = new byte[length];
                for (var i = 0; i < (int) length; i++) {
                    rval[i] = this[i];
                }
                return rval;
            }
        }
        public int Count => (int) length;
        public IEnumerator<byte> GetEnumerator()
        {
            for (var i = 0; i < (int)length; ++i)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct U16Slice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    public partial struct U16Slice : IEnumerable<ushort>
    {
        public U16Slice(GCHandle handle, ulong count)
        {
            this.slice_pointer = handle.AddrOfPinnedObject();
            this.length = count;
        }
        public U16Slice(IntPtr handle, ulong count)
        {
            this.slice_pointer = handle;
            this.length = count;
        }
        public ushort this[int i]
        {
            get
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(typeof(ushort));
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                return Marshal.PtrToStructure<byte>(ptr);
            }
            set
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(typeof(ushort));
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                Marshal.StructureToPtr<ushort>(value, ptr, false);
            }
        }
        public ushort[] Copied
        {
            get
            {
                var rval = new ushort[length];
                for (var i = 0; i < (int)length; i++)
                {
                    rval[i] = this[i];
                }
                return rval;
            }
        }
        public int Count => (int)length;
        public IEnumerator<ushort> GetEnumerator()
        {
            for (var i = 0; i < (int)length; ++i)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    /*
        Demonstration on converting a string into a u16 slice

        string testString = "test";
        U16Slice test;
        test.slice_pointer = Marshal.StringToHGlobalAnsi(testString);
        test.length = (ulong)testString.Length;
        CustomCSharpString testCustomString = new CustomCSharpString();
        testCustomString.string_slice = test;
        for (ulong i = 0; i < testCustomString.string_slice.length; i++)
        {
            Console.WriteLine((char)testCustomString.string_slice[(int)i]);
        }           
    */

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomRustStringSlice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomCSharpStringSlice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct U128U64TupleSlice
    {
        public IntPtr slice_pointer;
        public ulong length;
    }
    public partial struct U128U64TupleSlice : IEnumerable<Tuple<CustomU128, ulong>>
    {
        public U16Slice(GCHandle handle, ulong count)
        {
            this.slice_pointer = handle.AddrOfPinnedObject();
            this.length = count;
        }
        public U16Slice(IntPtr handle, ulong count)
        {
            this.slice_pointer = handle;
            this.length = count;
        }
        public ushort this[int i]
        {
            get
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(Tuple<CustomU128, ulong>);
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                return Marshal.PtrToStructure<Tuple<CustomU128, ulong>>(ptr);
            }
            set
            {
                if (i >= Count) throw new IndexOutOfRangeException();
                var size = Marshal.SizeOf(Tuple<CustomU128, ulong>);
                var ptr = new IntPtr(slice_pointer.ToInt64() + i * size);
                Marshal.StructureToPtr<Tuple<CustomU128, ulong>>(value, ptr, false);
            }
        }
        public Tuple<CustomU128, ulong>[] Copied
        {
            get
            {
                var rval = new Tuple<CustomU128, ulong>[length];
                for (var i = 0; i < (int)length; i++)
                {
                    rval[i] = this[i];
                }
                return rval;
            }
        }
        public int Count => (int)length;
        public IEnumerator<Tuple<CustomU128, ulong>> GetEnumerator()
        {
            for (var i = 0; i < (int)length; ++i)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }



    /////////////////////////////////////////
    /// Tuple Structs
    /////////////////////////////////////////
    /// <summary>
    ///     Helper struct that helps transfer tuples containing u16 values between Rust and C# as this struct is C palatable.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct U16Tuple
    {
        ushort value1;
        ushort value2;
    }



    /////////////////////////////////////////
    /// String Structs
    /////////////////////////////////////////
    /// <summary>
    ///     Helper struct that helps transfer strings between Rust and C# as this struct is C palatable. Represents a UTF-16 C# String
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomCSharpString
    {
        public uint capacity;
        public U16Slice string_slice;
    }
    /// <summary>
    ///     Helper struct that helps transfer strings between Rust and C# as this struct is C palatable. Represents a UTF-8 Rust String
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct CustomRustString
    {
        public uint capacity;
        public U8Slice string_slice;
    }



    /////////////////////////////////////////
    /// Option Types (similar to C# T? types)
    /////////////////////////////////////////
    /// <summary>
    ///     Option type containing boolean flag and maybe valid data.
    /// </summary> 
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct OptionU16Tuple
    {
        ///Element that is maybe valid.
        U16Tuple t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct OptionU16Tuple
    {
        public static OptionU16Tuple FromNullable(U16Tuple? nullable)
        {
            var result = new OptionU16Tuple();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public U16Tuple? ToNullable()
        {
            return this.is_some == 1 ? this.t : (U16Tuple?)null;
        }
    }

    /// <summary>
    ///     Option type containing boolean flag and maybe valid data.
    /// </summary>     [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Optionf64
    {
        ///Element that is maybe valid.
        double t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct Optionf64
    {
        public static Optionf64 FromNullable(double? nullable)
        {
            var result = new Optionf64();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public double? ToNullable()
        {
            return this.is_some == 1 ? this.t : (double?)null;
        }
    }

    /// <summary>
    ///     Option type containing boolean flag and maybe valid data.
    /// </summary>     [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Optionu64
    {
        ///Element that is maybe valid.
        ulong t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct Optionu64
    {
        public static Optionu64 FromNullable(ulong? nullable)
        {
            var result = new Optionu64();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public ulong? ToNullable()
        {
            return this.is_some == 1 ? this.t : (ulong?)null;
        }
    }
    
    ///Option type containing boolean flag and maybe valid data.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct OptionU128U64TupleSlice
    {
        ///Element that is maybe valid.
        U128U64TupleSlice t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct OptionU128U64TupleSlice
    {
        public static OptionU128U64TupleSlice FromNullable(U128U64TupleSlice? nullable)
        {
            var result = new OptionU128U64TupleSlice();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public U128U64TupleSlice? ToNullable()
        {
            return this.is_some == 1 ? this.t : (U128U64TupleSlice?)null;
        }
    }

    ///Option type containing boolean flag and maybe valid data.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Optionu8
    {
        ///Element that is maybe valid.
        byte t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct Optionu8
    {
        public static Optionu8 FromNullable(byte? nullable)
        {
            var result = new Optionu8();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public byte? ToNullable()
        {
            return this.is_some == 1 ? this.t : (byte?)null;
        }
    }
    
    ///Option type containing boolean flag and maybe valid data.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct OptionTxIdWrapper
    {
        ///Element that is maybe valid.
        TxIdWrapper t;
        ///Byte where `1` means element `t` is valid.
        byte is_some;
    }
    public partial struct OptionTxIdWrapper
    {
        public static OptionTxIdWrapper FromNullable(TxIdWrapper? nullable)
        {
            var result = new OptionTxIdWrapper();
            if (nullable.HasValue)
            {
                result.is_some = 1;
                result.t = nullable.Value;
            }

            return result;
        }

        public TxIdWrapper? ToNullable()
        {
            return this.is_some == 1 ? this.t : (TxIdWrapper?)null;
        }
    }

}