# Sprint 4 Report (01/09/2023 - 02/02/2023)

## What's New (User Facing)
 * Feature 1 Implented ClientFactory base class in C#.
 * Feature 2 Refined our approach to passing pointers between Rust and C# code.
 * Feature 3 Gave code the ability to detect .dll file locations and update automatically
 * Feature 4 Implemented basic ClientFactory Functionality
 * Feature 5 Started working on async functions and tests

## Work Summary (Developer Facing)
The work on this sprint laid the foundation for our future work. We finally discovered how to pass pointers to objects from Rust to C#, which is the basis for our code. From that we implemented the ClientFactory class in C# so that it can spawn other classes needed for further functionality. 
We were also able to start working on async functions and starting tests for the final projects.

## Unfinished Work
We started implementing tests and async function capability this sprint, but we will have to roll those over into the next sprint. This is because of the complicated nature of these goals. Async is hard to intermingle with C# because it 
is handled in Rust in a different way than what C# understands. Testing is an ongoing process that we will update during all of our sprints.
## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * [Create code to automatically change directory for build files in Rust](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/51)
 * [Revised .gitignore and cleaned up file structure](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/52)
 * [Update string struct](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/53)
 * [Create ByteWriter & SegmentWrapper Rust Structs](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/54)
 * [Create ClientFactory setup in Rust and C#](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/55)
 * [Create Unit Tests for Client Factory](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/56)
 * [Design a system for pointer sharing](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/57)
 * [Implement all methods starting with "create" for ClientFactoryWrapper](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/58)
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * [Create a solution for async methods to be accesesible in the C# codebase.](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/59)
   * This issue is not fully complete yet due to the complexity of the solutuion needed.
   * We expected the issue may take longer than one sprint to find a solution.
 
## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:

 * [DLL Path Generating Code](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/cSharpTest/PathGen.cs)
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
 * [WireProtocolWrapperContents](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaCSharpLibrary/WireProtocolWrapper)
 * [AuthTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/AuthTests.cs)
 * [ByteTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/ByteTests.cs)
 * [ClientConfigTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/ClientConfig.cs)
 * [ClientFactoryTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/ClientFactory.cs)
 * [ConnectionPoolTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/ConnectionPoolTests.cs)
 * [ControllerClientTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/ControllerClientTests.cs)
 * [EventTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/EventTests.cs)
 * [IndexTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/IndexTests.cs)
 * [PravegaTestsMain](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/PravegaTestsMain.cs)
 * [RetryTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/RetryTests.cs)
 * [SharedTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/SharedTests.cs)
 * [UtilityTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/UtilityTests.cs)
 * [WireProtocolTests](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/tree/main/Project_Code_Base/cSharpTest/PravegaWrapperTestProject/WireProtocolTests.cs)
 
## Retrospective Summary
Here's what went well:
  * We stuck closely to the schedule this time
  * Testing and Non-async methods of Client Factory were completed
 
Here's what we'd like to improve:
   * There's nothing that inherently went wrong besides running into a roadblock that we are working to resolve while moving the rest of the project forward
  
Here are changes we plan to implement in the next sprint:
