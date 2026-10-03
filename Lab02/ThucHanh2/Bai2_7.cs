using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_7
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.7: Tinh luong phong ban ===");

            PhongBan pb = new PhongBan();
            pb.Input();

            Console.WriteLine("-- Bang luong --");
            pb.Output();

            Console.WriteLine($"Tong luong ca phong ban: {pb.TongLuong()}");
        }
    }
}
