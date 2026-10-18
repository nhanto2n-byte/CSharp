# Lab 06 – Tương tác cơ sở dữ liệu (6a, 6b)

**Sinh viên:** Tô Thành Nhân **MSSV:** 31244113198 
**Công nghệ:** C# Windows Forms, ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`), SQL Server.

## Phạm vi bài làm
- **Thực hành 6a:** Form quản lý môn học – thêm, xóa, sửa bảng `MonHoc`, kiểm tra dữ liệu rỗng.
- **Thực hành 6b:** Form quản lý sinh viên và điểm – nạp khóa ngoại vào ComboBox; kiểm tra trùng khóa chính, khóa ngoại, dữ liệu bắt buộc, tuổi sinh viên từ 17 (tính theo năm), điểm trong khoảng 0–10; thêm/xóa/sửa.
- Code có chú thích tiếng Việt, dùng truy vấn có tham số để tránh ghép chuỗi SQL trực tiếp.

## Cách chạy
1. Mở SQL Server Management Studio, chạy toàn bộ `Database/QLSinhVien.sql`.
2. Mở `Lab06_CSDL_Basic.sln` bằng Visual Studio (có workload **.NET desktop development**).
3. Mở `App.config`, chỉnh `Data Source` cho đúng SQL Server trên máy. Mặc định dùng Windows Authentication.
4. Build và chạy project. Chọn chức năng từ cửa sổ chính.
5. Nếu SQL Server của bạn đã có CSDL `QLSinhVien`, hãy sao lưu trước và chỉ chạy phần tạo bảng/dữ liệu phù hợp; script có lệnh tạo mới CSDL.

## Ghi chú về kết quả
Báo cáo nêu các ca kiểm thử cần chạy. Kết quả thực tế phụ thuộc vào SQL Server và dữ liệu trên máy; không đính kèm ảnh chụp giả lập việc chạy chương trình.


## Phần bổ sung theo yêu cầu đầy đủ 06a/06b
- `LopForm.cs`: quản lý lớp; nạp mã khoa vào ComboBox; thêm/sửa/xóa và xử lý lỗi khóa ngoại.
- `ListLopForm.cs`: hiển thị danh sách lớp trên ListView.
- `ListDiemForm.cs`: hiển thị/lọc điểm theo môn học và tính tổng điểm đang hiển thị.
- `CuaHangForm.cs` và `Database/QLCuaHang.sql`: các luồng cốt lõi tạo tồn kho, thêm sản phẩm + tồn kho, tạo hóa đơn, thêm chi tiết có kiểm tra tồn và cập nhật số lượng bán, thanh toán.
- Chạy `QLCuaHang.sql` riêng trong SSMS để tạo database `QLCuaHang`. Cấu hình chuỗi kết nối trong `App.config` vẫn trỏ đến instance SQL Server; các bảng cửa hàng được tham chiếu đầy đủ bằng tên database.
- Bản hiện tại là bài thực hành minh họa. Chưa xác nhận build/chạy trên máy đích; cần kiểm thử thực tế với SQL Server đã cấu hình. Không coi các ca kiểm thử trong báo cáo là kết quả chạy thật.
