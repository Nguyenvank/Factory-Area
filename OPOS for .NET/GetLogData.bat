
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
rmdir /S /Q ".\LogData"
mkdir ".\LogData"
set > ".\LogData\set.txt"
copy "%TEMPPATH%\Microsoft\Point of Service\Configuration\configuration.xml" ".\LogData"
xcopy /Y /E /I "%TEMPPATH%\EPSON\devicecontrollog" ".\LogData\devicecontrollog\"
xcopy /Y /E /I "%TEMPPATH%\EPSON\portcommunicationservice" ".\LogData\portcommunicationservice\"
copy "%ALLUSERSPROFILE%\EPSON\pos\tm\trace\*.log" ".\LogData"

