using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.1: Dung phuong thuc tinh Array.Sort(...) de sap xep cac doi tuong SinhVien
    // (SinhVien da implement IComparable<SinhVien> - xem file SinhVien.cs)
    public class Bai3_1
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.1: Array.Sort voi doi tuong SinhVien ===");

            Console.Write("Nhap so luong sinh vien: ");
            int n = int.Parse(Console.ReadLine()!);

            SinhVien[] ds = new SinhVien[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Sinh vien thu {i + 1} --");
                ds[i] = new SinhVien();
                ds[i].Nhap();
            }

            Console.WriteLine("-- Truoc khi sap xep --");
            foreach (SinhVien sv in ds) sv.Xuat();

            // Array.Sort goi CompareTo cua SinhVien (sap xep theo ho ten)
            Array.Sort(ds);

            Console.WriteLine("-- Sau khi Array.Sort (theo ho ten) --");
            foreach (SinhVien sv in ds) sv.Xuat();
        }
    }
}
