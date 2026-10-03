using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.1: Lop SinhVien - Field, Constructor, Property, Method
    // Implement IComparable de dung cho Bai 3.1 (Array.Sort) va Bai 3.2 (sap xep bang interface)
    public class SinhVien : IComparable<SinhVien>
    {
        // Field
        private string hoTen;
        private int namSinh;

        // Property
        public string HoTen
        {
            get => hoTen;
            set => hoTen = value;
        }

        public int NamSinh
        {
            get => namSinh;
            set => namSinh = value;
        }

        // Constructor mac dinh
        public SinhVien()
        {
            hoTen = "";
            namSinh = 0;
        }

        // Constructor co tham so
        public SinhVien(string hoTen, int namSinh)
        {
            this.hoTen = hoTen;
            this.namSinh = namSinh;
        }

        // Method: nhap thong tin
        public void Nhap()
        {
            Console.Write("Nhap ho ten sinh vien: ");
            hoTen = Console.ReadLine() ?? "";
            Console.Write("Nhap nam sinh: ");
            namSinh = int.Parse(Console.ReadLine()!);
        }

        // Method: tinh tuoi dua tren nam hien tai
        public int TinhTuoi()
        {
            return DateTime.Now.Year - namSinh;
        }

        // Method: xuat thong tin va tuoi
        public void Xuat()
        {
            Console.WriteLine($"Ho ten: {hoTen}, Nam sinh: {namSinh}, Tuoi: {TinhTuoi()}");
        }

        // CompareTo: sap xep theo ho ten (tang dan)
        public int CompareTo(SinhVien? other)
        {
            if (other == null) return 1;
            return string.Compare(hoTen, other.hoTen, StringComparison.Ordinal);
        }
    }
}
