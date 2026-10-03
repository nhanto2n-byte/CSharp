using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.2: Lop Point - Field, Property, Constructor, Method, toan tu +, -, lay am
    public class Point
    {
        // Field
        private double x;
        private double y;

        // Property
        public double X
        {
            get => x;
            set => x = value;
        }

        public double Y
        {
            get => y;
            set => y = value;
        }

        // Default constructor: khoi tao x, y bang 0
        public Point()
        {
            x = 0;
            y = 0;
        }

        // Constructor co tham so (tien ich de tao nhanh 1 diem trong code)
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // Method: nhap toa do
        public void Input()
        {
            Console.Write("Nhap toa do x: ");
            x = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap toa do y: ");
            y = double.Parse(Console.ReadLine()!);
        }

        // Method: xuat toa do
        public void Output()
        {
            Console.WriteLine(ToString());
        }

        // Override ToString de xuat Point
        public override string ToString()
        {
            return $"({x}, {y})";
        }

        // Toan tu +: cong 2 diem
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        // Toan tu -: tru 2 diem
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        // Toan tu lay am (mot ngoi)
        public static Point operator -(Point a)
        {
            return new Point(-a.x, -a.y);
        }

        // (a) Khoang cach giua 2 diem - phuong thuc thanh vien
        public double KhoangCachDen(Point b)
        {
            return Math.Sqrt((x - b.x) * (x - b.x) + (y - b.y) * (y - b.y));
        }

        // (a) Khoang cach giua 2 diem - phuong thuc tinh
        public static double KhoangCach(Point a, Point b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }

        // (b) Trung diem cua 2 diem - phuong thuc thanh vien
        public Point TrungDiemVoi(Point b)
        {
            return new Point((x + b.x) / 2, (y + b.y) / 2);
        }

        // (b) Trung diem cua 2 diem - phuong thuc tinh
        public static Point TrungDiem(Point a, Point b)
        {
            return new Point((a.x + b.x) / 2, (a.y + b.y) / 2);
        }
    }
}
