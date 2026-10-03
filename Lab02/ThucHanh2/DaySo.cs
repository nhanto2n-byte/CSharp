using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.3: Lop chua mang 1 chieu - DaySo
    public class DaySo
    {
        private int[] mang;

        // Constructor mac dinh
        public DaySo()
        {
            mang = new int[0];
        }

        // Constructor theo so luong phan tu
        public DaySo(int n)
        {
            mang = new int[n];
        }

        // Constructor sao chep
        public DaySo(DaySo other)
        {
            mang = new int[other.mang.Length];
            Array.Copy(other.mang, mang, mang.Length);
        }

        public int SoLuong => mang.Length;

        // Indexer de truy cap phan tu thu i trong day
        public int this[int i]
        {
            get => mang[i];
            set => mang[i] = value;
        }

        // Nhap day so
        public void Input()
        {
            Console.Write("Nhap so luong phan tu: ");
            int n = int.Parse(Console.ReadLine()!);
            mang = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Phan tu {i}: ");
                mang[i] = int.Parse(Console.ReadLine()!);
            }
        }

        // Xuat day so
        public void Output()
        {
            Console.Write("Day so:");
            foreach (int x in mang) Console.Write($" {x}");
            Console.WriteLine();
        }

        // Tim cac so chan, tra ve mang moi
        public int[] TimSoChan()
        {
            List<int> ketQua = new List<int>();
            foreach (int x in mang)
            {
                if (x % 2 == 0) ketQua.Add(x);
            }
            return ketQua.ToArray();
        }
    }
}
