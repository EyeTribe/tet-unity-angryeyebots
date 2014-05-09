AngryEyeBots Unity Sample for The Eye Tribe Tracker
====
<p>

Introduction
----

This is a modified version of the well known [Unity](http://unity3d.com/) sample project 'AngryBots'. This variant is called 'AngryEyeBots' and uses eye tracking input from The Eye Tribe Tracker to add unique features to the game.

![](http://theeyetribe.com/github/angryeyebots_1.png)

The gameplay of the sample has been altered to use gaze input. 

- Avatar is moved using *arrow keys* or *joystick*
- Firing is done using *ctrl* or *joystick btn 0*
- Aiming is done using *eye coordinates* (instead of normal mouse position) 

**Note**<br>
The game requires a calibrated EyeTribe Server. The EyeTribe Server should therefore be calibrated before launching this sample.

**Improvements**<br>
In its current state, this sample does _not_ resolve all issues related to using eye input for game control.  Since eye gaze can jump from one corner of a screen to the opposite in a very short time, special input handling may be needed. The Eye Tribe encourages developers to improve the overall experience by introducing they own solutions to these issues.


Modifications
----

The following files from the original sample have been modded:

- Assets/Scripts/Modules/TriggerOnMouseOrJoystick.js
- Assets/Scripts/Movement/PlayerMoveController.js
- Assets/Scripts/Movement/Joystick.js
- Assets/AngryBots.unity


Changes to the original script files are tagged with /* @TheEyeTribe */
<br/>
<br/>
The following scripts were added to the sample:

- Assets/Standard assets/GazeAngryBotsWrap.cs
- Assets/Standard assets/GazeDataUtils.cs
- Assets/Standard assets/UnityGazeUtils.cs

Minor change in *Build Settings* was required to use [EyeTribe C# SDK](https://github.com/EyeTribe/tet-csharp-client). *Player Settings -> Windows -> Other Settings -> Api Compatibility Level* must be set to .NET 2.0


Dependencies
----

This sample has been developed in Unity 4.3.3 and uses the [EyeTribe C# SDK](https://github.com/EyeTribe/tet-csharp-client).

For Windows desktop builds using touch, the [TouchScript](http://interactivelab.github.io/TouchScript/) framework is used.


Build
----

To build for regular Windows, open project in [Unity](http://unity3d.com/) and build for Windows OS.

To build for touch enabled Windows devices (e.g. Surface Pro), open project in [Unity](http://unity3d.com/), go to *Player Settings -> Windows -> Other Settings -> Scripting Define Symbols* and write custom symbol **UNITY\_WIN\_TOUCH**. Then build for WIndows OS.

Note that the EyeTribe Server currently supports Windows 7 and newer. Support for other platforms will be added in the future.


FAQ
----

Should question arise, do not hesitate to post them on [The Eye Tribe Forum](http://theeyetribe.com/forum/).


Changelog
----

0.9.34 (2014-05-09)

- Updated C# SDK
- Added touch support for Windows using TouchScript 

0.9.26 (2014-02-03)

- Initial release
