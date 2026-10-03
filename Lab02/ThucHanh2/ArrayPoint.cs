using System;
using System.Collections;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.1: Lop ArrayPoint - luu tru cac Point bang ArrayList, co Indexer
    public class ArrayPoint
    {
        // Field: mot ArrayList cac Point
        private ArrayList danhSach;

        public ArrayPoint()
        {
            danhSach = new ArrayList();
        }

        public int SoLuong => danhSach.Count;

        // Indexer cho phep truy cap Point thu i cua ArrayList
        public Point this[int i]
        {
            get => (Point)danhSach[i]!;
            set => danhSach[i] = value;
        }

        public void Them(Point p)
        {
            danhSach.Add(p);
        }

        public void Xuat()
        {
            for (int i = 0; i < danhSach.Count; i++)
            {
                Console.WriteLine($"Diem thu {i}: {this[i]}");
            }
        }
    }
}
