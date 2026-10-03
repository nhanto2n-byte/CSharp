using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 4: Giong bai 3, nhung bao loi khi x hay y khong phai la so nguyen.
    // Dung int.TryParse thay vi int.Parse de khong lam sap chuong trinh
    // khi nguoi dung nhap sai kieu du lieu.
    static class Bai4
    {
        public static void Run()
        {
            Console.Write("Nhap so nguyen x: ");
            string? inputX = Console.ReadLine();

            Console.Write("Nhap so nguyen y: ");
            string? inputY = Console.ReadLine();

            if (!int.TryParse(inputX, out int x) || !int.TryParse(inputY, out int y))
            {
                Console.WriteLine("Loi: x va y phai la so nguyen!");
                return;
            }

            double ketQua = Math.Pow(x, y);
            Console.WriteLine($"Ket qua {x} mu {y} la: {ketQua}");
        }
    }
}
