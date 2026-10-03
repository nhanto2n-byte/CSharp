using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 2: Xuat va nhap chuoi
    // Dinh dang yeu cau:
    //   Nhap ho ten cua ban: Tran Anh Minh
    //   Chao ban Tran Anh Minh!
    static class Bai2
    {
        public static void Run()
        {
            Console.Write("Nhap ho ten cua ban: ");
            string? hoTen = Console.ReadLine();

            Console.WriteLine($"Chao ban {hoTen}!");
        }
    }
}
