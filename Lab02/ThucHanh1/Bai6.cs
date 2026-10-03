using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 6: Phuong thuc tim gia tri lon nhat cua ba so nguyen (return gia tri)
    static class Bai6
    {
        public static int Max3(int a, int b, int c)
        {
            int max = a;
            if (b > max) max = b;
            if (c > max) max = c;
            return max;
        }

        public static void Run()
        {
            Console.Write("Nhap a: ");
            int a = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap b: ");
            int b = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap c: ");
            int c = int.Parse(Console.ReadLine()!);

            Console.WriteLine($"Gia tri lon nhat la: {Max3(a, b, c)}");
        }
    }
}
