using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập tại lớp, Bài 1: Giải phương trình bậc nhất / bậc hai.
    /// - Form load: nút Giải bị mờ.
    /// - Chọn bậc nhất: ô c bị mờ. Chọn bậc hai: hiện đủ 3 ô.
    /// - Nhập đủ dữ liệu thì nút Giải mới sáng; giải xong nút Giải lại mờ.
    /// - Dùng class PhuongTrinhBacHai. Đóng form phải xác nhận.
    /// </summary>
    public class Bai4d_TL1_PhuongTrinh : Form
    {
        private RadioButton rdoBacNhat, rdoBacHai;
        private Label lblC;
        private TextBox txtA, txtB, txtC, txtKetQua;
        private Button btnGiai, btnThoat;
        private ErrorProvider errorProvider1;

        public Bai4d_TL1_PhuongTrinh()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Giải phương trình bậc 1-2";
            Font = new Font("Times New Roman", 10.5F);
            ClientSize = new Size(380, 380);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            errorProvider1 = new ErrorProvider();

            var lblTieuDe = new Label
            {
                Text = "GIẢI PHƯƠNG TRÌNH",
                Font = new Font("Times New Roman", 17F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 10),
                Size = new Size(380, 36)
            };

            var grp = UiHelper.TaoGroupBox("Bạn vui lòng chọn", 15, 55, 350, 85);
            rdoBacNhat = UiHelper.TaoRadio("Phương trình bậc nhất", 25, 25);
            rdoBacHai = UiHelper.TaoRadio("Phương trình bậc hai", 25, 52);
            grp.Controls.AddRange(new Control[] { rdoBacNhat, rdoBacHai });

            var lblA = UiHelper.TaoLabel("Nhập a", 20, 165);
            var lblB = UiHelper.TaoLabel("Nhập b", 20, 200);
            lblC = UiHelper.TaoLabel("Nhập c", 20, 235);
            txtA = UiHelper.TaoTextBox(95, 162, 140);
            txtB = UiHelper.TaoTextBox(95, 197, 140);
            txtC = UiHelper.TaoTextBox(95, 232, 140);
            txtA.TextAlign = txtB.TextAlign = txtC.TextAlign = HorizontalAlignment.Right;

            btnGiai = UiHelper.TaoButton("Giải", 255, 158, 110, 42);
            btnThoat = UiHelper.TaoButton("Thoát", 255, 208, 110, 42);

            var lblKq = UiHelper.TaoLabel("Kết quả", 20, 285);
            txtKetQua = UiHelper.TaoTextBox(95, 282, 270, true);
            txtKetQua.Multiline = true;
            txtKetQua.Height = 60;

            // --- Sự kiện ---
            rdoBacNhat.CheckedChanged += Radio_CheckedChanged;
            rdoBacHai.CheckedChanged += Radio_CheckedChanged;
            foreach (var tb in new[] { txtA, txtB, txtC })
            {
                tb.TextChanged += TextBox_TextChanged;
                tb.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, true);
            }
            btnGiai.Click += btnGiai_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, grp, lblA, txtA, lblB, txtB, lblC, txtC,
                btnGiai, btnThoat, lblKq, txtKetQua });

            // Trạng thái ban đầu: chọn bậc nhất, nút Giải mờ
            rdoBacNhat.Checked = true;
            CapNhatNutGiai();
        }

        // Đổi loại phương trình: ô c bị mờ (bậc nhất) hoặc sáng (bậc hai)
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked) return;

            bool bacHai = rdoBacHai.Checked;
            txtC.Enabled = bacHai;
            lblC.Enabled = bacHai;
            if (!bacHai) { txtC.Clear(); errorProvider1.SetError(txtC, ""); }

            txtKetQua.Clear();
            CapNhatNutGiai();
        }

        // Sửa dữ liệu -> kiểm tra lỗi, xóa kết quả cũ và bật/tắt nút Giải
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Length > 0 && !UiHelper.LaSoThuc(tb.Text, out _))
                errorProvider1.SetError(tb, "Đây không phải là số hợp lệ");
            else
                errorProvider1.SetError(tb, "");

            txtKetQua.Clear();
            CapNhatNutGiai();
        }

        // Nút Giải chỉ sáng khi các ô cần thiết đều chứa số hợp lệ
        private void CapNhatNutGiai()
        {
            bool hopLe = UiHelper.LaSoThuc(txtA.Text, out _) && UiHelper.LaSoThuc(txtB.Text, out _);
            if (rdoBacHai.Checked)
                hopLe = hopLe && UiHelper.LaSoThuc(txtC.Text, out _);
            btnGiai.Enabled = hopLe;
        }

        private void btnGiai_Click(object sender, EventArgs e)
        {
            // Kiểm tra lại cho chắc, báo lỗi nếu cần
            if (!UiHelper.LaSoThuc(txtA.Text, out double a) || !UiHelper.LaSoThuc(txtB.Text, out double b))
            {
                MessageBox.Show("Hệ số a, b phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rdoBacNhat.Checked)
            {
                var pt = new PhuongTrinhBacHai(a, b, 0);
                txtKetQua.Text = pt.GiaiBacNhat();
            }
            else
            {
                if (!UiHelper.LaSoThuc(txtC.Text, out double c))
                {
                    MessageBox.Show("Hệ số c phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var pt = new PhuongTrinhBacHai(a, b, c);
                txtKetQua.Text = pt.GiaiBacHai();
            }

            // Giải xong thì nút Giải mờ đi (sáng lại khi người dùng sửa dữ liệu)
            btnGiai.Enabled = false;
        }
    }
}
