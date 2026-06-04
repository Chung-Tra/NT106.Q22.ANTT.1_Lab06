@echo off
REM Launches FOUR whiteboard clients at once (build once, then open 4 windows).
cd /d "%~dp0Drawing_App\Client"
set "EXE=bin\Debug\net8.0-windows\Client.exe"
if not exist "%EXE%" (
    echo Building client...
    dotnet build -c Debug
)
if not exist "%EXE%" (
    echo Build failed - cannot find %EXE%
    pause
    exit /b 1
)
for /L %%i in (1,1,4) do start "" "%EXE%"
echo Opened 4 client windows. (Run run-1-client.bat for the 5th to fill the room.)
