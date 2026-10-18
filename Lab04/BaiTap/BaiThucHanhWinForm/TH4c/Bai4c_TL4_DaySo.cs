using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập tại lớp, Bài 4: Nhập dãy số nguyên, tính tổng, tổng chẵn, tổng lẻ.
    /// </summary>
    public class Bai4c_TL4_DaySo : Form
    {
        private TextBox txtNhapSo, txtDay, txtTong, txtChan, txtLe;
        private Button btnNhap, btnTinhTong, btnTiepTuc, btnThoat;

        // Danh sách lưu các số đã nhập
        private readonly List<int> _dsSo = new List<int>();

        public Bai4c_TL4_DaySo()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Dãy số và Tính Tổng";
            Font = new Font("Times New Roman", 10.5F);
            ClientSize = new Size(420, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "Nhập Dãy Số và Tính Tổng",
                Font = new Font("Times New Roman", 14F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 12),
                Size = new Size(420, 32)
            };

            var lblNhap = UiHelper.TaoLabel("Nhập số :", 25, 62);
            txtNhapSo = UiHelper.TaoTextBox(110, 59, 90);
            btnNhap = UiHelper.TaoButton("Nhập", 225, 55, 85, 30);

            var lblDay = UiHelper.TaoLabel("Dãy vừa nhập :", 25, 100);
            txtDay = UiHelper.TaoTextBox(135, 97, 255, true);

            var lblTong = UiHelper.TaoLabel("Tổng các phần tử trong dãy :", 25, 135);
            txtTong = UiHelper.TaoTextBox(250, 132, 85, true);

            var lblChan = UiHelper.TaoLabel("Tổng Chẵn :", 25, 172);
            txtChan = UiHelper.TaoTextBox(115, 169, 70, true);
            var lblLe = UiHelper.TaoLabel("Tổng Lẻ :", 215, 172);
            txtLe = UiHelper.TaoTextBox(285, 169, 70, true);

            btnTinhTong = UiHelper.TaoButton("Tính tổng", 25, 215, 110, 32);
            btnTiepTuc = UiHelper.TaoButton("Tiếp Tục", 155, 215, 110, 32);
            btnThoat = UiHelper.TaoButton("Thoát", 285, 215, 110, 32);

            // Cho nhập số nguyên (có thể âm)
            txtNhapSo.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, false);
            AcceptButton = btnNhap;   // Enter = bấm nút Nhập

            btnNhap.Click += btnNhap_Click;
            btnTinhTong.Click += btnTinhTong_Click;
            btnTiepTuc.Click += btnTiepTuc_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, lblNhap, txtNhapSo, btnNhap, lblDay, txtDay,
                lblTong, txtTong, lblChan, txtChan, lblLe, txtLe, btnTinhTong, btnTiepTuc, btnThoat });
        }

        // Nhập: thêm số vừa gõ vào dãy rồi hiển thị cả dãy
        private void btnNhap_Click(object sender, EventArgs e)
        {
            if (!UiHelper.LaSoNguyen(txtNhapSo.Text, out int so))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapSo.Focus();
                return;
            }
            _dsSo.Add(so);
            txtDay.Text = string.Join(" ", _dsSo);
            txtNhapSo.Clear();
            txtNhapSo.Focus();
        }

        // Tính tổng cả dãy, tổng số chẵn, tổng số lẻ
        private void btnTinhTong_Click(object sender, EventArgs e)
        {
            if (_dsSo.Count == 0)
            {
                MessageBox.Show("Chưa có số nào trong dãy, hãy nhập số trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNhapSo.Focus();
                return;
            }

            long tong = 0, tongChan = 0, tongLe = 0;
            foreach (int so in _dsSo)
            {
                tong += so;
                if (so % 2 == 0) tongChan += so;   // chia hết cho 2 -> chẵn
                else tongLe += so;                 // còn lại -> lẻ (kể cả số âm lẻ)
            }
            txtTong.Text = tong.ToString();
            txtChan.Text = tongChan.ToString();
            txtLe.Text = tongLe.ToString();
        }

        // Tiếp tục: trả form về trạng thái ban đầu
        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            _dsSo.Clear();
            txtNhapSo.Clear(); txtDay.Clear(); txtTong.Clear(); txtChan.Clear(); txtLe.Clear();
            txtNhapSo.Focus();
        }
    }
}
