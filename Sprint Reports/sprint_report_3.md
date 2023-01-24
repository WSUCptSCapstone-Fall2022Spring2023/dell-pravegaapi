<<<<<<< HEAD
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
=======
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

 * URL of issue 1
 * URL of issue 2
 * URL of issue n

 Reminders (Remove this section when you save the file):
  * Each issue should be assigned to a milestone
  * Each completed issue should be assigned to a pull request
  * Each completed pull request should include a link to a "Before and After" video
  * All team members who contributed to the issue should be assigned to it on GitHub
  * Each issue should be assigned story points using a label
  * Story points contribution of each team member should be indicated in a comment
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * URL of issue 1 <<One sentence explanation of why issue was not completed>>
 * URL of issue 2 <<One sentence explanation of why issue was not completed>>
 * URL of issue n <<One sentence explanation of why issue was not completed>>
 
 Examples of explanations (Remove this section when you save the file):
  * "We ran into a complication we did not anticipate (explain briefly)." 
  * "We decided that the feature did not add sufficient value for us to work on it in this sprint (explain briefly)."
  * "We could not reproduce the bug" (explain briefly).
  * "We did not get to this issue because..." (explain briefly)

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
>>>>>>> 8705520bf0794b3f35b7148c147f899247d6bcfe
   * Designing a schedule that accounts for possible changes and delays. Keeping it tight is setting us up for disapointment.