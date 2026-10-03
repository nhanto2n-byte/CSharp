using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_4
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.4: Lop chua mang 2 chieu (Mang2Chieu) ===");

            Mang2Chieu m = new Mang2Chieu();
            m.Input();

            Console.WriteLine("-- Mang vua nhap --");
            m.Output();

            Console.WriteLine($"Phan tu tai (0,0) (qua indexer): {(m.SoDong > 0 && m.SoCot > 0 ? m[0, 0].ToString() : "(mang rong)")}");

            var soNguyenTo = m.TimSoNguyenTo();
            Console.Write("Cac so nguyen to trong mang:");
            foreach (int x in soNguyenTo) Console.Write($" {x}");
            Console.WriteLine();
        }
    }
}
