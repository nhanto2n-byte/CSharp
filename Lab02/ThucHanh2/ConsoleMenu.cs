using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.4*: Lop ConsoleMenu tong quat, mo rong qua su kien (event) va ke thua
    public abstract class ConsoleMenu
    {
        // Event: phat sinh khi nguoi dung chon 1 chuc nang (khac 0)
        public event Action<int>? Choose;

        // Moi lop con tu ve menu cua rieng minh
        protected abstract void HienThiMenu();

        public void Chay()
        {
            bool dangChay = true;
            while (dangChay)
            {
                Console.WriteLine();
                HienThiMenu();
                Console.Write("Thuc hien: ");
                int luaChon = int.Parse(Console.ReadLine()!);

                if (luaChon == 0)
                {
                    dangChay = false;
                }
                else
                {
                    Console.WriteLine($"Ban thuc hien chuc nang {luaChon}");
                    Choose?.Invoke(luaChon);
                }
            }
        }
    }

    // Ap dung cho bai toan giai phuong trinh bac 2
    public class PTBac2Console : ConsoleMenu
    {
        protected override void HienThiMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. Giai phuong trinh bac 2");
            Console.WriteLine("0. Thoat chuong trinh");
        }
    }
}
