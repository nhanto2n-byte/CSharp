using System;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai3_4
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.4: Lop ConsoleMenu tong quat (ap dung giai PT bac 2) ===");

            PTBac2Console app = new PTBac2Console();
            app.Choose += (x) =>
            {
                if (x == 1)
                {
                    GiaiPhuongTrinh();
                }
            };
            app.Chay();
        }

        private static void GiaiPhuongTrinh()
        {
            Console.Write("Nhap he so a, b, c (cach nhau boi dau cach): ");
            string[] tam = (Console.ReadLine() ?? "").Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            double a = double.Parse(tam[0]);
            double b = double.Parse(tam[1]);
            double c = double.Parse(tam[2]);

            double x1 = 0, x2 = 0;
            int sn = GiaiPTBac2(a, b, c, ref x1, ref x2);

            if (sn == -1) Console.WriteLine("Vo so nghiem.");
            else if (sn == 0) Console.WriteLine("Vo nghiem.");
            else if (sn == 1) Console.WriteLine($"1 nghiem: x = {x1}");
            else Console.WriteLine($"2 nghiem: x1 = {x1}, x2 = {x2}");
        }

        private static int GiaiPTBac2(double a, double b, double c, ref double x1, ref double x2)
        {
            int sn;
            if (a == 0)
            {
                if (b == 0)
                {
                    sn = (c == 0) ? -1 : 0;
                }
                else
                {
                    sn = 1;
                    x1 = -c / b;
                }
            }
            else
            {
                double delta = b * b - 4 * a * c;
                if (delta < 0) sn = 0;
                else if (delta == 0)
                {
                    sn = 1;
                    x1 = -b / (2 * a);
                }
                else
                {
                    sn = 2;
                    x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                    x2 = (-b + Math.Sqrt(delta)) / (2 * a);
                }
            }
            return sn;
        }
    }
}
