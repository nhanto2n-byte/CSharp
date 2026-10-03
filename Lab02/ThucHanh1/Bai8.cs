using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 8: Hoan vi hai so thuc bang tham chieu ref
    static class Bai8
    {
        public static void HoanVi(ref double a, ref double b)
        {
            double tam = a;
            a = b;
            b = tam;
        }

        public static void Run()
        {
            Console.Write("Nhap a: ");
            double a = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap b: ");
            double b = double.Parse(Console.ReadLine()!);

            HoanVi(ref a, ref b);

            Console.WriteLine($"Sau khi hoan vi: a = {a}, b = {b}");
        }
    }
}
