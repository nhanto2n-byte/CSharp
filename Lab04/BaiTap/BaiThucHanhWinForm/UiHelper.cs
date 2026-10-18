using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BaiThucHanhWinForm
{
    /// <summary>
    /// Các hàm dùng chung: tạo control bằng code và kiểm tra dữ liệu nhập.
    /// (Vì không dùng Designer nên mỗi form tự dựng giao diện bằng code.)
    /// </summary>
    public static class UiHelper
    {
        // ---------- Tạo control nhanh ----------

        public static Label TaoLabel(string text, int x, int y, int width = 0)
        {
            var lbl = new Label { Text = text, Location = new Point(x, y) };
            if (width > 0) { lbl.AutoSize = false; lbl.Size = new Size(width, 23); }
            else lbl.AutoSize = true;
            return lbl;
        }

        public static TextBox TaoTextBox(int x, int y, int width, bool chiDoc = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Width = width,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = chiDoc
            };
        }

        public static Button TaoButton(string text, int x, int y, int width = 80, int height = 32)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                UseVisualStyleBackColor = true
            };
        }

        public static GroupBox TaoGroupBox(string text, int x, int y, int width, int height)
        {
            return new GroupBox { Text = text, Location = new Point(x, y), Size = new Size(width, height) };
        }

        public static RadioButton TaoRadio(string text, int x, int y)
        {
            return new RadioButton { Text = text, Location = new Point(x, y), AutoSize = true };
        }

        public static CheckBox TaoCheckBox(string text, int x, int y)
        {
            return new CheckBox { Text = text, Location = new Point(x, y), AutoSize = true };
        }

        // ---------- Xác nhận khi đóng form ----------

        /// <summary>
        /// Gọi trong sự kiện FormClosing: hỏi "Bạn có muốn thoát?".
        /// Chọn No thì hủy việc đóng form (e.Cancel = true).
        /// </summary>
        public static void XacNhanDong(FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát?", "Thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        // ---------- Kiểm tra dữ liệu nhập ----------

        /// <summary>
        /// Gắn vào sự kiện KeyPress của TextBox: chặn mọi ký tự không phải số.
        /// Cho phép phím điều khiển (Backspace...), dấu '-' ở đầu (nếu choPhepAm)
        /// và dấu thập phân (nếu choPhepThapPhan).
        /// </summary>
        public static void ChanNhapKhongPhaiSo(object sender, KeyPressEventArgs e,
            bool choPhepAm = false, bool choPhepThapPhan = false)
        {
            char c = e.KeyChar;
            if (char.IsControl(c) || char.IsDigit(c)) return;

            var tb = (TextBox)sender;
            string dauThapPhan = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            if (choPhepThapPhan && c.ToString() == dauThapPhan && !tb.Text.Contains(dauThapPhan)) return;
            if (choPhepAm && c == '-' && tb.SelectionStart == 0 && !tb.Text.Contains("-")) return;

            e.Handled = true; // ký tự bị chặn, không hiện vào TextBox
        }

        /// <summary>Chuỗi có phải số thực hợp lệ không?</summary>
        public static bool LaSoThuc(string s, out double giaTri)
        {
            return double.TryParse(s.Trim(), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.CurrentCulture, out giaTri);
        }

        /// <summary>Chuỗi có phải số nguyên hợp lệ không?</summary>
        public static bool LaSoNguyen(string s, out int giaTri)
        {
            return int.TryParse(s.Trim(), NumberStyles.AllowLeadingSign,
                CultureInfo.CurrentCulture, out giaTri);
        }
    }
}
