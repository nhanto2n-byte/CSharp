using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai3_6
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.6: Tinh diem thi sinh (ke thua, da hinh) ===");

            Console.Write("Nhap so luong thi sinh: ");
            int n = int.Parse(Console.ReadLine()!);

            List<ThiSinh> danhSach = new List<ThiSinh>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Thi sinh thu {i + 1} --");
                Console.Write("Doi tuong (1 = Chuyen, 2 = Sieu cup): ");
                string doiTuong = Console.ReadLine() ?? "";

                ThiSinh ts = doiTuong == "2" ? new ThiSinhSieuCup() : new ThiSinhChuyen();
                ts.Input();
                danhSach.Add(ts);
            }

            Console.WriteLine("-- Ket qua (da hinh: goi TinhTongDiem() qua kieu ThiSinh) --");
            foreach (ThiSinh ts in danhSach)
            {
                ts.Output();
            }
        }
    }
}
