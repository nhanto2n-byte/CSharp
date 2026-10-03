using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_1
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.1: Lop ArrayPoint (ArrayList + Indexer) ===");

            ArrayPoint ds = new ArrayPoint();

            Console.Write("Nhap so luong diem can them: ");
            int n = int.Parse(Console.ReadLine()!);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Diem thu {i} --");
                Point p = new Point();
                p.Input();
                ds.Them(p);
            }

            Console.WriteLine("-- Danh sach diem --");
            ds.Xuat();

            // Demo dung indexer de truy cap truc tiep 1 phan tu
            if (ds.SoLuong > 0)
            {
                Console.WriteLine($"Diem dau tien (qua indexer ds[0]): {ds[0]}");
            }
        }
    }
}
