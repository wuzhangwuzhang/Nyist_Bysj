rmdir /q Bin\Android
mkdir Bin\Android

:: set your own Unity path
set unity="E:\Unity\Editor\Unity.exe"
:: -debug or -release
set debugParam=-debug

set projectPath=%~dp0
::call Environment.bat

echo "Start Build Unity to Apk"
COLOR 0F

%unity% -batchmode -projectPath %projectPath% -executeMethod CommandBuild.PreBuild %debugParam% -quit -logFile ./PreBuild.log
%unity% -batchmode -projectPath %projectPath% -executeMethod CommandBuild.Build %debugParam% -android -quit -logFile ./BuildApk.log


echo "End Build,please see log PreBuild.log and BuildApk.log"