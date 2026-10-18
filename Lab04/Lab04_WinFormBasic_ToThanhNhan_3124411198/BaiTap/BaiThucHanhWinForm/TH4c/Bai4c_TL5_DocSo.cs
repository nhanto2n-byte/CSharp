using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập tại lớp, Bài 5: Đọc số nguyên dương từ 1 đến 999 thành chữ.
    /// </summary>
    public class Bai4c_TL5_DocSo : Form
    {
        private TextBox txtSo, txtChu;
        private Button btnThucHien, btnXoa, btnThoat;

        private static readonly string[] ChuSo =
            { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        public Bai4c_TL5_DocSo()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Đọc số thành chữ";
            Font = new Font("Tahoma", 10.5F);
            ClientSize = new Size(440, 230);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "ĐỌC SỐ THÀNH CHỮ",
                Font = new Font("Tahoma", 14F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 12),
                Size = new Size(440, 32)
            };

            var lblSo = UiHelper.TaoLabel("Nhập số (1 - 999):", 25, 62);
            txtSo = UiHelper.TaoTextBox(185, 59, 100);
            txtSo.MaxLength = 3;                       // tối đa 3 chữ số
            var lblChu = UiHelper.TaoLabel("Đọc thành chữ:", 25, 100);
            txtChu = UiHelper.TaoTextBox(25, 125, 390, true);

            btnThucHien = UiHelper.TaoButton("Thực hiện", 25, 170, 120, 35);
            btnXoa = UiHelper.TaoButton("Xóa", 160, 170, 120, 35);
            btnThoat = UiHelper.TaoButton("Thoát", 295, 170, 120, 35);

            txtSo.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e);   // chỉ nhập chữ số
            AcceptButton = btnThucHien;

            btnThucHien.Click += btnThucHien_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, lblSo, txtSo, lblChu, txtChu, btnThucHien, btnXoa, btnThoat });
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (!UiHelper.LaSoNguyen(txtSo.Text, out int n) || n < 1 || n > 999)
            {
                MessageBox.Show("Vui lòng nhập một số nguyên dương từ 1 đến 999!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo.Focus();
                txtSo.SelectAll();
                return;
            }
            txtChu.Text = DocSo(n);
        }

        // Xóa: trả form về trạng thái ban đầu
        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtSo.Clear();
            txtChu.Clear();
            txtSo.Focus();
        }

        /// <summary>
        /// Đọc số 1..999 thành chữ. Tách số thành trăm - chục - đơn vị rồi ghép lại.
        /// Các quy tắc đặc biệt: "lẻ" (105), "mười" (1x), "mốt" (21, 31...), "lăm" (15, 25...).
        /// </summary>
        public static string DocSo(int n)
        {
            int tram = n / 100;
            int chuc = n / 10 % 10;
            int donvi = n % 10;
            var kq = new List<string>();

            if (tram > 0) { kq.Add(ChuSo[tram]); kq.Add("trăm"); }

            if (chuc > 1) { kq.Add(ChuSo[chuc]); kq.Add("mươi"); }
            else if (chuc == 1) kq.Add("mười");
            else if (tram > 0 && donvi > 0) kq.Add("lẻ");   // vd: 105 = một trăm lẻ năm

            if (donvi > 0)
            {
                if (chuc > 1 && donvi == 1) kq.Add("mốt");        // 21 = hai mươi mốt
                else if (chuc >= 1 && donvi == 5) kq.Add("lăm");  // 15 = mười lăm, 25 = hai mươi lăm
                else kq.Add(ChuSo[donvi]);
            }

            string s = string.Join(" ", kq);
            return char.ToUpper(s[0]) + s.Substring(1);   // viết hoa chữ cái đầu
        }
    }
}
