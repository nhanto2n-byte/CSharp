using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.3: Lop Person - Default constructor, Copy constructor, Input/Output, IsLiving
    public class Bai1_3
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 1.3: Lop Person ===");

            Person p1 = new Person();
            p1.Input();
            Console.WriteLine("-- Thong tin vua nhap --");
            p1.Output();
            Console.WriteLine(p1.IsLiving() ? "Nguoi nay con song." : "Nguoi nay da mat.");

            // Demo copy constructor
            Person p2 = new Person(p1);
            Console.WriteLine("-- Ban sao (copy constructor) --");
            p2.Output();
        }
    }
}
