using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 16: Nhap vao mot mang ho ten cua n nguoi, sap xep mang do
    // theo thu tu tang dan (theo bang chu cai)
    static class Bai16
    {
        public static void Run()
        {
            Console.Write("Nhap so nguoi n: ");
            int n = int.Parse(Console.ReadLine()!);

            string[] hoTenMang = new string[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nhap ho ten nguoi thu {i + 1}: ");
                hoTenMang[i] = Console.ReadLine() ?? "";
            }

            Array.Sort(hoTenMang, StringComparer.Ordinal);

            Console.WriteLine();
            Console.WriteLine("Danh sach ho ten sau khi sap xep tang dan:");
            foreach (string hoTen in hoTenMang)
            {
                Console.WriteLine(hoTen);
            }
        }
    }
}
