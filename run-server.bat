@echo off
title Whiteboard Server
cd /d "%~dp0Server"
echo ============================================================
echo   Starting Whiteboard SERVER (SignalR hub + admin feed)
echo   Listening on http://0.0.0.0:5000
echo ============================================================
dotnet run
echo.
echo (Server stopped) Press a key to close...
pause >nul
