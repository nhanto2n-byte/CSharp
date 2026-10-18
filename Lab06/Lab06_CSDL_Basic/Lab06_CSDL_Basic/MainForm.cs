using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab06_CSDL_Basic
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Lab 06 - Tương tác cơ sở dữ liệu";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(440, 280);
            Font = new Font("Segoe UI", 10);
            Label title = new Label { Text = "QUẢN LÝ CƠ SỞ DỮ LIỆU QLSinhVien", AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Height = 65,
                Font = new Font("Segoe UI", 14, FontStyle.Bold) };
            Controls.Add(title);
            AddButton("6a - Quản lý môn học", 70, () => new MonHocForm().ShowDialog(this));
            AddButton("6b - Quản lý sinh viên", 120, () => new SinhVienForm().ShowDialog(this));
            AddButton("6b - Quản lý điểm", 170, () => new DiemForm().ShowDialog(this));
            AddButton("06a/06b - Quản lý lớp", 220, () => new LopForm().ShowDialog(this));
            AddButton("06b nâng cao - Danh sách lớp", 270, () => new ListLopForm().ShowDialog(this));
            AddButton("06b nâng cao - Lọc danh sách điểm", 320, () => new ListDiemForm().ShowDialog(this));
            AddButton("06b về nhà - QLCuaHang", 370, () => new CuaHangForm().ShowDialog(this));
            ClientSize = new Size(440, 430);
        }

        private void AddButton(string text, int top, Action action)
        {
            Button button = new Button { Text = text, Left = 80, Top = top, Width = 280, Height = 36 };
            button.Click += (s, e) => {
                try { action(); }
                catch (Exception ex) { MessageBox.Show("Không thể mở chức năng.\n" + ex.Message, "Lỗi"); }
            };
            Controls.Add(button);
        }
    }
}
