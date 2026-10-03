using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.6: Ke thua va da hinh - tinh diem thi sinh
    public abstract class ThiSinh
    {
        public string Sbd { get; set; }
        public string HoTen { get; set; }
        public double Bai1 { get; set; }
        public double Bai2 { get; set; }
        public double Bai3 { get; set; }

        public ThiSinh()
        {
            Sbd = "";
            HoTen = "";
        }

        public virtual void Input()
        {
            Console.Write("Nhap so bao danh: ");
            Sbd = Console.ReadLine() ?? "";
            Console.Write("Nhap ho ten: ");
            HoTen = Console.ReadLine() ?? "";
            Console.Write("Nhap diem bai 1: ");
            Bai1 = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap diem bai 2: ");
            Bai2 = double.Parse(Console.ReadLine()!);
            Console.Write("Nhap diem bai 3: ");
            Bai3 = double.Parse(Console.ReadLine()!);
        }

        // Phuong thuc truu tuong: moi doi tuong thi sinh tinh tong diem 1 kieu (da hinh)
        public abstract double TinhTongDiem();

        public virtual void Output()
        {
            Console.WriteLine($"SBD: {Sbd}, Ho ten: {HoTen}, Tong diem: {TinhTongDiem():0.00}");
        }
    }

    // Thi sinh Chuyen: tong 3 bai lap trinh + diem thuong tieng Anh
    public class ThiSinhChuyen : ThiSinh
    {
        public double TiengAnh { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhap diem tieng Anh: ");
            TiengAnh = double.Parse(Console.ReadLine()!);
        }

        public override double TinhTongDiem()
        {
            double tong = Bai1 + Bai2 + Bai3;
            if (TiengAnh >= 9 && TiengAnh <= 10) tong += 2;
            else if (TiengAnh >= 7 && TiengAnh <= 8) tong += 1;
            return tong;
        }

        public override void Output()
        {
            Console.WriteLine($"SBD: {Sbd}, Ho ten: {HoTen} (Chuyen), Tieng Anh: {TiengAnh}, Tong diem: {TinhTongDiem():0.00}");
        }
    }

    // Thi sinh Sieu cup: tong diem cua 4 bai (3 bai lap trinh + CSDL)
    public class ThiSinhSieuCup : ThiSinh
    {
        public double Csdl { get; set; }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhap diem CSDL: ");
            Csdl = double.Parse(Console.ReadLine()!);
        }

        public override double TinhTongDiem()
        {
            return Bai1 + Bai2 + Bai3 + Csdl;
        }

        public override void Output()
        {
            Console.WriteLine($"SBD: {Sbd}, Ho ten: {HoTen} (Sieu cup), CSDL: {Csdl}, Tong diem: {TinhTongDiem():0.00}");
        }
    }
}
