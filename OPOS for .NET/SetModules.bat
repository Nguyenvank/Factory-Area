
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
copy .\configuration.xml. "%TEMPPATH%\Microsoft\Point of Service\Configuration\"
copy .\pcs.properties "%TEMPPATH%\EPSON\portcommunicationservice\"
attrib -r "%TEMPPATH%\EPSON\portcommunicationservice\pcs.properties"
