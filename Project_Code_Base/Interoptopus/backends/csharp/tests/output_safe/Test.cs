using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using My.Company;
using My.Company.Common;
using Xunit;
using Array = My.Company.Array;

namespace interop_test
{
    public class GeneralTests
    {
    
        [Fact]
        public void array_1()
        {
            var array = new Array {data0 = 5};
            Assert.Equal(5, Interop.array_1(array));
        }

        [Fact]
        public void pattern_ffi_slice_delegate()
        {
            Interop.pattern_ffi_slice_delegate(delegate (Sliceu8 x0)
            {
                Assert.Equal(x0.Count, 10);
                Assert.Equal(x0[0], 0);
                Assert.Equal(x0[5], 5);

                // Test IEnumerable using LINQ 
                var arr = x0.ToArray();
                Assert.Equal(arr.Length, 10);
                Assert.Equal(arr[0], 0);
                Assert.Equal(arr[5], 5);

                return x0[0];
            });
        }
        
        [Fact]
        public void pattern_ffi_slice_3()
        {
            var data = new byte[100_000];
            
            Interop.pattern_ffi_slice_3(data, x0 =>
            {
                x0[1] = 100;
            });
            
            Assert.Equal(data[0], 1);
            Assert.Equal(data[1], 100);
        }


        [Fact]
        public void pattern_ffi_option_nullable()
        {
            var t = new Inner();
            OptionInner someOpt = OptionInner.FromNullable(t);
            Inner? nullableOpt = someOpt.ToNullable();
            Assert.True(nullableOpt.HasValue);

            OptionInner someOpt2 = OptionInner.FromNullable(null);
            Inner? nullableOpt2 = someOpt2.ToNullable();
            Assert.False(nullableOpt2.HasValue);
        }

        [Fact]
        public void pattern_service_generated()
        {
            var simpleService = SimpleService.NewWith(123);
            var b = new byte[] { 1, 2, 3 } ;
            
            simpleService.MethodMutSelfFfiError(b);
            var s1 = simpleService.ReturnString();
            var s2 = simpleService.ReturnString();

            var sliceMut = simpleService.ReturnSliceMut();
            sliceMut[0] = 44;
            
            var slice = simpleService.ReturnSlice();
            Assert.Equal(slice.Count, 123);
            Assert.Equal((int) slice[0], 44);
            Assert.Equal((int) slice[1], 123);
            
            uint value = 123;
            var lt = SimpleServiceLifetime.NewWith(ref value);
            var s3 = lt.ReturnStringAcceptSlice(System.Array.Empty<byte>());
            var s4 = lt.ReturnStringAcceptSlice(System.Array.Empty<byte>());
        }

    }
}