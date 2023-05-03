

# Upgrade Instructions

Tips for solving non-trivial breaking changes when upgrading from previous versions.


### 0.13 → 0.14

- Removed `inventory!` macro
  - You now just write a regular function returning an `Inventory`
  - See the reference project for details 


### 0.12 → 0.13

- Deprecated Python CFFI backend, replace with Python CTypes backend. 
  - Might require changing some invocations. Please see `reference_project.py`. 
- Renamed attributes of `#[ffi_service_method]` once more, no behavior changed: 
  - `wrap` is now `on_panic`
  - `direct` is `return_default`
  - `raw` is `undefined_behavior`


### 0.11 → 0.12

- Changed behavior of `#[ffi_service_method]`
  - `#[ffi_service_method(direct)]` is now `#[ffi_service_method(wrap = "direct")]` 


### 0.10 → 0.11

- C# backend switched constructors to static methods
  - Wherever you used `new Service(x)` now use `Service.NewWith(x)` (or similar).


### 0.9 → 0.10

- C# backend split into `DotNet` and `Unity`. If methods are missing:
  - Add `.add_overload_writer(DotNet::new())` to `Generator`.
  - Consider adding `.add_overload_writer(Unity::new())` when targeting Unity


### 0.8 → 0.9

- Replaced most `pattern!` macros with `#[pattern]` attributes, see individual pattern documentation for details.
- Added type hints support, upgraded minimum supported Python version to 3.7 [no workaround]
