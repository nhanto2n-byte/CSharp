using System;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// Class PhuongTrinhBacHai (4d - tại lớp, Bài 1).
    /// Lưu hệ số a, b, c và giải phương trình bậc nhất (ax + b = 0)
    /// hoặc bậc hai (ax^2 + bx + c = 0). Kết quả trả về là chuỗi để form hiển thị.
    /// </summary>
    public class PhuongTrinhBacHai
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public PhuongTrinhBacHai() { }

        public PhuongTrinhBacHai(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>Giải ax + b = 0 (dùng A và B).</summary>
        public string GiaiBacNhat()
        {
            return GiaiBacNhat(A, B);
        }

        // Hàm dùng chung: giải hx + k = 0
        private static string GiaiBacNhat(double h, double k)
        {
            if (h == 0)
            {
                if (k == 0) return "Phương trình có vô số nghiệm";
                return "Phương trình vô nghiệm";
            }
            return "Phương trình có nghiệm x = " + (-k / h).ToString("0.00");
        }

        /// <summary>Giải ax^2 + bx + c = 0 (dùng A, B, C).</summary>
        public string GiaiBacHai()
        {
            // a = 0 thì phương trình trở thành bx + c = 0
            if (A == 0) return GiaiBacNhat(B, C);

            double delta = B * B - 4 * A * C;
            if (delta < 0)
                return "Phương trình vô nghiệm";
            if (delta == 0)
                return "Phương trình có nghiệm kép x1 = x2 = " + (-B / (2 * A)).ToString("0.00");

            double x1 = (-B + Math.Sqrt(delta)) / (2 * A);
            double x2 = (-B - Math.Sqrt(delta)) / (2 * A);
            return "Phương trình có 2 nghiệm phân biệt: x1 = " + x1.ToString("0.00") + "; x2 = " + x2.ToString("0.00");
        }
    }
}
