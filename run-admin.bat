@echo off
title Whiteboard Admin
cd /d "%~dp0Admin"
echo ============================================================
echo   Starting ADMIN dashboard (WinForms)
echo   In the app: type the server address then click Connect.
echo ============================================================
dotnet run
echo.
echo (Admin closed) Press a key to close...
pause >nul
