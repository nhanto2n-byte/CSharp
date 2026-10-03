using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.4: Lop PhanSo - demo constructor va cac toan tu overload
    public class Bai1_4
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 1.4: Lop Phan so ===");

            PhanSo a = new PhanSo();
            Console.WriteLine("-- Nhap phan so A --");
            a.Input();

            PhanSo b = new PhanSo();
            Console.WriteLine("-- Nhap phan so B --");
            b.Input();

            // Constructor sao chep
            PhanSo c = new PhanSo(a);

            Console.WriteLine($"A = {a}");
            Console.WriteLine($"B = {b}");
            Console.WriteLine($"C (sao chep tu A) = {c}");
            Console.WriteLine($"+A = {+a}");
            Console.WriteLine($"-A = {-a}");
            Console.WriteLine($"A + B = {a + b}");
            Console.WriteLine($"A - B = {a - b}");
            Console.WriteLine($"A * B = {a * b}");
            Console.WriteLine($"A / B = {a / b}");
            Console.WriteLine($"A > B  : {a > b}");
            Console.WriteLine($"A < B  : {a < b}");
            Console.WriteLine($"A >= B : {a >= b}");
            Console.WriteLine($"A <= B : {a <= b}");
            Console.WriteLine($"A == B : {a == b}");
            Console.WriteLine($"A != B : {a != b}");
            Console.WriteLine($"A == C : {a == c}");
        }
    }
}
