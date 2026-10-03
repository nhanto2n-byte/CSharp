<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Bai21();
        Bai22();
        Bai31();
        Bai32();
        Bai41();
        Bai51();
        Bai52();
        Bai62();

        Console.WriteLine("\n===== HOÀN TẤT LAB 03 =====");
    }

    // =========================================================
    // BÀI 2.1 - TRUY VẤN MẢNG SỐ NGUYÊN
    // =========================================================
    static void Bai21()
    {
        int[] mangSo = { 50, 42, 16, 3, 9, 8, 12, 7, 24, 0 };

        Console.WriteLine("\n===== BÀI 2.1 =====");

        // a. Query Syntax: chia hết cho cả 4 và 3.
        var cauAQuery =
            from x in mangSo
            where x % 4 == 0 && x % 3 == 0
            select x;

        InDay("2.1a - Chia hết cho 4 và 3 (Query Syntax)", cauAQuery);

        // b. Method Syntax: nhỏ hơn hoặc bằng 3.
        var cauBMethod = mangSo.Where(x => x <= 3);
        InDay("2.1b - Nhỏ hơn hoặc bằng 3 (Method Syntax)", cauBMethod);

        // c. Select: số chẵn chia đôi, số lẻ giữ nguyên.
        var cauCQuery =
            from x in mangSo
            select x % 2 == 0 ? x / 2 : x;

        InDay("2.1c - Chẵn chia đôi, lẻ giữ nguyên", cauCQuery);
    }

    // =========================================================
    // BÀI 2.2 - TRUY VẤN MẢNG CHUỖI
    // =========================================================
    static void Bai22()
    {
        string[] mangChuoi =
        {
            "đầu", "lòng", "hai", "ả", "tố", "nga",
            "Thúy", "Kiều", "là", "chị", "em", "là",
            "Thúy", "Vân"
        };

        Console.WriteLine("\n===== BÀI 2.2 =====");

        // a. Có 4 ký tự, sắp xếp tăng dần theo ký tự đầu tiên.
        var cauA =
            mangChuoi
                .Where(x => x.Length == 4)
                .OrderBy(x => x[0]);

        InDay("2.2a - Chuỗi dài 4 ký tự, tăng theo ký tự đầu", cauA);

        // b. Chuyển thành dạng: chữ thường - CHỮ HOA.
        var cauB =
            mangChuoi.Select(x => $"{x.ToLower()} - {x.ToUpper()}");

        InDay("2.2b - Chữ thường - CHỮ HOA", cauB);

        // c. Chứa ký tự 'u' (không phân biệt hoa/thường).
        var cauC =
            mangChuoi.Where(x => x.Contains('u') || x.Contains('U'));

        InDay("2.2c - Có chứa ký tự 'u'", cauC);

        // d. Chọn các phần tử bắt đầu bằng chữ in hoa.
        var cauD =
            mangChuoi.Where(x => x.Length > 0 && char.IsUpper(x[0]));

        InDay("2.2d - Bắt đầu bằng chữ in hoa", cauD);
    }

    // =========================================================
    // BÀI 3.1 - THỐNG KÊ MẢNG SỐ
    // =========================================================
    static void Bai31()
    {
        int[] mangSo = { 50, 42, 12, 3, 9, 8, 1, 50, 3, 42, 85 };

        Console.WriteLine("\n===== BÀI 3.1 =====");

        Console.WriteLine($"3.1a - Tổng số phần tử: {mangSo.Count()}");
        Console.WriteLine($"      Số phần tử chẵn: {mangSo.Count(x => x % 2 == 0)}");
        Console.WriteLine($"      Số phần tử lẻ: {mangSo.Count(x => x % 2 != 0)}");

        Console.WriteLine($"3.1b - Tổng giá trị: {mangSo.Sum()}");
        Console.WriteLine($"      Giá trị lớn nhất: {mangSo.Max()}");
        Console.WriteLine($"      Giá trị nhỏ nhất: {mangSo.Min()}");

        Console.WriteLine($"3.1c - Số giá trị khác nhau: {mangSo.Distinct().Count()}");

        // d. GroupBy theo số dư khi chia cho 5.
        var groups =
            mangSo
                .GroupBy(x => x % 5)
                .OrderBy(g => g.Key);

        Console.WriteLine("3.1d - Phân nhóm theo số dư khi chia cho 5:");
        foreach (var group in groups)
        {
            Console.WriteLine($"  Dư {group.Key}: {string.Join(", ", group)}");
        }
    }

    // =========================================================
    // BÀI 3.2 - THỐNG KÊ MẢNG CHUỖI
    // =========================================================
    static void Bai32()
    {
        string[] monAn =
        {
            "Bún bò Huế", "Hủ tiếu heo", "Bánh canh", "Bánh mì",
            "Nước Cà phê", "Mì quảng", "Cơm tấm", "Nước Chanh dây",
            "Mì xào", "Bún riêu", "Bánh cuốn", "Mì gói",
            "Bún chả", "Hủ tiếu Nam vang"
        };

        Console.WriteLine("\n===== BÀI 3.2 =====");

        int minLength = monAn.Min(x => x.Length);
        int maxLength = monAn.Max(x => x.Length);

        var nganNhat = monAn.Where(x => x.Length == minLength);
        var daiNhat = monAn.Where(x => x.Length == maxLength);

        Console.WriteLine($"3.2a - Độ dài ngắn nhất: {minLength}");
        InDay("      Món ngắn nhất", nganNhat);
        Console.WriteLine($"      Độ dài dài nhất: {maxLength}");
        InDay("      Món dài nhất", daiNhat);

        // b. Nhóm theo từ đầu tiên.
        var groups =
            monAn.GroupBy(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0])
                 .OrderBy(g => g.Key);

        Console.WriteLine("3.2b - Phân nhóm theo từ đầu tiên:");
        foreach (var group in groups)
        {
            Console.WriteLine($"  {group.Key}:");
            foreach (var mon in group)
            {
                Console.WriteLine($"    - {mon}");
            }
        }

        // c. Đếm số món bắt đầu bằng "Bánh".
        int soMonBanh = monAn.Count(x => x.StartsWith("Bánh"));
        Console.WriteLine($"3.2c - Số món bắt đầu bằng 'Bánh': {soMonBanh}");
    }

    // =========================================================
    // BÀI 4.1 - XÂY DỰNG NGUỒN DỮ LIỆU ĐỐI TƯỢNG
    // =========================================================
    static void Bai41()
    {
        Console.WriteLine("\n===== BÀI 4.1 =====");

        var ds = DuLieu.DS_Mon();

        Console.WriteLine("4.1 - Danh sách môn học:");
        foreach (var mon in ds)
        {
            Console.WriteLine(
                $"{mon.MaMon,-6} | {mon.TenMon,-50} | {mon.He,-3} | {mon.SoTiet,3} tiết");
        }
    }

    // =========================================================
    // BÀI 5.1 - TRUY VẤN List<MonHoc>
    // =========================================================
    static void Bai51()
    {
        var ds = DuLieu.DS_Mon();

        Console.WriteLine("\n===== BÀI 5.1 =====");

        // a. Query Syntax.
        var cauA =
            from mon in ds
            where mon.TenMon.StartsWith("Lập trình")
            select mon.TenMon;

        InDay("5.1a - Tên môn bắt đầu bằng 'Lập trình'", cauA);

        // b. Method Syntax + ThenBy.
        var cauB =
            ds.Where(mon => mon.He == "CD")
              .OrderByDescending(mon => mon.SoTiet)
              .ThenBy(mon => mon.MaMon);

        InMonHoc("5.1b - Hệ CD, số tiết giảm dần, mã môn tăng dần", cauB);

        // c. Không phân biệt hoa/thường khi tìm "web".
        var cauC =
            ds.Where(mon => mon.TenMon.Contains("web", StringComparison.OrdinalIgnoreCase))
              .Select(mon => new { mon.TenMon, mon.He });

        Console.WriteLine("5.1c - Tên môn chứa 'web':");
        foreach (var mon in cauC)
        {
            Console.WriteLine($"  {mon.TenMon} | Hệ: {mon.He}");
        }

        // d. Method Syntax.
        var cauD =
            ds.Where(mon => mon.He == "KTV")
              .OrderBy(mon => mon.MaMon);

        InMonHoc("5.1d - Hệ KTV, tăng dần theo mã môn", cauD);
    }

    // =========================================================
    // BÀI 5.2 - THỐNG KÊ List<MonHoc>
    // =========================================================
    static void Bai52()
    {
        var ds = DuLieu.DS_Mon();

        Console.WriteLine("\n===== BÀI 5.2 =====");

        // a.
        Console.WriteLine($"5.2a - Tổng số môn: {ds.Count}");

        // b.
        Console.WriteLine(
            $"5.2b - Số môn bắt đầu bằng 'Lập trình': " +
            $"{ds.Count(mon => mon.TenMon.StartsWith("Lập trình"))}");

        // c.
        Console.WriteLine(
            $"5.2c - Tổng số tiết của hệ KTV: " +
            $"{ds.Where(mon => mon.He == "KTV").Sum(mon => mon.SoTiet)}");

        // d. Tổng số môn của mỗi hệ, kể cả hệ rỗng của XYZ.
        var theoHe =
            ds.GroupBy(mon => mon.He)
              .OrderBy(g => g.Key);

        Console.WriteLine("5.2d - Tổng số môn theo hệ:");
        foreach (var group in theoHe)
        {
            Console.WriteLine(
                $"  Hệ {(string.IsNullOrEmpty(group.Key) ? "(chưa khai báo)" : group.Key)}: {group.Count()}");
        }

        // e.
        var theoSoTiet =
            ds.GroupBy(mon => mon.SoTiet)
              .OrderByDescending(g => g.Key);

        Console.WriteLine("5.2e - Nhóm theo số tiết:");
        foreach (var group in theoSoTiet)
        {
            Console.WriteLine($"  {group.Key} tiết: {group.Count()} môn");
        }

        // f.
        var maxTiet = ds.Max(mon => mon.SoTiet);
        var monMax = ds.Where(mon => mon.SoTiet == maxTiet);

        InMonHoc("5.2f - Môn học có số tiết cao nhất", monMax);

        // g.
        var thongKeHe =
            ds.GroupBy(mon => mon.He)
              .OrderBy(g => g.Key);

        Console.WriteLine("5.2g - Thống kê theo hệ:");
        foreach (var group in thongKeHe)
        {
            Console.WriteLine(
                $"  Hệ {(string.IsNullOrEmpty(group.Key) ? "(chưa khai báo)" : group.Key)}: " +
                $"Số môn={group.Count()}, Tổng tiết={group.Sum(x => x.SoTiet)}, " +
                $"Max={group.Max(x => x.SoTiet)}, Min={group.Min(x => x.SoTiet)}");
        }

        // h.
        Console.WriteLine("5.2h - Các môn được phân nhóm theo hệ:");
        foreach (var group in ds.GroupBy(mon => mon.He).OrderBy(g => g.Key))
        {
            Console.WriteLine(
                $"  Hệ {(string.IsNullOrEmpty(group.Key) ? "(chưa khai báo)" : group.Key)}:");
            foreach (var mon in group.OrderBy(x => x.MaMon))
            {
                Console.WriteLine($"    - {mon.MaMon}: {mon.TenMon}");
            }
        }

        // i.
        Console.WriteLine("5.2i - Các môn được phân nhóm theo số tiết:");
        foreach (var group in ds.GroupBy(mon => mon.SoTiet).OrderBy(g => g.Key))
        {
            Console.WriteLine($"  {group.Key} tiết:");
            foreach (var mon in group.OrderBy(x => x.MaMon))
            {
                Console.WriteLine($"    - {mon.MaMon}: {mon.TenMon}");
            }
        }

        // j. KTV và nhóm theo học phần HP2, HP3, HP4, HP5.
        var hpKtv =
            ds.Where(mon => mon.He == "KTV")
              .GroupBy(mon => mon.MaMon.Length >= 3 ? mon.MaMon[..3] : mon.MaMon)
              .OrderBy(g => g.Key);

        Console.WriteLine("5.2j - KTV, phân nhóm HP2/HP3/HP4/HP5:");
        foreach (var group in hpKtv)
        {
            Console.WriteLine($"  {group.Key}:");
            foreach (var mon in group.OrderBy(x => x.MaMon))
            {
                Console.WriteLine($"    - {mon.MaMon}: {mon.TenMon}");
            }
        }

        // k.
        var heHon40 =
            ds.Where(mon => mon.SoTiet > 40)
              .GroupBy(mon => mon.He)
              .OrderBy(g => g.Key);

        Console.WriteLine("5.2k - Nhóm theo hệ, chỉ lấy môn > 40 tiết:");
        foreach (var group in heHon40)
        {
            Console.WriteLine(
                $"  Hệ {(string.IsNullOrEmpty(group.Key) ? "(chưa khai báo)" : group.Key)}:");
            foreach (var mon in group.OrderBy(x => x.MaMon))
            {
                Console.WriteLine($"    - {mon.MaMon}: {mon.TenMon} ({mon.SoTiet} tiết)");
            }
        }
    }

    // =========================================================
    // BÀI 6.1 + 6.2 - JOIN HAI NGUỒN DỮ LIỆU
    // =========================================================
    static void Bai62()
    {
        var dsMon = DuLieu.DS_Mon();
        var dsHe = DuLieu.DS_He();

        Console.WriteLine("\n===== BÀI 6.2 =====");

        // a. Inner join: chỉ lấy môn có hệ tồn tại trong DS_He.
        var cauA =
            from he in dsHe
            join mon in dsMon on he.MaHe equals mon.He
            select new { he.TenHe, mon.MaMon, mon.TenMon };

        Console.WriteLine("6.2a - Inner Join:");
        foreach (var item in cauA)
        {
            Console.WriteLine($"  {item.TenHe} | {item.MaMon} | {item.TenMon}");
        }

        // b. Left outer join bằng GroupJoin + DefaultIfEmpty.
        var cauB =
            from he in dsHe
            join mon in dsMon on he.MaHe equals mon.He into nhom
            from mon in nhom.DefaultIfEmpty()
            select new
            {
                he.TenHe,
                MaMon = mon?.MaMon ?? "(chưa có)",
                TenMon = mon?.TenMon ?? "(chưa có môn)"
            };

        Console.WriteLine("6.2b - Left outer join:");
        foreach (var item in cauB)
        {
            Console.WriteLine($"  {item.TenHe} | {item.MaMon} | {item.TenMon}");
        }

        // c. Full outer join mô phỏng bằng Union:
        // phần 1 lấy tất cả hệ, phần 2 bổ sung môn chưa có hệ.
        var coHe =
            from he in dsHe
            join mon in dsMon on he.MaHe equals mon.He into nhom
            from mon in nhom.DefaultIfEmpty()
            select new
            {
                MaHe = he.MaHe,
                TenHe = he.TenHe,
                MaMon = mon?.MaMon,
                TenMon = mon?.TenMon
            };

        var monKhongHe =
            from mon in dsMon
            where string.IsNullOrEmpty(mon.He) ||
                  !dsHe.Any(he => he.MaHe == mon.He)
            select new
            {
                MaHe = "(chưa có)",
                TenHe = "(chưa khai báo hệ)",
                MaMon = mon.MaMon,
                TenMon = mon.TenMon
            };

        var cauC = coHe.Concat(monKhongHe);

        Console.WriteLine("6.2c - Cả hệ chưa có môn và môn chưa khai báo hệ:");
        foreach (var item in cauC)
        {
            Console.WriteLine(
                $"  {item.TenHe} | {item.MaMon ?? "(chưa có)"} | {item.TenMon ?? "(chưa có môn)"}");
        }

        // d. Chỉ lấy hệ chưa có môn và môn chưa khai báo hệ.
        var heChuaCoMon =
            from he in dsHe
            where !dsMon.Any(mon => mon.He == he.MaHe)
            select new
            {
                Loai = "Hệ chưa có môn",
                Ma = he.MaHe,
                Ten = he.TenHe
            };

        var monChuaCoHe =
            from mon in dsMon
            where string.IsNullOrEmpty(mon.He) ||
                  !dsHe.Any(he => he.MaHe == mon.He)
            select new
            {
                Loai = "Môn chưa khai báo hệ",
                Ma = mon.MaMon,
                Ten = mon.TenMon
            };

        var cauD = heChuaCoMon.Concat(monChuaCoHe);

        Console.WriteLine("6.2d - Chỉ các phần tử chưa ghép được:");
        foreach (var item in cauD)
        {
            Console.WriteLine($"  {item.Loai}: {item.Ma} | {item.Ten}");
        }

        // e. Lấy 5 môn có số tiết cao nhất.
        var cauE =
            (from he in dsHe
             join mon in dsMon on he.MaHe equals mon.He
             orderby mon.SoTiet descending, mon.MaMon
             select new
             {
                 he.TenHe,
                 mon.MaMon,
                 mon.TenMon,
                 mon.SoTiet
             }).Take(5);

        Console.WriteLine("6.2e - 5 môn có số tiết giảm dần:");
        foreach (var item in cauE)
        {
            Console.WriteLine(
                $"  {item.TenHe} | {item.MaMon} | {item.TenMon} | {item.SoTiet}");
        }

        // f. Tổng số môn của mỗi hệ.
        var cauF =
            from he in dsHe
            join mon in dsMon on he.MaHe equals mon.He into nhom
            select new
            {
                he.MaHe,
                he.TenHe,
                TongSoMon = nhom.Count()
            };

        Console.WriteLine("6.2f - Tổng số môn mỗi hệ:");
        foreach (var item in cauF)
        {
            Console.WriteLine($"  {item.MaHe} | {item.TenHe} | {item.TongSoMon}");
        }

        // g. Số loại số tiết khác nhau.
        Console.WriteLine(
            $"6.2g - Số loại Số tiết khác nhau: {dsMon.Select(mon => mon.SoTiet).Distinct().Count()}");

        // h. Môn đầu tiên có tên bắt đầu bằng "Lập trình".
        var cauH = dsMon.FirstOrDefault(mon => mon.TenMon.StartsWith("Lập trình"));

        Console.WriteLine("6.2h - Môn đầu tiên bắt đầu bằng 'Lập trình':");
        if (cauH != null)
        {
            Console.WriteLine($"  {cauH.MaMon} | {cauH.TenMon}");
        }

        // i. Đánh số thứ tự trong từng nhóm hệ.
        var cauI =
            dsMon.GroupBy(mon => mon.He)
                 .OrderBy(g => g.Key)
                 .SelectMany(group => group
                     .OrderBy(mon => mon.MaMon)
                     .Select((mon, index) => new
                     {
                         He = group.Key,
                         STT = index + 1,
                         mon.MaMon,
                         mon.TenMon
                     }));

        Console.WriteLine("6.2i - Các môn theo từng hệ, có STT trong nhóm:");
        foreach (var item in cauI)
        {
            Console.WriteLine(
                $"  Hệ {(string.IsNullOrEmpty(item.He) ? "(chưa khai báo)" : item.He)} | " +
                $"STT {item.STT} | {item.MaMon} | {item.TenMon}");
        }
    }

    // =========================================================
    // HÀM HỖ TRỢ IN KẾT QUẢ
    // =========================================================
    static void InDay<T>(string title, IEnumerable<T> data)
    {
        Console.WriteLine($"{title}:");
        Console.WriteLine($"  {string.Join(", ", data)}");
    }

    static void InMonHoc(string title, IEnumerable<MonHoc> data)
    {
        Console.WriteLine($"{title}:");
        foreach (var mon in data)
        {
            Console.WriteLine(
                $"  {mon.MaMon} | {mon.TenMon} | {mon.He} | {mon.SoTiet} tiết");
        }
    }
}

