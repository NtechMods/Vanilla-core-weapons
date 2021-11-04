set SEInstallDir="C:\Steam\steamapps\common\SpaceEngineers"
for %%I in (.) do set ParentDirName=%%~nxI
%SEInstallDir%\Bin64\SEWorkshopTool.exe push --mods "%ParentDirName%" --exclude-ext .bat .gif .psd .fbx .hkt .xml .blend .blend1 .mp4
pause