using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.1: Tinh tuoi 1 sinh vien
    public class Bai1_1
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 1.1: Tinh tuoi sinh vien ===");

            SinhVien sv = new SinhVien();
            sv.Nhap();
            sv.Xuat();
        }
    }
}
