using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.2: Lop PersonList - quan ly nhieu Person
    public class PersonList
    {
        private List<Person> danhSach;

        // Default constructor
        public PersonList()
        {
            danhSach = new List<Person>();
        }

        // Copy constructor (sao chep sau - dung lai copy constructor cua Person)
        public PersonList(PersonList other)
        {
            danhSach = new List<Person>();
            foreach (Person p in other.danhSach)
            {
                danhSach.Add(new Person(p));
            }
        }

        public int SoLuong => danhSach.Count;

        public void Input()
        {
            Console.Write("Nhap so luong nguoi: ");
            int n = int.Parse(Console.ReadLine()!);

            danhSach = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Nguoi thu {i + 1} --");
                Person p = new Person();
                p.Input();
                danhSach.Add(p);
            }
        }

        public void Output()
        {
            for (int i = 0; i < danhSach.Count; i++)
            {
                Console.Write($"[{i}] ");
                danhSach[i].Output();
            }
        }

        // Them 1 Person vao danh sach
        public void Add(Person x)
        {
            danhSach.Add(x);
        }

        // Tra ve mot PersonList chi gom nhung nguoi con song
        public PersonList LivingPeople()
        {
            PersonList ketQua = new PersonList();
            foreach (Person p in danhSach)
            {
                if (p.IsLiving())
                {
                    ketQua.Add(p);
                }
            }
            return ketQua;
        }
    }
}
