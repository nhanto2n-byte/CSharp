using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài mẫu 3: Hồ sơ nhân viên - TreeView phòng ban + ComboBox phòng ban.
    /// </summary>
    public class FrmTreeView : Form
    {
        private TreeView trv_DS;
        private TextBox txt_phongban, txt_maso, txt_hoten, txt_diachi;
        private ComboBox cbo_phongban;
        private Button btn_ThemPB, btn_XoaPB, btn_ThemNV, btn_Thoat;

        public FrmTreeView()
        {
            Helper.CauHinhForm(this, "Phòng ban - Nhân viên", 660, 430);
            this.Font = new Font("Times New Roman", 11);

            // ----- Cột trái: danh sách phòng ban -----
            Label lblPB = new Label { Text = "Phòng ban", Left = 10, Top = 8, Width = 200, Font = new Font("Times New Roman", 11, FontStyle.Bold) };
            trv_DS = new TreeView { Name = "trv_DS", Left = 10, Top = 32, Width = 200, Height = 280 };
            Label lblPB2 = Helper.Lbl("Phòng ban", 10, 320, 70);
            txt_phongban = Helper.Txt(85, 320, 125);
            btn_ThemPB = Helper.Btn("Thêm phòng ban", 85, 352, 125);
            btn_XoaPB = Helper.Btn("Xóa phòng ban", 85, 388, 125);

            // ----- Cột phải: hồ sơ nhân viên -----
            Label lblTitle = new Label
            {
                Text = "HỒ SƠ NHÂN VIÊN", Left = 230, Top = 8, Width = 400, Height = 30,
                Font = new Font("Times New Roman", 16, FontStyle.Bold), ForeColor = Color.Blue
            };
            Label lblMa = Helper.Lbl("Mã số", 230, 55, 60);
            txt_maso = Helper.Txt(295, 55, 90);
            Label lblTen = Helper.Lbl("Họ tên", 400, 55, 60);
            txt_hoten = Helper.Txt(460, 55, 180);
            Label lblDC = Helper.Lbl("Địa chỉ", 230, 95, 60);
            txt_diachi = Helper.Txt(295, 95, 345);
            Label lblPhong = Helper.Lbl("Phòng ban", 230, 135, 65);
            cbo_phongban = new ComboBox { Left = 295, Top = 135, Width = 345, DropDownStyle = ComboBoxStyle.DropDownList };
            btn_ThemNV = Helper.Btn("Thêm", 295, 175, 90);
            btn_Thoat = Helper.Btn("Thoát", 395, 175, 90);

            this.Load += FrmTreeView_Load;
            this.FormClosing += FrmTreeView_FormClosing;
            btn_ThemPB.Click += btn_ThemPB_Click;
            btn_XoaPB.Click += btn_XoaPB_Click;
            btn_ThemNV.Click += btn_ThemNV_Click;
            btn_Thoat.Click += btn_Thoat_Click;

            this.Controls.AddRange(new Control[] {
                lblPB, trv_DS, lblPB2, txt_phongban, btn_ThemPB, btn_XoaPB,
                lblTitle, lblMa, txt_maso, lblTen, txt_hoten, lblDC, txt_diachi,
                lblPhong, cbo_phongban, btn_ThemNV, btn_Thoat });
        }

        // Form Load: nạp sẵn 4 phòng ban vào TreeView và ComboBox
        private void FrmTreeView_Load(object sender, EventArgs e)
        {
            string[] pb = { "Giám đốc", "Tổ chức hành chính", "Kế hoạch", "Kế Toán" };
            foreach (string s in pb)
            {
                trv_DS.Nodes.Add(s);        // thêm node vào treeview
                cbo_phongban.Items.Add(s);  // thêm item vào combobox
            }
            cbo_phongban.SelectedIndex = 0; // item đầu tiên được chọn
        }

        // Kiểm tra phòng ban CHƯA tồn tại (true = chưa có, được phép thêm)
        // string.Compare(..., true): so sánh không phân biệt hoa/thường
        private bool KiemTra(string s)
        {
            foreach (TreeNode node in trv_DS.Nodes)
                if (string.Compare(node.Text, s, true) == 0)
                    return false;
            return true;
        }

        // Thêm phòng ban mới vào treeview và combobox
        private void btn_ThemPB_Click(object sender, EventArgs e)
        {
            string ten = txt_phongban.Text.Trim();
            if (ten == "")
            {
                MessageBox.Show("Hãy nhập tên phòng ban!", "Thông báo");
            }
            else if (KiemTra(ten))
            {
                trv_DS.Nodes.Add(ten);
                cbo_phongban.Items.Add(ten);
            }
            else
            {
                MessageBox.Show("Phòng ban đã tồn tại!", "Thông báo");
            }
            txt_phongban.Text = "";
            txt_phongban.Focus();
        }

        // Xóa phòng ban đang chọn trong treeview (có xác nhận)
        private void btn_XoaPB_Click(object sender, EventArgs e)
        {
            TreeNode node = trv_DS.SelectedNode;
            // Chỉ cho xóa node gốc (phòng ban), không xóa node con (nhân viên)
            if (node == null || node.Parent != null)
            {
                MessageBox.Show("Hãy chọn một phòng ban trong TreeView!", "Thông báo");
                return;
            }
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa phòng ban \"" + node.Text + "\"?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (kq == DialogResult.Yes)
            {
                cbo_phongban.Items.Remove(node.Text);
                trv_DS.Nodes.Remove(node);
                if (cbo_phongban.Items.Count > 0)
                    cbo_phongban.SelectedIndex = 0;
                else
                    cbo_phongban.Text = "";
            }
        }

        // Thêm nhân viên vào phòng ban đang chọn trong combobox
        private void btn_ThemNV_Click(object sender, EventArgs e)
        {
            if (txt_maso.Text.Trim() == "" || txt_hoten.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập mã số và họ tên nhân viên!", "Thông báo");
                return;
            }
            // Tìm node phòng ban có nội dung trùng với item đang chọn trong combobox
            TreeNode pb = null;
            foreach (TreeNode node in trv_DS.Nodes)
            {
                if (node.Text == cbo_phongban.Text)
                {
                    pb = node;
                    break;
                }
            }
            if (pb == null)
            {
                MessageBox.Show("Hãy chọn phòng ban!", "Thông báo");
                return;
            }
            pb.Nodes.Add(txt_hoten.Text.Trim() + " (" + txt_maso.Text.Trim() + ")");
            trv_DS.ExpandAll();   // mở rộng treeview để thấy nhân viên vừa thêm
            txt_maso.Clear();
            txt_hoten.Clear();
            txt_maso.Focus();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();   // sự kiện FormClosing sẽ hỏi xác nhận
        }

        private void FrmTreeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
