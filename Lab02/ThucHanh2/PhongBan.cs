using System;
using System.Collections.Generic;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.7 (PDF ghi la "Bai 2.5" - trung so, day la loi danh so cua de goc):
    // Lop NhanVienDon (chua ke thua, se mo rong thanh NhanVienKinhDoanh/SanXuat o Bai 3.5)
    public class NhanVienDon
    {
        public string HoTen { get; set; }
        public double MucLuong { get; set; }
        public int SoNgayVang { get; set; }

        public NhanVienDon()
        {
            HoTen = "";
            MucLuong = 0;
            SoNgayVang = 0;
        }

        public void Input()
        {
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? "";
            Console.Write("Nhap muc luong: ");
            MucLuong = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap so ngay vang: ");
            SoNgayVang = int.Parse(Console.ReadLine()!);
        }

        // Moi ngay vang bi tru 100.000 VND
        public double TinhLuongThucNhan()
        {
            return MucLuong - SoNgayVang * 100000;
        }

        public void Output()
        {
            Console.WriteLine($"{HoTen}: luong = {MucLuong}, vang = {SoNgayVang} ngay, thuc nhan = {TinhLuongThucNhan()}");
        }
    }

    // Lop PhongBan chua n nhan vien
    public class PhongBan
    {
        private List<NhanVienDon> danhSach;

        public PhongBan()
        {
            danhSach = new List<NhanVienDon>();
        }

        public void Input()
        {
            Console.Write("Nhap so luong nhan vien: ");
            int n = int.Parse(Console.ReadLine()!);

            danhSach = new List<NhanVienDon>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Nhan vien thu {i + 1} --");
                NhanVienDon nv = new NhanVienDon();
                nv.Input();
                danhSach.Add(nv);
            }
        }

        public void Output()
        {
            foreach (NhanVienDon nv in danhSach)
            {
                nv.Output();
            }
        }

        // Tong luong cua ca phong ban
        public double TongLuong()
        {
            double tong = 0;
            foreach (NhanVienDon nv in danhSach)
            {
                tong += nv.TinhLuongThucNhan();
            }
            return tong;
        }
    }
}
