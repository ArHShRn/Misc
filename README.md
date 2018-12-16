# Miscellaneous

## Project build status
[![Build status](https://ci.appveyor.com/api/projects/status/4fqk3addj9j158qa?svg=true)](https://ci.appveyor.com/project/ArHShRn/misc)

## Artifacts

### Argument Displayer
ARGDisplayer.exe will display all arguments used if you "hook" this app onto other things.<br>
It's just a small trick, using foreach(var s in args).<br>

### Custom Maid 3D 2 x64 Launcher
![CM3D2x64_Launcher_Cover](https://fileshk.arhshrn.cn/github/miscellaneous/CM3D2x64_Launcher/CM3D2x64_Launcher_Cover.jpg)
CM3D2x64_Launcher.exe will auto-detect environment and launch CM3D2 conveniently and silently.<br>

#### 1.Auto-detect launch directory and Registry status, supporting multiple CM3D2 versions.
**The launcher supports to launch between multiple versions of CM3D2.**<br>
**First of all, the launcher must launch in the directory of CM3D2.**<br>
Once it successfully launched, it will search for current CM3D2 installation path in Registry
and compares it with the current launching directory.<br>
**If the current launching directory = Registry installation path,** the launcher will next try to launch CM3D2 with NTLEA.<br>
**If the current launching directori differs from Registry installation path,** the launcher will **change Registry installation path
to current launching path** and then try to launch current version of CM3D2 with NTLEA
![Different_Dir](https://fileshk.arhshrn.cn/github/miscellaneous/CM3D2x64_Launcher/Different_Dir.png)

#### 2.Auto-detect NTLEA DLL status
After 1., the launcher will try to launch CM3D2x64.exe with NTLEA by checking NTLEA's Registry keys.<br>
**If NTLEA is not registered at the system,** a notification will be shown to guide you to register NTLEA to the system.<br>
![No_NTLEA](https://fileshk.arhshrn.cn/github/miscellaneous/CM3D2x64_Launcher/No_NTLEA.jpg)
Please follow the guide to register NTLEA to your system Registry.<br>
A picture below will show you the way to register it while the notification given by the launcher is just words.<br>
![NTLEA_pos](https://fileshk.arhshrn.cn/github/miscellaneous/CM3D2x64_Launcher/NTLEA_pos.jpg)

### Log Helper
This item is currently under construction.
![result](https://fileshk.arhshrn.cn/github/miscellaneous/ArLib_Logger/result.jpg)