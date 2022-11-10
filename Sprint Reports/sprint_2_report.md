# Sprint 2 Report (10/9/22 - 11/9/22)

## What's New (User Facing)
 * Feature 1 Example code for converting rust to C# with interoptopus added for multiple rust features(See Issues for more detail)
 * Feature 2 Report updated to include Testing Section

## Work Summary (Developer Facing)
The main focus that our team worked on for this sprint was getting more comfortable with interoptopus and seeing how well it can convert different aspects of Rust into C#. We did this by coming up with a list of multiple rust features that are used in the Pravega Rust API already. Then we divided up the work and each person worked on testing different aspects. We also worked on the testing plans portions for the report. We did this by breaking up the sections amongst ourselves and working on them independently.

## Unfinished Work
Not every aspect that we wanted to test was able to be implemented. We tried our best but other schoolwork often came up which caused us to lose progress. We have made plans to address this in the future. As seen below, We have come up with a plan to remedy this so hopefully it won't be an issue for the next sprint report.


## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * [Testing Approach](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/21)
 * [Feature Transfer Testing](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/22)
 * [Feature Transfer Testing 2](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/23)
 * [Feature Transfer Testing 3](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/24)
 * [Add Testing+Acceptance Section to Master Document](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/25)
 * [Feature Transfer Testing 4](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/28)
 * [Feature Transfer Testing 6](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/31)
 * [Feature Transfer Testing 7](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/32)

 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * [Feature Transfer Testing 5](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/29)
   * Testing other features took more time than expected
 * [Feature Transfer Testing 8](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/33)
   * Focusing on completing earlier issue before getting this one completed
 * [Feature Transfer Testing 9](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/34)
   * Understanding how to test certain features taking longer than expected


## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * DeepClone implementation [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Clone_Test/cSharpTest/cSharpTest/Interop.cs)
 * Enum test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Enum_Test/csharpBindings/Interop.cs)
 * Global test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Global_Test/csharpBindings/Interop.cs)
 * Mem Allocation test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Mem_Allocation_Test/cSharpTest/cSharpTest/Interop.cs)
                      * [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Mem_Allocation_Test/cSharpTest/cSharpTest/Program.cs)
 * pub(crate) test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_pub(crate)_Test/cSharpBindings/Interop.cs)
                  * [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_pub(crate)_Test/testing.rs)
                  
 * Serialize test  [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Serialize_Test/cSharpBindings/Interop.cs)
                  * [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Serialize_Test/testing.rs)
 * Snafu and error passing test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Snafu_Test/testing.rs)
                              * [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Snafu_Test/cSharpTest/cSharpTest/Program.cs)
                              * [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Snafu_Test/cSharpTest/cSharpTest/Interop.cs)
 * Tuple test [Interop.cs](https://github.com/dell-pravegaapi/Tests/Rust_Tuple_Test/testing.rs)
 
## Retrospective 
Here's what went well:
  * 1) Staying on top of documentation deadlines
  * 2) Honesty about workload and what each individual is doing
  * 3) Stayed up to date with class content
  * 4) Good communication
 
Here's what we'd like to improve:
   * 1) Staying on top of testing and code deadlines set by the group.
   * 2) Asking the Dell team more questions on Slack as they appear
  
Here are changes we plan to implement in the next sprint:
   * 1) It is up to the individual to plan ahead when their schedule becomes busy to work on code beforehand so that other work does not get in the way of this project.
   * 2) Remind eachother of questions to ask on Slack if it was talked about in a meeting before and keep questions for Zoom meetings in a question backlog for the Dell team.
