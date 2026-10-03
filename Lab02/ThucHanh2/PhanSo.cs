using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 1.4: Lop PhanSo - constructor, ToString, overload toan tu
    public class PhanSo
    {
        // Field
        private int tuSo;
        private int mauSo;

        // Property
        public int TuSo
        {
            get => tuSo;
            set => tuSo = value;
        }

        public int MauSo
        {
            get => mauSo;
            set
            {
                if (value == 0)
                    throw new ArgumentException("Mau so khong duoc bang 0.");
                mauSo = value;
            }
        }

        // Constructor mac nhien
        public PhanSo()
        {
            tuSo = 0;
            mauSo = 1;
        }

        // Constructor sao chep
        public PhanSo(PhanSo other)
        {
            tuSo = other.tuSo;
            mauSo = other.mauSo;
        }

        // Constructor day du (tu so, mau so)
        public PhanSo(int tuSo, int mauSo)
        {
            if (mauSo == 0)
                throw new ArgumentException("Mau so khong duoc bang 0.");
            this.tuSo = tuSo;
            this.mauSo = mauSo;
        }

        // Constructor chi co tu so (mau so mac dinh la 1)
        public PhanSo(int tuSo)
        {
            this.tuSo = tuSo;
            this.mauSo = 1;
        }

        // Override ToString de xuat phan so
        public override string ToString()
        {
            return $"{tuSo}/{mauSo}";
        }

        // Mot ngoi: +, -
        public static PhanSo operator +(PhanSo p)
        {
            return new PhanSo(p.tuSo, p.mauSo);
        }

        public static PhanSo operator -(PhanSo p)
        {
            return new PhanSo(-p.tuSo, p.mauSo);
        }

        // Hai ngoi: +, -, *, /
        public static PhanSo operator +(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.mauSo + b.tuSo * a.mauSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator -(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.mauSo - b.tuSo * a.mauSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator *(PhanSo a, PhanSo b)
        {
            return new PhanSo(a.tuSo * b.tuSo, a.mauSo * b.mauSo);
        }

        public static PhanSo operator /(PhanSo a, PhanSo b)
        {
            if (b.tuSo == 0)
                throw new DivideByZeroException("Khong the chia cho phan so bang 0.");
            return new PhanSo(a.tuSo * b.mauSo, a.mauSo * b.tuSo);
        }

        // Gia tri thuc de so sanh (quy ve cung 1 don vi)
        private static double GiaTri(PhanSo p)
        {
            return (double)p.tuSo / p.mauSo;
        }

        // So sanh: >, <, >=, <=, ==, !=
        public static bool operator >(PhanSo a, PhanSo b) => GiaTri(a) > GiaTri(b);
        public static bool operator <(PhanSo a, PhanSo b) => GiaTri(a) < GiaTri(b);
        public static bool operator >=(PhanSo a, PhanSo b) => GiaTri(a) >= GiaTri(b);
        public static bool operator <=(PhanSo a, PhanSo b) => GiaTri(a) <= GiaTri(b);
        public static bool operator ==(PhanSo a, PhanSo b) => GiaTri(a) == GiaTri(b);
        public static bool operator !=(PhanSo a, PhanSo b) => GiaTri(a) != GiaTri(b);

        // Bat buoc override khi da overload == va !=
        public override bool Equals(object? obj)
        {
            if (obj is not PhanSo other) return false;
            return this == other;
        }

        public override int GetHashCode()
        {
            return GiaTri(this).GetHashCode();
        }

        // Method: nhap phan so (kiem tra mau so khac 0)
        public void Input()
        {
            Console.Write("Nhap tu so: ");
            tuSo = int.Parse(Console.ReadLine()!);

            Console.Write("Nhap mau so: ");
            int m = int.Parse(Console.ReadLine()!);
            while (m == 0)
            {
                Console.Write("Mau so khong duoc bang 0, nhap lai: ");
                m = int.Parse(Console.ReadLine()!);
            }
            mauSo = m;
        }

        // Method: xuat phan so
        public void Output()
        {
            Console.WriteLine(ToString());
        }
    }
}
