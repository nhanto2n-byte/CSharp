using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập tại lớp, Bài 3: Nhập 2 số nguyên dương a, b -> xuất UCLN và BCNN.
    /// Có kiểm tra dữ liệu nhập và báo lỗi.
    /// </summary>
    public class Bai4c_TL3_UCLN_BCNN : Form
    {
        private TextBox txtA, txtB, txtUCLN, txtBCNN;
        private Button btnThucHien, btnTiepTuc, btnThoat;

        public Bai4c_TL3_UCLN_BCNN()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Ước Số - Bội Số";
            Font = new Font("Times New Roman", 11F);
            ClientSize = new Size(380, 290);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "Ước Số Chung - Bội Số Chung",
                Font = new Font("Times New Roman", 15F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 15),
                Size = new Size(380, 35)
            };

            var lblA = UiHelper.TaoLabel("Nhập số a :", 40, 70);
            var lblB = UiHelper.TaoLabel("Nhập số b :", 40, 105);
            var lblUc = UiHelper.TaoLabel("Ước số chung lớn nhất :", 40, 140);
            var lblBc = UiHelper.TaoLabel("Bội số chung nhỏ nhất :", 40, 175);
            txtA = UiHelper.TaoTextBox(250, 67, 100);
            txtB = UiHelper.TaoTextBox(250, 102, 100);
            txtUCLN = UiHelper.TaoTextBox(250, 137, 100, true);   // chỉ đọc: chỉ để hiện kết quả
            txtBCNN = UiHelper.TaoTextBox(250, 172, 100, true);

            btnThucHien = UiHelper.TaoButton("Thực Hiện", 25, 225, 100, 35);
            btnTiepTuc = UiHelper.TaoButton("Tiếp Tục", 140, 225, 100, 35);
            btnThoat = UiHelper.TaoButton("Thoát", 255, 225, 100, 35);

            // Chỉ cho nhập chữ số (số nguyên dương)
            txtA.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e);
            txtB.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e);

            btnThucHien.Click += btnThucHien_Click;
            btnTiepTuc.Click += btnTiepTuc_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, lblA, lblB, lblUc, lblBc,
                txtA, txtB, txtUCLN, txtBCNN, btnThucHien, btnTiepTuc, btnThoat });
        }

        // Thuật toán Euclid: UCLN(a, b) = UCLN(b, a % b) cho đến khi b = 0
        private static long UCLN(long a, long b)
        {
            while (b != 0)
            {
                long r = a % b;
                a = b;
                b = r;
            }
            return a;
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu: phải là số nguyên dương
            if (!UiHelper.LaSoNguyen(txtA.Text, out int a) || a <= 0)
            {
                MessageBox.Show("Số a phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtA.Focus();
                return;
            }
            if (!UiHelper.LaSoNguyen(txtB.Text, out int b) || b <= 0)
            {
                MessageBox.Show("Số b phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtB.Focus();
                return;
            }

            long ucln = UCLN(a, b);
            long bcnn = (long)a / ucln * b;   // BCNN = a * b / UCLN (chia trước để đỡ tràn số)
            txtUCLN.Text = ucln.ToString();
            txtBCNN.Text = bcnn.ToString();
        }

        // Tiếp tục: trả form về trạng thái ban đầu
        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            txtA.Clear(); txtB.Clear(); txtUCLN.Clear(); txtBCNN.Clear();
            txtA.Focus();
        }
    }
}
