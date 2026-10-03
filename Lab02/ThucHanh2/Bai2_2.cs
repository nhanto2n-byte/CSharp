using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai2_2
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 2.2: Lop PersonList ===");

            PersonList ds = new PersonList();
            ds.Input();

            Console.WriteLine("-- Danh sach vua nhap --");
            ds.Output();

            PersonList dsSaoChep = new PersonList(ds);
            Console.WriteLine("-- Ban sao (copy constructor) --");
            dsSaoChep.Output();

            PersonList dsConSong = ds.LivingPeople();
            Console.WriteLine("-- Nhung nguoi con song --");
            if (dsConSong.SoLuong == 0)
                Console.WriteLine("(Khong co ai con song trong danh sach.)");
            else
                dsConSong.Output();
        }
    }
}
