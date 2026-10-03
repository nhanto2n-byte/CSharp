using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_3
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.3: Lop chua mang 1 chieu (DaySo) ===");

            DaySo ds = new DaySo();
            ds.Input();
            ds.Output();

            Console.Write("Phan tu tai vi tri 0 (qua indexer): ");
            Console.WriteLine(ds.SoLuong > 0 ? ds[0].ToString() : "(day rong)");

            int[] cacSoChan = ds.TimSoChan();
            Console.Write("Cac so chan trong day:");
            foreach (int x in cacSoChan) Console.Write($" {x}");
            Console.WriteLine();
        }
    }
}
