using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.5: Ke thua va da hinh - tinh luong nhan vien theo tung bo phan
    public abstract class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }

        public NhanVien()
        {
            MaNV = "";
            HoTen = "";
        }

        public virtual void Input()
        {
            Console.Write("Nhap ma nhan vien: ");
            MaNV = Console.ReadLine() ?? "";
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? "";
        }

        // Phuong thuc truu tuong: moi bo phan tinh luong 1 kieu (da hinh)
        public abstract double TinhLuong();

        public virtual void Output()
        {
            Console.WriteLine($"Ma NV: {MaNV}, Ho ten: {HoTen}, Luong: {TinhLuong():0.00}");
        }
    }

    // Nhan vien kinh doanh: luong co ban + 500.000/hop dong da ky
    public class NhanVienKinhDoanh : NhanVien
    {
        public double LuongCoBan { get; set; }
        public int SoHopDong { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhap luong co ban: ");
            LuongCoBan = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap so hop dong da ky: ");
            SoHopDong = int.Parse(Console.ReadLine()!);
        }

        public override double TinhLuong()
        {
            return LuongCoBan + SoHopDong * 500000;
        }
    }

    // Nhan vien san xuat: luong = so luong san pham x 1000, tren 3000 sp thi thuong them 5%
    public class NhanVienSanXuat : NhanVien
    {
        public int SoLuongSanPham { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhap so luong san pham: ");
            SoLuongSanPham = int.Parse(Console.ReadLine()!);
        }

        public override double TinhLuong()
        {
            double luong = SoLuongSanPham * 1000;
            if (SoLuongSanPham > 3000)
            {
                luong += luong * 0.05;
            }
            return luong;
        }
    }
}
