using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập tại lớp, Bài 1: Cộng trừ nhân chia hai số a, b.
    /// Mức 1: báo lỗi bằng ErrorProvider.
    /// Mức 2: chặn nhập ký tự không phải số, báo lỗi bằng MessageBox, hỏi xác nhận khi đóng form.
    /// </summary>
    public class Bai4c_TL1_PhepTinh : Form
    {
        private TextBox txtA, txtB, txtKetQua;
        private Button btnCong, btnTru, btnNhan, btnChia;
        private ErrorProvider errorProvider1;

        public Bai4c_TL1_PhepTinh()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Cộng trừ nhân chia";
            Font = new Font("Tahoma", 10F);
            ClientSize = new Size(440, 150);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            errorProvider1 = new ErrorProvider();

            var lblA = UiHelper.TaoLabel("a =", 20, 20);
            txtA = UiHelper.TaoTextBox(55, 17, 120);
            var lblB = UiHelper.TaoLabel("b =", 215, 20);
            txtB = UiHelper.TaoTextBox(250, 17, 170);
            var lblKq = UiHelper.TaoLabel("Kết quả", 20, 58);
            txtKetQua = UiHelper.TaoTextBox(85, 55, 335, true);

            btnCong = UiHelper.TaoButton("+", 20, 95, 90, 35);
            btnTru = UiHelper.TaoButton("-", 125, 95, 90, 35);
            btnNhan = UiHelper.TaoButton("x", 230, 95, 90, 35);
            btnChia = UiHelper.TaoButton("/", 335, 95, 85, 35);

            // Mức 1: ErrorProvider khi nhập sai
            txtA.TextChanged += KiemTraSo_TextChanged;
            txtB.TextChanged += KiemTraSo_TextChanged;

            // Mức 2: chặn không cho nhập ký tự không phải số
            txtA.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, true);
            txtB.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, true);

            btnCong.Click += btnPhepToan_Click;
            btnTru.Click += btnPhepToan_Click;
            btnNhan.Click += btnPhepToan_Click;
            btnChia.Click += btnPhepToan_Click;

            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblA, txtA, lblB, txtB, lblKq, txtKetQua, btnCong, btnTru, btnNhan, btnChia });
        }

        // Mức 1: nếu nội dung không phải số thì ErrorProvider báo lỗi
        private void KiemTraSo_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Length > 0 && !UiHelper.LaSoThuc(tb.Text, out _))
                errorProvider1.SetError(tb, "Đây không phải là số hợp lệ");
            else
                errorProvider1.SetError(tb, "");
        }

        // Một handler chung cho 4 button, phân biệt bằng Text của button
        private void btnPhepToan_Click(object sender, EventArgs e)
        {
            // Mức 2: báo lỗi bằng MessageBox nếu dữ liệu không phù hợp
            if (!UiHelper.LaSoThuc(txtA.Text, out double a))
            {
                MessageBox.Show("Giá trị a không hợp lệ, vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtA.Focus();
                return;
            }
            if (!UiHelper.LaSoThuc(txtB.Text, out double b))
            {
                MessageBox.Show("Giá trị b không hợp lệ, vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtB.Focus();
                return;
            }

            string pt = ((Button)sender).Text;
            double kq;
            switch (pt)
            {
                case "+": kq = a + b; break;
                case "-": kq = a - b; break;
                case "x": kq = a * b; break;
                default: // phép chia
                    if (b == 0)
                    {
                        MessageBox.Show("Không thể chia cho 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtB.Focus();
                        return;
                    }
                    kq = a / b;
                    break;
            }
            txtKetQua.Text = kq.ToString("0.######");
        }
    }
}
