@echo off
REM Launches ONE whiteboard client.
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
start "" "%EXE%"
