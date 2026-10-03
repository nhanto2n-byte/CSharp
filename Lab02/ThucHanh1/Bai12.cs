using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 12: Nhap mot chuoi gom nhieu tu. Chuyen chuoi sang ky tu thuong,
    // sang ky tu hoa, va dem so tu trong chuoi.
    static class Bai12
    {
        public static int DemSoTu(string s)
        {
            string[] tuMang = s.Split(
                new char[] { ' ', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            return tuMang.Length;
        }

        public static void Run()
        {
            Console.Write("Nhap chuoi (nhieu tu): ");
            string chuoi = Console.ReadLine() ?? "";

            Console.WriteLine($"Chuoi viet thuong: {chuoi.ToLower()}");
            Console.WriteLine($"Chuoi viet hoa: {chuoi.ToUpper()}");
            Console.WriteLine($"So tu trong chuoi: {DemSoTu(chuoi)}");
        }
    }
}
