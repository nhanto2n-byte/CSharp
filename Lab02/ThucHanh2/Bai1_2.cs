using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.2: Lop Point - khoang cach va trung diem giua 2 diem
    public class Bai1_2
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 1.2: Lop Point ===");

            Point a = new Point();
            Console.WriteLine("-- Nhap diem A --");
            a.Input();

            Point b = new Point();
            Console.WriteLine("-- Nhap diem B --");
            b.Input();

            Console.WriteLine($"A = {a}");
            Console.WriteLine($"B = {b}");
            Console.WriteLine($"A + B = {a + b}");
            Console.WriteLine($"A - B = {a - b}");
            Console.WriteLine($"-A = {-a}");

            // (a) Khoang cach giua 2 diem
            Console.WriteLine($"Khoang cach AB (phuong thuc thanh vien): {a.KhoangCachDen(b):0.00}");
            Console.WriteLine($"Khoang cach AB (phuong thuc tinh)     : {Point.KhoangCach(a, b):0.00}");

            // (b) Trung diem cua 2 diem
            Point i1 = a.TrungDiemVoi(b);
            Point i2 = Point.TrungDiem(a, b);
            Console.WriteLine($"Trung diem I (phuong thuc thanh vien): {i1}");
            Console.WriteLine($"Trung diem I (phuong thuc tinh)     : {i2}");
        }
    }
}
