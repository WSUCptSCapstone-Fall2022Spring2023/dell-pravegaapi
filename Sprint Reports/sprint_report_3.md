# Sprint Report 3

# Sprint x Report (8/26/21 - 9/24/2021)

## What's New (User Facing)
 * Feature 1 Finished up testint Rust/Pravega Component Transfer
 * Feature 2 Created Code Skeleton for code base
 * Feature 3 Started Transferring/Wrapping ClientFactory

## Work Summary (Developer Facing)
At the end of this sprint our biggest accomplishemnts were completing testing, setting up a code skeleton for our future code, and starting to wrap ClientFactory. We have made decent progress in wrapping all of the rust components that are needed for a user to use the ClientFactory object in their code.

## Unfinished Work
We were hoping to complete ClientFactory entirely, but we were unable to by the end of this sprint. This is because we recieved feedback from our clients that rather than manually wrapping every aspect, we should find a way to pass raw data into C# to hold a rust object. We have begun researching the best way to accomplish this and our current idea is to pass pointers from rust to C#.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * [Feature Testing 8](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/33)
 * [Feature Testing 9](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/34)
 * [Client Factory Components](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/42)
 * [Final Project Report](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/43)
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * [Code Skeleton Setup](https://github.com/WSUCptSCapstone-Fall2022Spring2023/dell-pravegaapi/issues/41)
   * Had to pivot our approach for transfering structs which caused our team to be backed up on issues.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [Utility.cs](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/UtilityWrappers/Utility.cs)
 * [lib.rs](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/UtilityWrappers/lib.rs)
 * [SharedWrapper](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/SharedWrapper)
 * [ConnectionPoolWrapper](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/ConnectionPoolWrapper)
 * [ClientFactoryWrapper](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/ClientFactoryWrapper)
 * [AuthWrapper](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/AuthWrapper)
 * [RetryWrapper](https://github.com/dell-pravegaapi/Project_Code_Base/CSharpWrapper/RetryWrapper)

## Retrospective Summary
Here's what went well:
  * Good communication
  * Kept each other informed and on the same page as issues arose
  * Many struct components were completed.
 
Here's what we'd like to improve:
   * We fell a bit behind on schedule this sprint due to the design needing to be changed. We weren't ready for a change like it and so it's taken a bit of adjusting.
   * Our ability to adapt to new changes.
   * Pushing for more feedback to lower the chances of design being changed.
  
Here are changes we plan to implement in the next sprint:
   * Confirm with our clients more during our weekly meetings what their thoughts are so we are on the same page.
   * Designing a schedule that accounts for possible changes and delays. Keeping it tight is setting us up for disapointment.