using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Lop SinhVien: luu tru thong tin mot sinh vien
    // (ma sinh vien, ho ten, dia chi, sinh vien nam thu may)
    class SinhVien
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public int NamThu { get; set; }

        public void Nhap()
        {
            Console.Write("Nhap ma sinh vien: ");
            MaSV = Console.ReadLine() ?? "";

            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? "";

            Console.Write("Nhap dia chi: ");
            DiaChi = Console.ReadLine() ?? "";

            Console.Write("Sinh vien nam thu may: ");
            NamThu = int.Parse(Console.ReadLine()!);
        }

        public void Xuat()
        {
            Console.WriteLine($"Ma SV   : {MaSV}");
            Console.WriteLine($"Ho ten  : {HoTen}");
            Console.WriteLine($"Dia chi : {DiaChi}");
            Console.WriteLine($"Nam thu : {NamThu}");
        }
    }

    // Bai 13: Nhap xuat thong tin 1 sinh vien
    static class Bai13
    {
        public static void Run()
        {
            SinhVien sv = new SinhVien();
            sv.Nhap();

            Console.WriteLine();
            Console.WriteLine("Thong tin sinh vien vua nhap:");
            sv.Xuat();
        }
    }
}
