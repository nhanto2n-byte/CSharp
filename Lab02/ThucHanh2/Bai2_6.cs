using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_6
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.6: Day phan so - tinh tong ===");

            DayPhanSo ds = new DayPhanSo();
            ds.Input();

            Console.WriteLine("-- Danh sach phan so --");
            ds.Output();

            PhanSo tong = ds.TinhTong();
            Console.WriteLine($"Tong cua {ds.SoLuong} phan so tren la: {tong}");
        }
    }
}
