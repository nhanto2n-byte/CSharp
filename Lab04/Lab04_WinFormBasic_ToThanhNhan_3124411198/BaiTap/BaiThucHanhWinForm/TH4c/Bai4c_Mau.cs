using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập mẫu (mục 1.2): form "My Name Project".
    /// Nhập tên + năm sinh -> Show hiện tên, tuổi; Clear xóa; Exit hỏi xác nhận.
    /// Dùng ErrorProvider để báo lỗi.
    /// </summary>
    public class frmBaiTap1 : Form
    {
        private Label lblYourName, lblYear;
        private TextBox txtYourName, txtYear;
        private Button btnShow, btnClear, btnExit;
        private ErrorProvider errorProvider1;

        public frmBaiTap1()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            // --- Thuộc tính của form (theo bảng hướng dẫn) ---
            Text = "My name Project";
            Font = new Font("Tahoma", 11F);
            ClientSize = new Size(420, 190);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            errorProvider1 = new ErrorProvider();

            lblYourName = UiHelper.TaoLabel("Your Name:", 25, 28);
            lblYear = UiHelper.TaoLabel("Year of birth:", 25, 68);
            txtYourName = UiHelper.TaoTextBox(145, 25, 250);
            txtYear = UiHelper.TaoTextBox(145, 65, 250);
            txtYourName.Name = "txtYourName";
            txtYear.Name = "txtYear";

            btnShow = UiHelper.TaoButton("&Show", 25, 120, 100, 35);
            btnClear = UiHelper.TaoButton("&Clear", 160, 120, 100, 35);
            btnExit = UiHelper.TaoButton("E&xit", 295, 120, 100, 35);

            // Enter = Click btnShow, Esc = Click btnExit
            AcceptButton = btnShow;
            CancelButton = btnExit;

            // --- Thứ tự nhận tiêu điểm (Tab Order) giống hình hướng dẫn ---
            lblYourName.TabIndex = 0; txtYourName.TabIndex = 1;
            lblYear.TabIndex = 2; txtYear.TabIndex = 3;
            btnShow.TabIndex = 4; btnClear.TabIndex = 5; btnExit.TabIndex = 6;

            // --- Gắn sự kiện ---
            FormClosing += frmBaiTap1_FormClosing;
            txtYourName.Leave += txtYourName_Leave;
            txtYear.TextChanged += txtYear_TextChanged;
            btnShow.Click += btnShow_Click;
            btnClear.Click += btnClear_Click;
            btnExit.Click += btnExit_Click;

            Controls.AddRange(new Control[] { lblYourName, txtYourName, lblYear, txtYear, btnShow, btnClear, btnExit });
        }

        // Hỏi xác nhận khi form được đóng
        private void frmBaiTap1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UiHelper.XacNhanDong(e);
        }

        // Kiểm tra TextBox YourName đã nhập nội dung chưa (khi mất tiêu điểm)
        private void txtYourName_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Trim().Length == 0)
                errorProvider1.SetError(ctr, "You must enter Your Name");
            else
                errorProvider1.SetError(ctr, "");
        }

        // Năm sinh phải là số: nhập sai thì báo lỗi bằng ErrorProvider
        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (ctr.Text.Length > 0 && !int.TryParse(ctr.Text, out _))
                errorProvider1.SetError(ctr, "This is not a valid number");
            else
                errorProvider1.SetError(ctr, "");
        }

        // Show: hiện tên và tuổi (năm hiện tại - năm sinh) trong MessageBox
        private void btnShow_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trước khi tính để tránh lỗi Convert
            if (txtYourName.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtYourName, "You must enter Your Name");
                txtYourName.Focus();
                return;
            }
            if (!int.TryParse(txtYear.Text, out int namSinh))
            {
                errorProvider1.SetError(txtYear, "This is not a valid number");
                txtYear.Focus();
                return;
            }

            int age = DateTime.Now.Year - namSinh;
            string s = "My name is: " + txtYourName.Text + "\n";
            s = s + "Age: " + age.ToString();
            MessageBox.Show(s);
        }

        // Clear: xóa 2 TextBox, đặt con trỏ vào YourName
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtYourName.Clear();
            txtYear.Clear();
            errorProvider1.Clear();
            txtYourName.Focus();
        }

        // Exit: đóng form (FormClosing sẽ hỏi xác nhận Yes/No)
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
