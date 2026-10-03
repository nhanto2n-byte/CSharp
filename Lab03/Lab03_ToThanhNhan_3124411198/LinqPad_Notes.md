# Ghi chú thử bài trên LINQPad

Tài liệu thực hành yêu cầu làm thử trên LINQPad trước rồi tạo Console App và áp dụng code trên VS Code. fileciteturn0file0L11-L15

## Cách thử
1. Mở LINQPad.
2. Chọn `C# Program`.
3. Dán phần code của từng phương thức vào query để kiểm tra từng bài.
4. Chạy bằng F5.
5. Kiểm tra kết quả với dữ liệu trong đề.
6. Sau khi đúng, chuyển code sang project `BaiThucHanhLINQ` trong VS Code.

## Ghi chú kỹ thuật
- `Where`: lọc phần tử theo điều kiện.
- `Select`: biến đổi mỗi phần tử sang giá trị/đối tượng mới.
- `OrderBy`, `OrderByDescending`, `ThenBy`: sắp xếp.
- `Count`, `Sum`, `Average`, `Min`, `Max`: thống kê.
- `Distinct`: loại bỏ giá trị trùng.
- `GroupBy`: phân nhóm.
- `Join`: ghép hai nguồn theo khóa.
- `GroupJoin` + `DefaultIfEmpty`: mô phỏng left outer join.
- `FirstOrDefault`: lấy phần tử đầu tiên thỏa điều kiện, hoặc null nếu không có.

## Lưu ý khi chạy
Code dùng `Console.OutputEncoding = System.Text.Encoding.UTF8` để hiển thị tiếng Việt rõ hơn khi chạy Console.

Phần 6.2c dùng `Concat` để bổ sung các môn chưa có hệ sau khi lấy toàn bộ hệ và các môn tương ứng. Vì `DS_He()` có hệ `QT` nhưng không có môn, còn `DS_Mon()` có môn `XYZ` với `He = ""`, đây là hai trường hợp cần thể hiện.

## Đối chiếu yêu cầu đề
Đề yêu cầu mỗi bài thành một phương thức riêng và kết quả phải được in rõ ràng, không viết cứng kết quả. fileciteturn0file0L11-L15
