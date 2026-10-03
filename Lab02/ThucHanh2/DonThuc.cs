using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.5: Lop DonThuc - P(x) = a.x^n
    public class DonThuc
    {
        // Field
        private double heSo; // a
        private int soMu;    // n (nguyen khong am)

        // Property
        public double HeSo
        {
            get => heSo;
            set => heSo = value;
        }

        public int SoMu
        {
            get => soMu;
            set
            {
                if (value < 0)
                    throw new ArgumentException("So mu phai la so nguyen khong am.");
                soMu = value;
            }
        }

        // Default constructor
        public DonThuc()
        {
            heSo = 0;
            soMu = 0;
        }

        // Constructor co tham so
        public DonThuc(double heSo, int soMu)
        {
            if (soMu < 0)
                throw new ArgumentException("So mu phai la so nguyen khong am.");
            this.heSo = heSo;
            this.soMu = soMu;
        }

        // Override ToString de xuat don thuc
        public override string ToString()
        {
            return $"{heSo}.x^{soMu}";
        }

        // (a) Tinh gia tri don thuc P(x) tai 1 diem x cho truoc
        public double TinhGiaTri(double x)
        {
            return heSo * Math.Pow(x, soMu);
        }

        // (b) Dao ham don thuc: Q(x) = P'(x) = a.n.x^(n-1)
        public DonThuc DaoHam()
        {
            if (soMu == 0)
            {
                // Dao ham cua hang so la 0
                return new DonThuc(0, 0);
            }
            return new DonThuc(heSo * soMu, soMu - 1);
        }

        // Method: nhap he so va so mu
        public void Input()
        {
            Console.Write("Nhap he so a: ");
            heSo = double.Parse(Console.ReadLine()!);

            Console.Write("Nhap so mu n (nguyen khong am): ");
            int n = int.Parse(Console.ReadLine()!);
            while (n < 0)
            {
                Console.Write("So mu phai la so nguyen khong am, nhap lai: ");
                n = int.Parse(Console.ReadLine()!);
            }
            soMu = n;
        }

        // Method: xuat don thuc
        public void Output()
        {
            Console.WriteLine(ToString());
        }
    }
}
