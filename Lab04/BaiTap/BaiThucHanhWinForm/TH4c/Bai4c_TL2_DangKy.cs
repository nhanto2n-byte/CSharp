using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập tại lớp, Bài 2: Form đăng ký tài khoản.
    /// - Kiểm tra định dạng email khi rời khỏi ô Địa chỉ email.
    /// - Bắt buộc nhập các ô có (*).
    /// - Nhấn Đăng ký hoặc Enter ở ô Xác nhận mật khẩu -> hiện thông tin trong MessageBox.
    /// - Hỏi xác nhận trước khi đóng form.
    /// </summary>
    public class Bai4c_TL2_DangKy : Form
    {
        private TextBox txtTenDN, txtEmail, txtMatKhau, txtXacNhan;
        private Button btnDangKy;
        private ErrorProvider errorProvider1;

        public Bai4c_TL2_DangKy()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Đăng ký tài khoản";
            Font = new Font("Tahoma", 9F);
            ClientSize = new Size(400, 280);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            errorProvider1 = new ErrorProvider();

            var lblTieuDe = new Label
            {
                Text = "Đăng ký tài khoản",
                Font = new Font("Tahoma", 13F, FontStyle.Bold),
                ForeColor = Color.DodgerBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 12),
                Size = new Size(400, 30)
            };

            var lblTen = UiHelper.TaoLabel("Tên đăng nhập", 20, 62);
            var lblEmail = UiHelper.TaoLabel("Địa chỉ email", 20, 97);
            var lblMk = UiHelper.TaoLabel("Mật khẩu", 20, 132);
            var lblXn = UiHelper.TaoLabel("Xác nhận mật khẩu", 20, 167);
            txtTenDN = UiHelper.TaoTextBox(150, 59, 190);
            txtEmail = UiHelper.TaoTextBox(150, 94, 190);
            txtMatKhau = UiHelper.TaoTextBox(150, 129, 190);
            txtXacNhan = UiHelper.TaoTextBox(150, 164, 190);
            txtMatKhau.UseSystemPasswordChar = true;   // ẩn mật khẩu
            txtXacNhan.UseSystemPasswordChar = true;

            // Dấu (*) cho các ô bắt buộc
            var s1 = UiHelper.TaoLabel("(*)", 348, 62);
            var s2 = UiHelper.TaoLabel("(*)", 348, 97);
            var s3 = UiHelper.TaoLabel("(*)", 348, 132);

            btnDangKy = UiHelper.TaoButton("Đăng ký", 150, 205, 190, 45);
            btnDangKy.ForeColor = Color.DodgerBlue;
            btnDangKy.Font = new Font("Tahoma", 9F, FontStyle.Bold);

            // --- Sự kiện ---
            txtEmail.Leave += txtEmail_Leave;                 // kiểm tra email khi ra khỏi ô
            txtTenDN.Leave += KiemTraBatBuoc_Leave;           // ô (*) không được để trống
            txtMatKhau.Leave += KiemTraBatBuoc_Leave;
            txtXacNhan.KeyDown += txtXacNhan_KeyDown;         // Enter ở ô xác nhận = Đăng ký
            btnDangKy.Click += btnDangKy_Click;
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, lblTen, lblEmail, lblMk, lblXn,
                txtTenDN, txtEmail, txtMatKhau, txtXacNhan, s1, s2, s3, btnDangKy });
        }

        // Kiểm tra định dạng email: có dạng ten@ten.mienten
        private bool EmailHopLe(string email)
        {
            return Regex.IsMatch(email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim().Length == 0)
                errorProvider1.SetError(txtEmail, "Vui lòng nhập địa chỉ email");
            else if (!EmailHopLe(txtEmail.Text))
                errorProvider1.SetError(txtEmail, "Email không đúng định dạng (vd: abc@gmail.com)");
            else
                errorProvider1.SetError(txtEmail, "");
        }

        // Dùng chung cho các ô bắt buộc nhập
        private void KiemTraBatBuoc_Leave(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Trim().Length == 0)
                errorProvider1.SetError(tb, "Ô này bắt buộc phải nhập");
            else
                errorProvider1.SetError(tb, "");
        }

        private void txtXacNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // bỏ tiếng "ding" của phím Enter
                DangKy();
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            DangKy();
        }

        // Kiểm tra toàn bộ dữ liệu rồi mới hiển thị thông tin
        private void DangKy()
        {
            // Kích hoạt kiểm tra cho các ô (*)
            KiemTraBatBuoc_Leave(txtTenDN, EventArgs.Empty);
            KiemTraBatBuoc_Leave(txtMatKhau, EventArgs.Empty);
            txtEmail_Leave(txtEmail, EventArgs.Empty);

            if (txtTenDN.Text.Trim().Length == 0) { txtTenDN.Focus(); return; }
            if (txtEmail.Text.Trim().Length == 0 || !EmailHopLe(txtEmail.Text)) { txtEmail.Focus(); return; }
            if (txtMatKhau.Text.Length == 0) { txtMatKhau.Focus(); return; }

            // Mật khẩu xác nhận phải trùng mật khẩu
            if (txtXacNhan.Text != txtMatKhau.Text)
            {
                errorProvider1.SetError(txtXacNhan, "Mật khẩu xác nhận không khớp");
                txtXacNhan.Focus();
                return;
            }
            errorProvider1.SetError(txtXacNhan, "");

            string s = "Tên đăng nhập: " + txtTenDN.Text + "\n"
                     + "Địa chỉ email: " + txtEmail.Text + "\n"
                     + "Mật khẩu: " + txtMatKhau.Text + "\n"
                     + "Xác nhận mật khẩu: " + txtXacNhan.Text;
            MessageBox.Show(s, "Thông tin đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
