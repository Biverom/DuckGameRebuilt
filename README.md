# Duck Game Rebuilt
Private decompilation of DG source to fix bugs, improve compatability, archive source, and add quality of life features for devs and users.

Welcome to the repo, enjoy your stay, please unfuck the code. thanks


### Building on Windows

* Make sure you have [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) installed and have a functioning IDE for C#

Recommended IDE: [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)

* Clone The Repository
```
git clone https://www.github.com/nikled/duckgames
```

* Find the repo folder and launch the .sln file

* In `C:\Program Files (x86)\Steam\steamapps\common`: Do `ctrl+A`, then deselect `DuckGame.exe` by holding `ctrl` and left clicking the file, then do ctrl+C`. And finally, in `DuckGames\bin` do `ctrl+V`

* In the `DuckGames` directory, copy over the following 16 files to `DuckGames\bin`

* Build the project
```
dotnet build
```

* \[OPTIONAL FOR BETTER DEBUGGING\] Set Startup program to the exe produced in the bin

<img src="https://user-images.githubusercontent.com/22122579/182766499-9b46ee7a-1291-4fbc-8c3e-7d7467ab8411.png" width="500">

### Building on GNU/Linux

* add official up to date mono repos from monoproject: https://www.mono-project.com/

* Install the packages `mono-complete` and `msbuild`

<!--* TODO FIX for now just temporarily add the Presentationframework nuget (you may need dotent sdk from microsoft (or you may need to download it fom questionable sources)): https://www.nuget.org/packages/PresentationFramework/ So pretty much just download the DLL and put it in the bin folder.-->

* Copy the following DLLs from root into the bin folder like so:
  ```
  cp System.Memory.dll bin/
  cp System.Buffers.dll bin/
  cp System.Runtime.CompilerServices.Unsafe.dll bin/
  cp System.Speech.dll bin/
  cp PresentationFramework.dll bin/
  ```
  
* In `DuckGame.csproj`, comment out or delete this line (near the bottom) `<PostBuildEvent>Xcopy $(SolutionDir)\deps $(SolutionDir)\bin /E /H /C /I /Y
call "$(SolutionDir)shaders_source\buildshaders.bat</PostBuildEvent>`

* Copy some stuff `cp -rv deps/* bin/`

* Now finally you can run the build command, `msbuild`

_Note: you may get over 200 warnings, but don't worry about those. give yourself a pat on the back. you did it._
