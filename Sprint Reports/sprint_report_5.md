# Sprint x Report (8/26/21 - 9/24/2021)

## What's New (User Facing)
 * Bug Fix 1 Got async running from C# to rust. 

## Work Summary (Developer Facing)
A lot of the work spent on this sprint was trying to get async methods to run when called from C# into Rust code. This was a setback that we were not expecting, and it took us longer to fix than anticipated. This was partly because our mentor from Dell with the most Rust experience had to travel for a couple of weeks, as was not able to troubleshoot with us. With his help we were able to get an async call running when called from C#. We also worked on other features like documentation, researching the best way to pass byte arrays from C# into rust, and implementing a controller client to control pravega steams from the C# side.

## Unfinished Work
As a result of not being able to get async working, we were delayed on our original plans to implement the byte and event modules completed. This is because a majority of the Pravega code relies on async functionality in most function calls, from creating structs to actual functionality. With the async working now, we hope to make decent headway on the other modules from now on.

## Completed Issues/User Stories

 * [Update unit tests that use time in nanoseconds in C# Wrapper](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/66)
 * [Create a solution for async methods to be accesesible in the C# codebase](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/59)
 
 ## Incomplete Issues/User Stories

 * [Update unit tests that use time in nanoseconds](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/62)
    * Partiallity completed but still need to implement the Rust updated measurement calculations.
 * [Update Client Factory public methods and classes with xml documentatio](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/61)
    * Because we needed asynchronous for all other modules to work, our team has been more focused on that then secondary issues.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
  * [Rust to C# pointer example](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/Rust%20to%20C%23%20pointer%20Example/Program.cs)
 * [Rust to C# pointer example rust side](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/Rust%20to%20C%23%20pointer%20Example/lib.rs)
 * [AuthWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/AuthWrapper)
 * [ByteWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/ByteWrapper)
 * [ConfigWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/ConfigWrapper)
 * [ConnectionPoolWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/ConnectionPoolWrapper)
 * [ControllerClientWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/ControllerClientWrapper)
 * [EventWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/EventWrapper)
 * [IndexWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/IndexWrapper)
 * [RetryWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/RetryWrapper)
 * [SharedWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/SharedWrapper)
 * [UtilityContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/UtilityWrapper)
## Retrospective Summary
Here's what went well:
  * While roadblocked by async problems, we were still able to keep the project moving along with other work.
 
Here's what we'd like to improve:
   * Communication with our client in resolving issues.
  
Here are changes we plan to implement in the next sprint:
  * There isn't a lot we can change on our end as communication hinges on their schedule, which is out of our control. However, we still plan on utilizing Slack whenever possible and schedule Zoom meetings to communicate with our clients when issues arise and we need recommendations on software design.