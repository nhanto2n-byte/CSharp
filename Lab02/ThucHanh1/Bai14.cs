using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 14: Nhap thong tin mot nhan vien (ho ten, muc luong, so ngay vang).
    // Tinh va xuat luong thuc nhan, moi ngay vang bi tru 100.000 VND.
    static class Bai14
    {
        private const double TienTruMoiNgayVang = 100000;

        public static void Run()
        {
            Console.Write("Nhap ho ten nhan vien: ");
            string hoTen = Console.ReadLine() ?? "";

            Console.Write("Nhap muc luong: ");
            double mucLuong = double.Parse(Console.ReadLine()!);

            Console.Write("Nhap so ngay vang: ");
            int soNgayVang = int.Parse(Console.ReadLine()!);

            double luongThucNhan = mucLuong - soNgayVang * TienTruMoiNgayVang;

            Console.WriteLine();
            Console.WriteLine($"Nhan vien: {hoTen}");
            Console.WriteLine($"Luong thuc nhan: {luongThucNhan}");
        }
    }
}
