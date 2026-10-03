using System;
using System.Linq;

namespace BaiThucHanhLINQ;

/// <summary>
/// Bài 4.1 - Xây dựng nguồn dữ liệu đối tượng.
/// Tạo và kiểm tra danh sách List&lt;MonHoc&gt; từ DuLieu.DS_Mon().
/// </summary>
static class Bai41
{
    public static void Run()
    {
        Console.WriteLine("\n===== BÀI 4.1 - XÂY DỰNG NGUỒN DỮ LIỆU ĐỐI TƯỢNG =====");

        // Lấy danh sách môn học do DuLieu.DS_Mon() tạo ra.
        var dsMon = DuLieu.DS_Mon();

        Console.WriteLine("4.1a - Số môn học trong danh sách: " + dsMon.Count);
        Console.WriteLine("4.1b - Danh sách môn học:");
        Console.WriteLine("Mã môn | Tên môn | Hệ | Số tiết");

        // foreach chỉ dùng để xuất kết quả; dữ liệu được xây dựng bởi List<MonHoc>.
        foreach (var mon in dsMon)
        {
            Console.WriteLine(
                $"{mon.MaMon,-6} | {mon.TenMon,-48} | {mon.He,-3} | {mon.SoTiet,3}");
        }

        // Kiểm tra một thuộc tính của nguồn dữ liệu để phần Bài 4 dễ quan sát.
        var monDauTien = dsMon.First();
        Console.WriteLine($"4.1c - Môn đầu tiên: {monDauTien.MaMon} - {monDauTien.TenMon}");
    }
}
