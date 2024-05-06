
echo off

VER|FIND "[Version 5.">NUL
IF %ERRORLEVEL% EQU 0 GOTO XP_PATH
VER|FIND "[Version 6.">NUL
IF %ERRORLEVEL% EQU 0 GOTO VISTA_PATH

:XP_PATH
set TEMPPATH=%ALLUSERSPROFILE%\Application Data
goto COMMAND

:VISTA_PATH
set TEMPPATH=%ALLUSERSPROFILE%
goto COMMAND

:COMMAND
rmdir /S /Q ".\EpsonCopyInstallModules"
mkdir ".\EpsonCopyInstallModules"
copy .\SetModules.bat .\EpsonCopyInstallModules\
copy "%TEMPPATH%\Microsoft\Point of Service\Configuration\configuration.xml" .\EpsonCopyInstallModules\
copy "%TEMPPATH%\EPSON\portcommunicationservice\pcs.properties" .\EpsonCopyInstallModules\
copy %windir%\setup.iss .\EpsonCopyInstallModules\
