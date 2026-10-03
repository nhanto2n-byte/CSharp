# Lab02 - Thực hành 2 (Lập trình hướng đối tượng C#)

Sinh viên:
- Trần Lê Gia Bảo - 3124411038
Môn: Ngôn ngữ lập trình C#

> Đề bài chính của project này là: **NNLTCS - Thực Hành 02 - Lập trình hướng đối tượng CSharp.pdf**.
> Đây là phần OOP đã được triển khai đầy đủ trong thư mục `ThucHanh2/`, và `Program.cs` đã được chỉnh để chạy trực tiếp vào menu của phần này.

## 1. Cấu trúc project

```
OOP/
├── Lab02_ThucHanh.csproj
├── Program.cs                  # Chạy trực tiếp menu Thực hành 2
├── README.md
├── ThucHanh1/                 # Giữ lại để tham khảo / lịch sử bài tập cơ bản
│   └── ...
└── ThucHanh2/
    ├── MenuThucHanh2.cs       # Menu tổng hợp 1.x, 2.x, 3.x
    ├── Bai1_1.cs ... Bai3_6.cs
    ├── Person.cs
    ├── Point.cs
    ├── PhanSo.cs
    ├── DonThuc.cs
    ├── DaySo.cs
    ├── Mang2Chieu.cs
    ├── DaThuc.cs
    ├── DayPhanSo.cs
    ├── PersonList.cs
    ├── ArrayPoint.cs
    ├── NhanVien.cs
    ├── PhongBan.cs
    ├── SinhVien.cs
    ├── SapXepTongQuat.cs
    ├── ConsoleMenu.cs
    ├── ThiSinh.cs
    └── ...
```

Project hiện tại tập trung vào phần Thực hành 2, vì vậy `Program.cs` không còn mở menu chọn giữa Thực hành 1 và Thực hành 2 nữa. Nó sẽ chạy trực tiếp vào `MenuThucHanh2.Hien()` để người dùng làm bài OOP ngay.

## 2. Cách chạy

```bash
dotnet run
```

Sau khi chạy, chương trình sẽ hiển thị ngay menu của Thực hành 2 với các bài:
- 1.x: thiết kế lớp cơ bản
- 2.x: lớp nâng cao
- 3.x: kế thừa và đa hình

Chọn số bài tương ứng để chạy, chọn `0` để quay lại hoặc thoát.

## 3. Nhật ký làm việc (changelog)

- Xác định rõ đây là project OOP, nên phần chính cần chạy là `ThucHanh2` chứ không phải menu lạ giữa hai phần chưa thống nhất.
- Sửa `Program.cs` để khởi động trực tiếp `MenuThucHanh2.Hien()` thay vì hiển thị menu lựa chọn không còn cần thiết.
- Tổng hợp các bài OOP thành menu 3 nhóm rõ ràng: `1.x`, `2.x`, `3.x`.
- Tách dữ liệu và hành vi theo hướng đối tượng, với các class riêng như `Person`, `Point`, `PhanSo`, `DonThuc`, `DaySo`, `Mang2Chieu`, `SinhVien`, `NhanVien`, `ThiSinh`, ...
- Duy trì `ThucHanh1/` để có thể tham khảo hoặc bổ sung thêm nếu cần, nhưng không làm trùng kế hoạch chính của project.
- Giữ lại việc menu có thể mở rộng ở tầng sau mà không phải phá cấu trúc sẵn có của Thực hành 2.
- Hoàn chỉnh README để phản ánh đúng trạng thái thực tế của project hiện tại, thay vì mô tả một project còn đang ở trạng thái chưa triển khai.

## 4. Tóm tắt nội dung thực hành 2

| Nhóm | Nội dung chính |
|---|---|
| 1.x | Thiết kế lớp cơ bản: `Person`, `Point`, `PhanSo`, `DonThuc` |
| 2.x | Lớp nâng cao: `ArrayPoint`, `PersonList`, `DaySo`, `Mang2Chieu`, `DaThuc`, `DayPhanSo` |
| 3.x | Kế thừa, đa hình, sắp xếp: `SinhVien`, `NhanVien`, `ThiSinh`, `ConsoleMenu`, `SapXepTongQuat` |

## 5. Lưu ý

- `Program.cs` hiện đang chạy luôn vào phần Thực hành 2 để đảm bảo tính thống nhất của project.
- `ThucHanh1` vẫn còn tồn tại như dữ liệu lịch sử hoặc tham khảo, nhưng không phải là phần chính được chạy mặc định.
- Nếu cần, có thể tiếp tục bổ sung thêm README theo từng bài OOP hoặc chuẩn bị file báo cáo Word cuối kì cho đúng qui định của giảng viên.
