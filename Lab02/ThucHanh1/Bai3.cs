using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 3: Nhap hai so nguyen x, y. Tinh x^y va xuat theo dinh dang:
    //   Nhap so nguyen x: 7
    //   Nhap so nguyen y: 3
    //   Ket qua 7 mu 3 la: 343
    static class Bai3
    {
        public static void Run()
        {
            Console.Write("Nhap so nguyen x: ");
            int x = int.Parse(Console.ReadLine()!);

            Console.Write("Nhap so nguyen y: ");
            int y = int.Parse(Console.ReadLine()!);

            double ketQua = Math.Pow(x, y);

            Console.WriteLine($"Ket qua {x} mu {y} la: {ketQua}");
        }
    }
}
