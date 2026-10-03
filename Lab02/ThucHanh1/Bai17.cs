using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 17: Cac phuong thuc thanh vien lam viec voi mang 2 chieu
    //  - Sinh ngau nhien mang A[n x m] trong doan [10, 100] (n, m nhap tu ban phim)
    //  - In mang ra man hinh
    //  - Tra ve hai mang: mang cac so chan va mang cac so le
    static class Bai17
    {
        public static int[,] SinhMangNgauNhien(int n, int m)
        {
            Random rd = new Random();
            int[,] mang = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mang[i, j] = rd.Next(10, 101); // sinh trong doan [10, 100]
                }
            }
            return mang;
        }

        public static void InMang(int[,] mang)
        {
            int n = mang.GetLength(0);
            int m = mang.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(mang[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void TachChanLe(int[,] mang, out int[] mangChan, out int[] mangLe)
        {
            List<int> danhSachChan = new List<int>();
            List<int> danhSachLe = new List<int>();

            int n = mang.GetLength(0);
            int m = mang.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mang[i, j] % 2 == 0)
                        danhSachChan.Add(mang[i, j]);
                    else
                        danhSachLe.Add(mang[i, j]);
                }
            }

            mangChan = danhSachChan.ToArray();
            mangLe = danhSachLe.ToArray();
        }

        public static void Run()
        {
            Console.Write("Nhap so dong n: ");
            int n = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap so cot m: ");
            int m = int.Parse(Console.ReadLine()!);

            int[,] mang = SinhMangNgauNhien(n, m);

            Console.WriteLine();
            Console.WriteLine("Mang vua sinh:");
            InMang(mang);

            TachChanLe(mang, out int[] mangChan, out int[] mangLe);

            Console.WriteLine();
            Console.Write("Cac so chan: ");
            Console.WriteLine(string.Join(" ", mangChan));

            Console.Write("Cac so le: ");
            Console.WriteLine(string.Join(" ", mangLe));
        }
    }
}
