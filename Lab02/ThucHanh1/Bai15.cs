using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 15: Cac phuong thuc thanh vien lam viec voi mang 1 chieu
    //  - Nhap mang gom n phan tu
    //  - In mang ra man hinh
    //  - Tim phan tu lon nhat va nho nhat trong mang
    //  - Tra ve mang cac so nguyen to
    static class Bai15
    {
        public static int[] NhapMang(int n)
        {
            int[] mang = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Nhap phan tu thu {i + 1}: ");
                mang[i] = int.Parse(Console.ReadLine()!);
            }
            return mang;
        }

        public static void InMang(int[] mang)
        {
            Console.Write("Mang: ");
            foreach (int giaTri in mang)
            {
                Console.Write(giaTri + " ");
            }
            Console.WriteLine();
        }

        public static void TimMaxMin(int[] mang, out int max, out int min)
        {
            max = mang[0];
            min = mang[0];

            foreach (int giaTri in mang)
            {
                if (giaTri > max) max = giaTri;
                if (giaTri < min) min = giaTri;
            }
        }

        public static int[] LayMangSoNguyenTo(int[] mang)
        {
            List<int> ketQua = new List<int>();
            foreach (int giaTri in mang)
            {
                if (Bai7.LaSoNguyenTo(giaTri))
                {
                    ketQua.Add(giaTri);
                }
            }
            return ketQua.ToArray();
        }

        public static void Run()
        {
            Console.Write("Nhap so phan tu n: ");
            int n = int.Parse(Console.ReadLine()!);

            int[] mang = NhapMang(n);

            InMang(mang);

            TimMaxMin(mang, out int max, out int min);
            Console.WriteLine($"Gia tri lon nhat: {max}");
            Console.WriteLine($"Gia tri nho nhat: {min}");

            int[] mangNguyenTo = LayMangSoNguyenTo(mang);
            Console.Write("Cac so nguyen to trong mang: ");
            if (mangNguyenTo.Length == 0)
            {
                Console.WriteLine("(khong co)");
            }
            else
            {
                InMang(mangNguyenTo);
            }
        }
    }
}
