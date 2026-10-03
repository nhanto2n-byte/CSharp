using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.5: Lop DonThuc - tinh gia tri va dao ham
    public class Bai1_5
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 1.5: Lop Don thuc ===");

            DonThuc p = new DonThuc();
            p.Input();
            Console.WriteLine($"P(x) = {p}");

            Console.Write("Nhap gia tri x can tinh: ");
            double x = double.Parse(Console.ReadLine()!);

            // (a) Tinh gia tri don thuc
            Console.WriteLine($"(a) P({x}) = {p.TinhGiaTri(x):0.00}");

            // (b) Dao ham don thuc
            DonThuc q = p.DaoHam();
            Console.WriteLine($"(b) Dao ham Q(x) = {q}");
            Console.WriteLine($"Q({x}) = {q.TinhGiaTri(x):0.00}");
        }
    }
}
