# Contributing
If you want to contribute to this LiveSplit component, feel free to fork the repository and submit a pull request! Here's how you can do that.

## Compiling
In order to compile any work you do into a useable assembly, you'll need to setup your project properly.

If you are *not* using Visual Studio 2017, you will need to download the [.NET Framework 4.6.1 Targeting Pack](https://www.microsoft.com/en-us/download/details.aspx?id=49978).
LiveSplit uses this version of .NET - this isn't a choice made strategically by this project in particular.

Download the [latest version](https://github.com/LiveSplit/LiveSplit/releases/latest) of LiveSplit. You need three DLLs from the folder you download:

  * LiveSplit.Core
  * SpeedrunComSharp
  * UpdateManager

Copy these DLLs to a nice place on your computer (for example, in C:/lib) and
[add them as references](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-add-or-remove-references-by-using-the-reference-manager?view=vs-2022) in Visual Studio.
You may need to delete the three missing references and then add your local ones.

## Testing your changes
Change your build target to Release, Any CPU, then build. Your DLL will be found in the obj/Release folder of the solution!

Copy this DLL to the Components folder in LiveSplit in order to be able to use it.

## Submitting a Pull Request
Before you submit a pull request with code-related changes, ensure that you:
  * Test your changes thoroughly
  * Check that code is properly formatted
  * Change the version of CollectorFactory and add an entry to the update xml file with this new version and summaries - follow [Semantic Versioning](https://semver.org/)
  * Build your solution and copy your generated DLL to the Components folder in the project's folder
  * Commit only the files you have changed to your branch. Visual Studio loves to generate unnecessary files - if it's not a .cs, .dll, or .xml, you likely don't need it
  * Add a descriptive commit message and body (if applicable).
