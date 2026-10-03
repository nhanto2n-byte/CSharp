using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_5
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.5: Lop Da thuc (n+1 don thuc) ===");

            DaThuc p = new DaThuc();
            p.Input();

            Console.WriteLine("-- Da thuc vua nhap --");
            p.Output();

            Console.WriteLine($"Don thuc thu 0 (qua indexer): {p[0]}");

            Console.Write("Nhap gia tri x can tinh: ");
            double x = double.Parse(Console.ReadLine()!);
            Console.WriteLine($"P({x}) = {p.TinhGiaTri(x):0.00}");
        }
    }
}
