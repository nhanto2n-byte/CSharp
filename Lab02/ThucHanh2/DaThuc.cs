using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 2.5 (PDF ghi la "Bai 2.3" - trung so voi bai mang 1 chieu, day la loi danh so cua de goc):
    // Lop DaThuc gom n+1 don thuc: P(x) = a0.x^0 + a1.x^1 + ... + an.x^n
    public class DaThuc
    {
        // He so, heSo[i] la he so cua x^i
        private double[] heSo;

        // Constructor mac dinh: da thuc bac 0, he so 0
        public DaThuc()
        {
            heSo = new double[1];
        }

        // Constructor theo bac n (co n+1 he so)
        public DaThuc(int bac)
        {
            heSo = new double[bac + 1];
        }

        // Constructor sao chep
        public DaThuc(DaThuc other)
        {
            heSo = new double[other.heSo.Length];
            Array.Copy(other.heSo, heSo, heSo.Length);
        }

        public int Bac => heSo.Length - 1;

        // Indexer de truy cap don thuc thu i (dung lai lop DonThuc cua Bai 1.5)
        public DonThuc this[int i]
        {
            get => new DonThuc(heSo[i], i);
            set => heSo[i] = value.HeSo;
        }

        // Nhap da thuc
        public void Input()
        {
            Console.Write("Nhap bac cua da thuc (n): ");
            int n = int.Parse(Console.ReadLine()!);
            heSo = new double[n + 1];
            for (int i = 0; i <= n; i++)
            {
                Console.Write($"Nhap he so a{i} (cua x^{i}): ");
                heSo[i] = double.Parse(Console.ReadLine()!);
            }
        }

        // Xuat da thuc
        public void Output()
        {
            Console.Write("P(x) =");
            for (int i = 0; i < heSo.Length; i++)
            {
                Console.Write(i == 0 ? $" {heSo[i]}" : $" + {heSo[i]}.x^{i}");
            }
            Console.WriteLine();
        }

        // Tinh gia tri da thuc tai x
        public double TinhGiaTri(double x)
        {
            double ketQua = 0;
            for (int i = 0; i < heSo.Length; i++)
            {
                ketQua += heSo[i] * Math.Pow(x, i);
            }
            return ketQua;
        }
    }
}
