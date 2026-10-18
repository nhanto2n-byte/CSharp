using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập mẫu, Bài 1: Cộng trừ nhân chia dùng RadioButton + class TinhToan.
    /// - Click chọn radio -> tính ngay và xuất vào ô Kết quả.
    /// - Click button Tính -> gọi các phương thức của class TinhToan, hiện kết quả bằng MessageBox.
    /// - Báo lỗi 2 mức: ErrorProvider và chặn nhập ký tự không phải số.
    /// - Hỏi xác nhận trước khi đóng form.
    /// </summary>
    public class Bai4d_Mau1_Radio : Form
    {
        private TextBox txt_a, txt_b, txt_ketqua;
        private RadioButton rdo_cong, rdo_tru, rdo_nhan, rdo_chia;
        private Button btn_Tinh;
        private ErrorProvider errorProvider1;

        public Bai4d_Mau1_Radio()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Cộng trừ nhân chia Radio";
            Font = new Font("Tahoma", 10F);
            ClientSize = new Size(440, 170);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            errorProvider1 = new ErrorProvider();

            var lblA = UiHelper.TaoLabel("a =", 20, 20);
            txt_a = UiHelper.TaoTextBox(55, 17, 120);
            var lblB = UiHelper.TaoLabel("b =", 215, 20);
            txt_b = UiHelper.TaoTextBox(250, 17, 170);
            var lblKq = UiHelper.TaoLabel("Kết quả", 20, 58);
            txt_ketqua = UiHelper.TaoTextBox(85, 55, 335, true);

            rdo_cong = UiHelper.TaoRadio("+", 90, 92);
            rdo_tru = UiHelper.TaoRadio("-", 170, 92);
            rdo_nhan = UiHelper.TaoRadio("x", 250, 92);
            rdo_chia = UiHelper.TaoRadio("/", 330, 92);
            btn_Tinh = UiHelper.TaoButton("Tính", 175, 122, 90, 32);

            // Mức 1: ErrorProvider; Mức 2: chặn ký tự không phải số
            txt_a.TextChanged += KiemTraSo_TextChanged;
            txt_b.TextChanged += KiemTraSo_TextChanged;
            txt_a.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, true);
            txt_b.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, true);

            // Cả 4 radio dùng chung 1 sự kiện CheckedChanged
            rdo_cong.CheckedChanged += Radio_CheckedChanged;
            rdo_tru.CheckedChanged += Radio_CheckedChanged;
            rdo_nhan.CheckedChanged += Radio_CheckedChanged;
            rdo_chia.CheckedChanged += Radio_CheckedChanged;

            btn_Tinh.Click += btn_Tinh_Click;
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblA, txt_a, lblB, txt_b, lblKq, txt_ketqua,
                rdo_cong, rdo_tru, rdo_nhan, rdo_chia, btn_Tinh });
        }

        private void KiemTraSo_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Length > 0 && !UiHelper.LaSoThuc(tb.Text, out _))
                errorProvider1.SetError(tb, "Đây không phải là số hợp lệ");
            else
                errorProvider1.SetError(tb, "");
        }

        // Chọn radio nào thì tính ngay phép toán đó và xuất vào ô Kết quả
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = (RadioButton)sender;
            if (!rdo.Checked) return;   // sự kiện cũng xảy ra ở radio vừa bị bỏ chọn -> bỏ qua

            if (!DocDuLieu(out float a, out float b)) return;
            var dt = new TinhToan(a, b);

            if (rdo_cong.Checked) txt_ketqua.Text = dt.Cong().ToString();
            else if (rdo_tru.Checked) txt_ketqua.Text = dt.Tru().ToString();
            else if (rdo_nhan.Checked) txt_ketqua.Text = dt.Nhan().ToString();
            else
            {
                if (b == 0) { txt_ketqua.Text = "Phép chia bị lỗi!"; return; }
                txt_ketqua.Text = dt.Chia().ToString();
            }
        }

        // Click Tính: gọi class TinhToan và hiện kết quả trong MessageBox
        private void btn_Tinh_Click(object sender, EventArgs e)
        {
            if (!DocDuLieu(out float a, out float b)) return;

            string s = "Kết quả là: \n";
            TinhToan dt = new TinhToan(a, b);

            if (rdo_cong.Checked)
                MessageBox.Show(s + a + "+" + b + " = " + dt.Cong());
            else if (rdo_tru.Checked)
                MessageBox.Show(s + a + "-" + b + " = " + dt.Tru());
            else if (rdo_nhan.Checked)
                MessageBox.Show(s + a + "*" + b + " = " + dt.Nhan());
            else if (rdo_chia.Checked)
            {
                if (b != 0)
                    MessageBox.Show(s + a + "/" + b + " = " + dt.Chia());
                else
                    MessageBox.Show("Phép chia bị lỗi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Vui lòng chọn một phép toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Đọc a, b từ TextBox; nếu sai thì báo lỗi bằng MessageBox và trả về false
        private bool DocDuLieu(out float a, out float b)
        {
            a = 0; b = 0;
            if (!UiHelper.LaSoThuc(txt_a.Text, out double da))
            {
                MessageBox.Show("Giá trị a không hợp lệ, vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_a.Focus();
                return false;
            }
            if (!UiHelper.LaSoThuc(txt_b.Text, out double db))
            {
                MessageBox.Show("Giá trị b không hợp lệ, vui lòng nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_b.Focus();
                return false;
            }
            a = (float)da;
            b = (float)db;
            return true;
        }
    }
}
