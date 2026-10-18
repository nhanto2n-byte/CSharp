using System;
using System.Windows.Forms;
using System.Drawing;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài mẫu 2: nạp dữ liệu vào ComboBox "Dân tộc" và hiển thị mục được chọn
    /// theo 2 cách (nút Hiển thị / MessageBox ngay khi chọn).
    /// </summary>
    public class FrmCboDanToc : Form
    {
        private Button btn_loadDL, btn_hienthi;
        private ComboBox cbo_dt;
        private Label lbl_kq;

        public FrmCboDanToc()
        {
            Helper.CauHinhForm(this, "Cbo_Dantoc", 340, 200);

            btn_loadDL = Helper.Btn("Load dữ liệu Combobox", 20, 15, 200);
            Label lblDT = Helper.Lbl("Dân tộc", 20, 60, 60);
            cbo_dt = new ComboBox { Left = 90, Top = 60, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            btn_hienthi = Helper.Btn("Hiển thị", 90, 100, 100);
            lbl_kq = new Label { Left = 90, Top = 145, Width = 230, Height = 25, ForeColor = Color.Red, Text = "Kết quả" };

            btn_loadDL.Click += btn_loadDL_Click;
            btn_hienthi.Click += btn_hienthi_Click;
            cbo_dt.SelectedIndexChanged += cbo_dt_SelectedIndexChanged;

            this.Controls.AddRange(new Control[] { btn_loadDL, lblDT, cbo_dt, btn_hienthi, lbl_kq });
        }

        // Nạp 5 dân tộc vào combobox
        private void btn_loadDL_Click(object sender, EventArgs e)
        {
            string[] dt = { "Kinh", "Hoa", "K’Me", "H’Mong", "Khác" };
            cbo_dt.Items.Clear();             // tránh bị nạp trùng khi bấm nhiều lần
            foreach (string s in dt)
                cbo_dt.Items.Add(s);
        }

        // Cách 1: bấm nút Hiển thị -> hiện lên Label
        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            string s = "Dân tộc được chọn: ";
            if (cbo_dt.SelectedIndex >= 0)
                lbl_kq.Text = s + cbo_dt.SelectedItem.ToString();
            else
                lbl_kq.Text = "Bạn chưa chọn dân tộc";
        }

        // Cách 2: chọn xong là hiện MessageBox ngay
        private void cbo_dt_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra >= 0 vì khi Clear() combobox thì SelectedIndex = -1 (SelectedItem = null)
            if (cbo_dt.SelectedIndex >= 0)
                MessageBox.Show("Dân tộc được chọn: " + cbo_dt.SelectedItem.ToString());
        }
    }
}
