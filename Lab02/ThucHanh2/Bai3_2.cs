using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.2: Viet phuong thuc sap xep mot mang tong quat bang interface
    // (mo phong Array.Sort - tu viet thuat toan, khong goi Array.Sort co san)
    public class Bai3_2
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.2: Sap xep mang tong quat bang interface ===");

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

            // Goi phuong thuc sap xep tong quat tu viet (dua tren IComparable<T>)
            SapXepTongQuat.SapXepBangInterface(ds);

            Console.WriteLine("-- Sau khi sap xep bang interface (theo ho ten) --");
            foreach (SinhVien sv in ds) sv.Xuat();
        }
    }
}
