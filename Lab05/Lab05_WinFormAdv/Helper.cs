using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv
{
    /// <summary>
    /// Lớp tiện ích: dựng nhanh các control bằng code (thay cho kéo-thả Designer)
    /// để mỗi bài chỉ cần 1 file .cs duy nhất.
    /// </summary>
    public static class Helper
    {
        // Thiết lập chung cho mọi form: tiêu đề, kích thước, vị trí, font
        public static void CauHinhForm(Form f, string tieuDe, int rong, int cao)
        {
            f.Text = tieuDe;
            f.ClientSize = new Size(rong, cao);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.MaximizeBox = false;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.Font = new Font("Segoe UI", 9.5f);
        }

        public static Label Lbl(string text, int x, int y, int w)
        {
            return new Label { Text = text, Left = x, Top = y + 4, Width = w, Height = 22, AutoSize = false };
        }

        public static TextBox Txt(int x, int y, int w)
        {
            return new TextBox { Left = x, Top = y, Width = w };
        }

        public static Button Btn(string text, int x, int y, int w)
        {
            return new Button { Text = text, Left = x, Top = y, Width = w, Height = 30 };
        }

        // Hỏi xác nhận trước khi đóng form (dùng trong sự kiện FormClosing)
        public static void XacNhanDong(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult kq = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (kq == DialogResult.No)
                    e.Cancel = true;   // hủy việc đóng form
            }
        }

        // Hộp nhập liệu đơn giản (thay cho Interaction.InputBox của VB).
        // Trả về null nếu người dùng bấm Hủy.
        public static string InputBox(string loiNhac, string tieuDe, string macDinh)
        {
            Form f = new Form();
            f.Text = tieuDe;
            f.ClientSize = new Size(360, 110);
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            f.StartPosition = FormStartPosition.CenterParent;
            f.MaximizeBox = false;
            f.MinimizeBox = false;

            Label lbl = new Label { Text = loiNhac, Left = 15, Top = 12, Width = 330, AutoSize = false };
            TextBox txt = new TextBox { Left = 15, Top = 38, Width = 330, Text = macDinh };
            Button ok = new Button { Text = "OK", Left = 190, Top = 70, Width = 75, DialogResult = DialogResult.OK };
            Button huy = new Button { Text = "Hủy", Left = 270, Top = 70, Width = 75, DialogResult = DialogResult.Cancel };
            f.Controls.AddRange(new Control[] { lbl, txt, ok, huy });
            f.AcceptButton = ok;
            f.CancelButton = huy;

            string kq = null;
            if (f.ShowDialog() == DialogResult.OK)
                kq = txt.Text;
            f.Dispose();
            return kq;
        }
    }
}
