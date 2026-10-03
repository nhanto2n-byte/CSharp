using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.4: Lop chua mang 2 chieu - Mang2Chieu (kich thuoc n x m)
    public class Mang2Chieu
    {
        private int[,] mang;

        // Constructor mac dinh
        public Mang2Chieu()
        {
            mang = new int[0, 0];
        }

        // Constructor theo kich thuoc n x m
        public Mang2Chieu(int n, int m)
        {
            mang = new int[n, m];
        }

        // Constructor sao chep
        public Mang2Chieu(Mang2Chieu other)
        {
            int n = other.SoDong, m = other.SoCot;
            mang = new int[n, m];
            Array.Copy(other.mang, mang, mang.Length);
        }

        public int SoDong => mang.GetLength(0);
        public int SoCot => mang.GetLength(1);

        // Indexer de truy cap phan tu tai (i, j)
        public int this[int i, int j]
        {
            get => mang[i, j];
            set => mang[i, j] = value;
        }

        // Nhap mang
        public void Input()
        {
            Console.Write("Nhap so dong: ");
            int n = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap so cot: ");
            int m = int.Parse(Console.ReadLine()!);

            mang = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"Phan tu [{i},{j}]: ");
                    mang[i, j] = int.Parse(Console.ReadLine()!);
                }
            }
        }

        // Xuat mang
        public void Output()
        {
            for (int i = 0; i < SoDong; i++)
            {
                for (int j = 0; j < SoCot; j++)
                {
                    Console.Write($"{mang[i, j],6}");
                }
                Console.WriteLine();
            }
        }

        private static bool LaNguyenTo(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        // Tim cac so nguyen to trong mang
        public List<int> TimSoNguyenTo()
        {
            List<int> ketQua = new List<int>();
            foreach (int x in mang)
            {
                if (LaNguyenTo(x)) ketQua.Add(x);
            }
            return ketQua;
        }
    }
}
