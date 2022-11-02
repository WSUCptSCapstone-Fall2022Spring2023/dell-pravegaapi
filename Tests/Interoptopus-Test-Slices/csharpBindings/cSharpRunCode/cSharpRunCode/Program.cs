using System;
using System.Collections;
using System.Runtime.InteropServices;
using My.Company;

namespace cSharpRunCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calls pattern_ffi_slice_1 and gets back size of array
            int[] array1 = new int[] { 1, 2, 3, 4 };
            GCHandle handle = GCHandle.Alloc(array1, GCHandleType.Pinned);
            Sliceu32 e = new Sliceu32();
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                e.data = pointer;
                e.len = (ulong)array1.Length;
            }
            finally
            {
                if (handle.IsAllocated)
                {
                    handle.Free();
                }
            }
            var size = Interop.pattern_ffi_slice_1(e);
            Console.WriteLine("Size -> " + size);
            int index = 2;
            var variable = Interop.get_value_at_index(e, index);
            Console.WriteLine("Variable at index " + index + " is " + variable);
        }
    }
}
