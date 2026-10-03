using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    public class Bai3_5
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.5: Tinh luong nhan vien (ke thua, da hinh) ===");

            Console.Write("Nhap so luong nhan vien: ");
            int n = int.Parse(Console.ReadLine()!);

            List<NhanVien> danhSach = new List<NhanVien>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Nhan vien thu {i + 1} --");
                Console.Write("Bo phan (1 = Kinh doanh, 2 = San xuat): ");
                string boPhan = Console.ReadLine() ?? "";

                NhanVien nv = boPhan == "2" ? new NhanVienSanXuat() : new NhanVienKinhDoanh();
                nv.Input();
                danhSach.Add(nv);
            }

            Console.WriteLine("-- Bang luong (da hinh: goi TinhLuong() qua kieu NhanVien) --");
            foreach (NhanVien nv in danhSach)
            {
                nv.Output();
            }
        }
    }
}
