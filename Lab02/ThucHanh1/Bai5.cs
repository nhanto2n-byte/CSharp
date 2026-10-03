using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 5: In ra menu va xu ly cac lua chon tuong ung
    //   MENU
    //   1. Nhap hai gia tri so thuc cho x, y
    //   2. Tinh x^y
    //   3. Tinh can bac 2 cua x va y
    //   4. Thoat
    static class Bai5
    {
        public static void Run()
        {
            double x = 0, y = 0;
            bool daNhap = false; // co du lieu x, y hay chua
            bool thoat = false;

            while (!thoat)
            {
                Console.WriteLine();
                Console.WriteLine("MENU");
                Console.WriteLine("1. Nhap hai gia tri so thuc cho x, y");
                Console.WriteLine("2. Tinh x^y");
                Console.WriteLine("3. Tinh can bac 2 cua x va y");
                Console.WriteLine("4. Thoat");
                Console.Write("Chon chuc nang: ");
                string? luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1":
                        Console.Write("Nhap x: ");
                        x = double.Parse(Console.ReadLine()!);
                        Console.Write("Nhap y: ");
                        y = double.Parse(Console.ReadLine()!);
                        daNhap = true;
                        break;

                    case "2":
                        if (!daNhap)
                        {
                            Console.WriteLine("Ban chua nhap x, y. Vui long chon muc 1 truoc.");
                        }
                        else
                        {
                            Console.WriteLine($"{x}^{y} = {Math.Pow(x, y)}");
                        }
                        break;

                    case "3":
                        if (!daNhap)
                        {
                            Console.WriteLine("Ban chua nhap x, y. Vui long chon muc 1 truoc.");
                        }
                        else
                        {
                            if (x < 0)
                                Console.WriteLine("x < 0 nen khong tinh duoc can bac 2 trong tap so thuc.");
                            else
                                Console.WriteLine($"Can bac 2 cua x = {Math.Sqrt(x)}");

                            if (y < 0)
                                Console.WriteLine("y < 0 nen khong tinh duoc can bac 2 trong tap so thuc.");
                            else
                                Console.WriteLine($"Can bac 2 cua y = {Math.Sqrt(y)}");
                        }
                        break;

                    case "4":
                        thoat = true;
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }
            }
        }
    }
}