public class MonHoc
{
    public string MaMon { get; set; } = "";
    public string TenMon { get; set; } = "";
    public string He { get; set; } = "";
    public byte SoTiet { get; set; }
}

public class He
{
    public string MaHe { get; set; } = "";
    public string TenHe { get; set; } = "";
}

using System.Collections.Generic;

public static class DuLieu
{
    public static List<MonHoc> DS_Mon()
    {
        return new List<MonHoc>
        {
            new() { MaMon = "HP2_1", TenMon = "Nền tảng C#", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP2_2", TenMon = "Công nghệ ADO.NET", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP3_1", TenMon = "Lập trình Windows Forms", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP3_2", TenMon = "Xây dựng ứng dụng Windows Forms", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP4_1", TenMon = "Lập trình Web với HTML, CSS và JavaScript", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP4_2", TenMon = "Xây dựng ứng dụng Web với ASP.NET", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP5_1", TenMon = "Lập trình CSDL SQL Server căn bản", He = "KTV", SoTiet = 64 },
            new() { MaMon = "HP5_2", TenMon = "Lập trình CSDL SQL Server nâng cao", He = "KTV", SoTiet = 64 },
            new() { MaMon = "JLCB", TenMon = "Joomla cơ bản", He = "CD", SoTiet = 72 },
            new() { MaMon = "LINQ", TenMon = "Language-Integrated Query", He = "CD", SoTiet = 64 },
            new() { MaMon = "DAWEB", TenMon = "Đồ án thực tế Web với ASP.NET", He = "CD", SoTiet = 40 },
            new() { MaMon = "DAWIN", TenMon = "Đồ án thực tế Windows Forms", He = "CD", SoTiet = 40 },
            new() { MaMon = "CC++", TenMon = "Lập trình hướng đối tượng với C/C++", He = "CD", SoTiet = 128 },
            new() { MaMon = "JQUE", TenMon = "JQuery", He = "CD", SoTiet = 22 },
            new() { MaMon = "XML", TenMon = "Công nghệ XML", He = "CD", SoTiet = 32 },
            new() { MaMon = "CRYS", TenMon = "Crystal Report trong Visual Studio", He = "CD", SoTiet = 32 },
            new() { MaMon = "BWEB", TenMon = "HTML, CSS và JavaScript", He = "CD", SoTiet = 32 },

            // XYZ có He = "" để phục vụ các câu outer join / phần tử chưa khai báo hệ.
            new() { MaMon = "XYZ", TenMon = "Chưa đặt tên môn", He = "", SoTiet = 0 }
        };
    }

    public static List<He> DS_He()
    {
        return new List<He>
        {
            new() { MaHe = "KTV", TenHe = "Kỹ thuật viên" },
            new() { MaHe = "CD", TenHe = "Chuyên đề" },
            new() { MaHe = "QT", TenHe = "Chứng chỉ quốc tế" }
        };
    }
}
