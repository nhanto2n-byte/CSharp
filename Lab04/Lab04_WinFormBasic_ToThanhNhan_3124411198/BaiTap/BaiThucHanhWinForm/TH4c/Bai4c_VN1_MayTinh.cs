using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập về nhà, Bài 1: Máy tính bỏ túi đơn giản (cộng, trừ, nhân, chia, xóa).
    /// </summary>
    public class Bai4c_VN1_MayTinh : Form
    {
        private TextBox txtManHinh;

        private double _soTruoc = 0;          // số đã nhập trước toán tử
        private string _phepToan = null;      // toán tử đang chờ: + - × ÷
        private bool _nhapSoMoi = true;       // true: lần bấm số kế tiếp sẽ bắt đầu số mới

        public Bai4c_VN1_MayTinh()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Máy tính bỏ túi";
            Font = new Font("Tahoma", 12F);
            ClientSize = new Size(300, 360);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            txtManHinh = new TextBox
            {
                Text = "0",
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Tahoma", 20F, FontStyle.Bold),
                Location = new Point(15, 15),
                Size = new Size(270, 40)
            };
            Controls.Add(txtManHinh);

            // Hàng đầu: nút xóa
            TaoNut("C", 0, 0, 2);      // xóa tất cả (rộng 2 ô)
            TaoNut("⌫", 2, 0, 1);      // xóa 1 ký tự cuối
            TaoNut("÷", 3, 0, 1);

            // Các hàng còn lại, bố cục 4 cột
            string[,] bang =
            {
                { "7", "8", "9", "×" },
                { "4", "5", "6", "-" },
                { "1", "2", "3", "+" },
                { "0", ".", "=", "" }
            };
            for (int h = 0; h < 4; h++)
                for (int c = 0; c < 4; c++)
                    if (bang[h, c] != "")
                        TaoNut(bang[h, c], c, h + 1, 1);

            FormClosing += (s, e) => UiHelper.XacNhanDong(e);
        }

        // Tạo nút tại ô (cot, hang) của lưới, rộng "rong" ô
        private void TaoNut(string chu, int cot, int hang, int rong)
        {
            var btn = new Button
            {
                Text = chu,
                Location = new Point(15 + cot * 68, 75 + hang * 55),
                Size = new Size(rong * 68 - 6, 49),
                Font = new Font("Tahoma", 14F)
            };
            if ("÷×-+=".Contains(chu)) btn.BackColor = Color.LightSteelBlue;
            if (chu == "C" || chu == "⌫") btn.BackColor = Color.LightCoral;
            btn.Click += Nut_Click;
            Controls.Add(btn);
        }

        // Một handler chung cho mọi nút, phân loại theo Text
        private void Nut_Click(object sender, EventArgs e)
        {
            string chu = ((Button)sender).Text;

            if (chu.Length == 1 && char.IsDigit(chu[0]))
                NhapChuSo(chu);
            else if (chu == ".")
                NhapDauThapPhan();
            else if (chu == "C")
                XoaTatCa();
            else if (chu == "⌫")
                XoaMotKyTu();
            else if (chu == "=")
                BamBang();
            else
                BamToanTu(chu);
        }

        private double GiaTriManHinh()
        {
            return double.Parse(txtManHinh.Text, CultureInfo.InvariantCulture);
        }

        private void HienKetQua(double so)
        {
            txtManHinh.Text = so.ToString("0.##########", CultureInfo.InvariantCulture);
        }

        private void NhapChuSo(string so)
        {
            if (_nhapSoMoi || txtManHinh.Text == "0")
                txtManHinh.Text = so;
            else
                txtManHinh.Text += so;
            _nhapSoMoi = false;
        }

        private void NhapDauThapPhan()
        {
            if (_nhapSoMoi)
            {
                txtManHinh.Text = "0.";
                _nhapSoMoi = false;
            }
            else if (!txtManHinh.Text.Contains("."))
                txtManHinh.Text += ".";
        }

        private void XoaTatCa()
        {
            txtManHinh.Text = "0";
            _soTruoc = 0;
            _phepToan = null;
            _nhapSoMoi = true;
        }

        private void XoaMotKyTu()
        {
            if (_nhapSoMoi) return;   // đang hiện kết quả thì không xóa từng ký tự
            string s = txtManHinh.Text;
            s = s.Length > 1 ? s.Substring(0, s.Length - 1) : "0";
            if (s == "-") s = "0";
            txtManHinh.Text = s;
        }

        // Bấm + - × ÷ : nếu đã có phép toán chờ thì tính trước (tính liên tiếp 2+3+4)
        private void BamToanTu(string toanTu)
        {
            if (_phepToan != null && !_nhapSoMoi)
            {
                if (!TinhKetQua()) return;
            }
            _soTruoc = GiaTriManHinh();
            _phepToan = toanTu;
            _nhapSoMoi = true;
        }

        private void BamBang()
        {
            if (_phepToan == null) return;
            if (TinhKetQua())
            {
                _phepToan = null;
                _nhapSoMoi = true;
            }
        }

        // Thực hiện _soTruoc (phép toán) số-trên-màn-hình; trả về false nếu lỗi
        private bool TinhKetQua()
        {
            double so2 = GiaTriManHinh();
            double kq;
            switch (_phepToan)
            {
                case "+": kq = _soTruoc + so2; break;
                case "-": kq = _soTruoc - so2; break;
                case "×": kq = _soTruoc * so2; break;
                default:  // ÷
                    if (so2 == 0)
                    {
                        MessageBox.Show("Không thể chia cho 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        XoaTatCa();
                        return false;
                    }
                    kq = _soTruoc / so2;
                    break;
            }
            HienKetQua(kq);
            _soTruoc = kq;
            return true;
        }
    }
}
