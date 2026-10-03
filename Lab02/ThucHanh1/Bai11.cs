using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 11: Phuong thuc tra ve chuoi la dao nguoc cua mot chuoi
    static class Bai11
    {
        public static string DaoNguoc(string s)
        {
            char[] mangKyTu = s.ToCharArray();
            Array.Reverse(mangKyTu);
            return new string(mangKyTu);
        }

        public static void Run()
        {
            Console.Write("Nhap chuoi: ");
            string chuoi = Console.ReadLine() ?? "";

            Console.WriteLine($"Chuoi dao nguoc: {DaoNguoc(chuoi)}");
        }
    }
}
