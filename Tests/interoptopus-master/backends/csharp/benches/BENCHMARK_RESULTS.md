
# FFI Call Overheads

The numbers below are to help FFI design decisions by giving order-of-magnitude estimates how
expensive certain constructs are.

## Notes

- Times were determined by running the given construct N times, taking the elapsed time in ticks,
and computing the cost per 1k invocations.

- The time of the called function is included.

- However, the reference project was written so that each function is _minimal_, i.e., any similar
function you wrote, would have to at least as expensive operations if it were to do anything sensible with
the given type.

- The list is ad-hoc, PRs adding more tests to `Benchmark.cs` are welcome.

- Bindings were generated with the C# `use_unsafe` config, which dramatically (between 2x and 150x(!)) speeds
  up slice access and copies in .NET and Unity, [see the FAQ for details](https://github.com/ralfbiedert/interoptopus/blob/master/FAQ.md#existing-backends). 

## System

The following system was used:

```
System: i9-9900K, 32 GB RAM; Windows 10
rustc: stable (i.e., 1.53 or later)
profile: --release
.NET: v3.1 (netcoreapp3.1)
```

## Results

| Construct | ns per call |
| --- | --- |
| `primitive_void()` | 7 |
| `primitive_u8(0)` | 8 |
| `primitive_u16(0)` | 8 |
| `primitive_u32(0)` | 8 |
| `primitive_u64(0)` | 8 |
| `many_args_5(0, 0, 0, 0, 0)` | 10 |
| `many_args_10(0, 0, 0, 0, 0, 0, 0, 0, 0, 0)` | 14 |
| `ptr(x)` | 8 |
| `ptr_mut(x)` | 9 |
| `ref_simple(x)` | 8 |
| `ref_option(x)` | 9 |
| `tupled(new Tupled())` | 8 |
| `complex_args_1(new Vec3f32(), ref e)` | 11 |
| `callback(x => x, 0)` | 43 |
| `dynamic_api.tupled(new Tupled())` | 17 |
| `pattern_ffi_option_1(new OptionInner())` | 9 |
| `pattern_ffi_slice_delegate(x => x[0])` | 53 |
| `pattern_ffi_slice_delegate(x => x.Copied[0])` | 85 |
| `pattern_ffi_slice_delegate_huge(x => x[0])` | 110 |
| `pattern_ffi_slice_delegate_huge(x => x.Copied[0])` | 79919 |
| `pattern_ffi_slice_2(short_vec, 0)` | 27 |
| `pattern_ffi_slice_2(long_vec, 0)` | 24 |
| `pattern_ffi_slice_4(short_byte, short_byte)` | 29 |
| `pattern_ascii_pointer_1('hello world')` | 42 |
