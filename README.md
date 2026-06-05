# Lab 06 — Whiteboard cộng tác realtime

## Thành viên
- Trà Chí Chung — 24520229
- Lê Nam Khanh — 24520783

## Công nghệ
- C# / .NET 8
- ASP.NET Core + SignalR (server realtime)
- WinForms (client & admin)
- SMTP – System.Net.Mail (email cảnh báo)

## Cấu trúc thư mục
```
NT106.Q22.ANTT.1_Lab06/
├─ Server/        # SignalR hub + REST /admin/state  (ASP.NET Core, net8.0)
├─ Drawing_App/   # Client vẽ bảng                   (WinForms, net8.0-windows)
├─ Admin/         # Dashboard theo dõi               (WinForms, net8.0-windows)
└─ *.bat          # Script chạy nhanh
```

## Hướng dẫn chạy
Yêu cầu: Windows + .NET 8 SDK.

Dùng file `.bat` (mở lần lượt):
1. `run-server.bat` — khởi động server
2. `run-admin.bat` — mở dashboard admin
3. `run-4-clients.bat` (+ `run-1-client.bat` cho client thứ 5)

Hoặc dùng lệnh:
```bash
cd Server && dotnet run
cd Admin && dotnet run
cd Drawing_App/Client && dotnet run
```
Tại màn hình Connect, nhập `localhost:5000` (hoặc `IP:5000` nếu chạy qua LAN).