# Sprint 6 Report (03/03/2023 - 04/02/2023)

## What's New (User Facing)
 * Feature 1 Implemented Byte Module for Pravega Client API
 * Feature 2 Began Testing Byte Module
 * Feature 3 Began Working on Event Module

## Work Summary (Developer Facing)
During this sprint we implemented and began testing the byte module. This is the first module that has the ability to read/write to a pravega server, so it is a major milestone. We've also started to test the module, and to begin to write the code for the event module. 

## Unfinished Work
While testing the byte module we made the discovery that for some reason we can spawn a byte writer and write information to the server, but we are unable to create a bytereader and read information back. This has slowed our progress down a bit, as in addition to testing the module for functionality, we are also trying to find the cause of this bug. This bug also affects event readers. Currently we believe that this bug is due to the way that C# imports DLLs. The code will just randomly halt while trying to construct a bytereader on the rust side, and does not even reach the code that we have made.
## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * [Update unit tests that use time in nanoseconds in Rust Wrapper](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/65)
 * [Update unit tests that use time in nanoseconds](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/62)
 * [Create Byte Reader and Writer](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/72)

 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:

 * [Create Event Writer Module](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/70)
    * Currently working on completing the code for this module.
 * [Create Event Reader Module](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/71)
    * We are having problems with how the DLL is being imported.
 * [Update Client Factory public methods and classes with xml documentation](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/71)
    * Our team has been focusing on the codebase but have slowly been working on the documentation.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
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
  * We were in good communication and good understanding with our clients. Our problems were well conveyed and they understood the road blocks and their significance as we were dealing with them.
 
Here's what we'd like to improve:
   * The project's integrity. Testing and implementation has become more unstable
  
Here are changes we plan to implement in the next sprint:
  * We are working on it alongside implementation and will continue to into the next spring.