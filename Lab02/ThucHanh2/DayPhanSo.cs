using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.6 (PDF ghi la "Bai 2.4" - trung so, day la loi danh so cua de goc):
    // Lop DayPhanSo chua n phan so, dung lai lop PhanSo cua Bai 1.4
    public class DayPhanSo
    {
        private PhanSo[] mang;

        public DayPhanSo()
        {
            mang = Array.Empty<PhanSo>();
        }

        public int SoLuong => mang.Length;

        public PhanSo this[int i]
        {
            get => mang[i];
            set => mang[i] = value;
        }

        public void Input()
        {
            Console.Write("Nhap so luong phan so: ");
            int n = int.Parse(Console.ReadLine()!);

            mang = new PhanSo[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Phan so thu {i} --");
                mang[i] = new PhanSo();
                mang[i].Input();
            }
        }

        public void Output()
        {
            for (int i = 0; i < mang.Length; i++)
            {
                Console.WriteLine($"Phan so {i}: {mang[i]}");
            }
        }

        // Tinh tong cua n phan so trong day
        public PhanSo TinhTong()
        {
            PhanSo tong = new PhanSo(0, 1);
            foreach (PhanSo p in mang)
            {
                tong = tong + p;
            }
            return tong;
        }
    }
}
