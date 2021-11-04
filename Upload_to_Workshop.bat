set SEInstallDir="C:\steam\steamapps\common\SpaceEngineers"
for %%I in (.) do set ParentDirName=%%~nxI
%SEInstallDir%\Bin64\SEWorkshopTool.exe push --appdata "%ParentDirName%" --exclude-ext .bat .psd .fbx .hkt .xml
pause