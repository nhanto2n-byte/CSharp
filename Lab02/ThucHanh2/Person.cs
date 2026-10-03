using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.3: Lop Person - id, name, yob, yod
    public class Person
    {
        // Field
        private int id;
        private string name;
        private int yob; // nam sinh
        private int yod; // nam mat, 0 neu con song

        // Property
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Yob { get => yob; set => yob = value; }
        public int Yod { get => yod; set => yod = value; }

        // Default constructor
        public Person()
        {
            id = 0;
            name = "";
            yob = 0;
            yod = 0;
        }

        // Copy constructor
        public Person(Person other)
        {
            id = other.id;
            name = other.name;
            yob = other.yob;
            yod = other.yod;
        }

        // Method: nhap du lieu
        public void Input()
        {
            Console.Write("Nhap id: ");
            id = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap ho ten: ");
            name = Console.ReadLine() ?? "";
            Console.Write("Nhap nam sinh: ");
            yob = int.Parse(Console.ReadLine()!);
            Console.Write("Nhap nam mat (nhap 0 neu con song): ");
            yod = int.Parse(Console.ReadLine()!);
        }

        // Method: xuat du lieu
        public void Output()
        {
            Console.WriteLine($"Id: {id}, Ho ten: {name}, Nam sinh: {yob}, Nam mat: {yod}");
        }

        // Method: kiem tra con song hay khong
        public bool IsLiving()
        {
            return yod == 0;
        }
    }
}
