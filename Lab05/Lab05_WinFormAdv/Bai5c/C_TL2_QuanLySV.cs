using System;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài tập tại lớp 2: Quản lý sinh viên theo lớp bằng TreeView.
    /// Cấu trúc cây:  Danh sách lớp
    ///                  └ Lớp
    ///                      └ "MãSV, Họ tên"   (Tag = string[] {mã, tên, địa chỉ})
    ///                          └ Địa chỉ
    /// </summary>
    public class FrmQuanLySV : Form
    {
        private TreeView trvDS;
        private ComboBox cboLop;
        private GroupBox grpSV, grpLop;
        private TextBox txtMaSV, txtHoTen, txtDiaChi, txtTenLop;
        private Button btnCapNhat, btnXoa, btnThemLop;
        private CheckBox chkThemLop;
        private TreeNode nodeGoc;

        public FrmQuanLySV()
        {
            Helper.CauHinhForm(this, "Quản lý sinh viên", 580, 440);

            trvDS = new TreeView { Left = 10, Top = 10, Width = 210, Height = 420 };
            Label lblLop = Helper.Lbl("Chọn Lớp", 235, 12, 65);
            cboLop = new ComboBox { Left = 305, Top = 12, Width = 255, DropDownStyle = ComboBoxStyle.DropDownList };

            // ----- GroupBox thông tin sinh viên -----
            grpSV = new GroupBox { Text = "Thông tin sinh viên", Left = 230, Top = 45, Width = 335, Height = 200 };
            txtMaSV = Helper.Txt(90, 28, 225);
            txtHoTen = Helper.Txt(90, 68, 225);
            txtDiaChi = Helper.Txt(90, 108, 225);
            btnCapNhat = Helper.Btn("Cập Nhật", 90, 150, 105);
            btnXoa = Helper.Btn("Xóa", 210, 150, 105);
            grpSV.Controls.AddRange(new Control[] {
                Helper.Lbl("Mã SV:", 15, 28, 70), txtMaSV,
                Helper.Lbl("Họ Tên:", 15, 68, 70), txtHoTen,
                Helper.Lbl("Địa Chỉ:", 15, 108, 70), txtDiaChi,
                btnCapNhat, btnXoa });

            // ----- CheckBox + GroupBox thêm lớp (ẩn lúc đầu) -----
            chkThemLop = new CheckBox { Text = "Thêm lớp", Left = 235, Top = 255, Width = 120, Checked = false };
            grpLop = new GroupBox { Text = "Thông tin lớp", Left = 230, Top = 285, Width = 335, Height = 75, Visible = false };
            txtTenLop = Helper.Txt(85, 28, 140);
            btnThemLop = Helper.Btn("Thêm lớp", 235, 26, 85);
            grpLop.Controls.AddRange(new Control[] { Helper.Lbl("Tên lớp", 15, 28, 65), txtTenLop, btnThemLop });

            // ----- Dữ liệu ban đầu: node gốc + 4 lớp -----
            nodeGoc = trvDS.Nodes.Add("Danh sách lớp");
            string[] dsLop = { "05DHTH1", "05DHTH2", "05DHTH3", "05DHTH4" };
            foreach (string lop in dsLop)
            {
                nodeGoc.Nodes.Add(lop);
                cboLop.Items.Add(lop);
            }
            cboLop.SelectedIndex = 0;
            nodeGoc.Expand();

            chkThemLop.CheckedChanged += chkThemLop_CheckedChanged;
            btnThemLop.Click += btnThemLop_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnXoa.Click += btnXoa_Click;
            trvDS.AfterSelect += trvDS_AfterSelect;

            this.Controls.AddRange(new Control[] { trvDS, lblLop, cboLop, grpSV, chkThemLop, grpLop });
        }

        // Tick "Thêm lớp" -> hiện/ẩn groupbox Thông tin lớp (và các control bên trong)
        private void chkThemLop_CheckedChanged(object sender, EventArgs e)
        {
            grpLop.Visible = chkThemLop.Checked;
        }

        // Thêm lớp mới vào TreeView + ComboBox (không trùng tên lớp)
        private void btnThemLop_Click(object sender, EventArgs e)
        {
            string ten = txtTenLop.Text.Trim();
            if (ten == "")
            {
                MessageBox.Show("Hãy nhập tên lớp!", "Thông báo");
                txtTenLop.Focus();
                return;
            }
            foreach (TreeNode lop in nodeGoc.Nodes)
            {
                if (string.Compare(lop.Text, ten, true) == 0)
                {
                    MessageBox.Show("Lớp \"" + ten + "\" đã tồn tại!", "Thông báo");
                    txtTenLop.Focus();
                    return;
                }
            }
            nodeGoc.Nodes.Add(ten);
            cboLop.Items.Add(ten);
            txtTenLop.Clear();
            txtTenLop.Focus();
        }

        // Thêm sinh viên vào lớp đang chọn trong combobox
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string ma = txtMaSV.Text.Trim();
            string ten = txtHoTen.Text.Trim();
            string dc = txtDiaChi.Text.Trim();

            // 1. Các textbox không được để trống
            if (ma == "" || ten == "" || dc == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ Mã SV, Họ tên và Địa chỉ!", "Thông báo");
                return;
            }
            // 2. Không trùng mã sinh viên (kiểm tra trên tất cả các lớp)
            if (TrungMaSV(ma))
            {
                MessageBox.Show("Mã sinh viên \"" + ma + "\" đã tồn tại!", "Thông báo");
                txtMaSV.Focus();
                return;
            }
            // 3. Tìm node lớp đang chọn trong combobox rồi thêm sinh viên
            TreeNode lop = TimNodeLop(cboLop.Text);
            if (lop == null)
            {
                MessageBox.Show("Hãy chọn lớp!", "Thông báo");
                return;
            }
            TreeNode nodeSV = lop.Nodes.Add(ma + ", " + ten);
            nodeSV.Tag = new string[] { ma, ten, dc };   // lưu thông tin để hiển thị lại khi click
            nodeSV.Nodes.Add(dc);
            lop.Expand();

            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtMaSV.Focus();
        }

        // Chỉ cho xóa node sinh viên đang chọn (có xác nhận)
        private void btnXoa_Click(object sender, EventArgs e)
        {
            TreeNode nodeSV = LaySinhVienDangChon();
            if (nodeSV == null)
            {
                MessageBox.Show("Hãy chọn một sinh viên trên TreeView để xóa!", "Thông báo");
                return;
            }
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa sinh viên \"" + nodeSV.Text + "\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                nodeSV.Remove();
                txtMaSV.Clear();
                txtHoTen.Clear();
                txtDiaChi.Clear();
            }
        }

        // Click node sinh viên -> hiển thị thông tin lên các textbox
        private void trvDS_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode nodeSV = LaySinhVienDangChon();
            if (nodeSV != null)
            {
                string[] tt = (string[])nodeSV.Tag;
                txtMaSV.Text = tt[0];
                txtHoTen.Text = tt[1];
                txtDiaChi.Text = tt[2];
                cboLop.SelectedItem = nodeSV.Parent.Text;
            }
            else if (e.Node.Level == 1)
            {
                cboLop.SelectedItem = e.Node.Text;   // chọn node lớp -> đồng bộ combobox
            }
        }

        // Trả về node sinh viên đang chọn (nếu chọn node địa chỉ thì lấy node cha); không phải sinh viên -> null
        private TreeNode LaySinhVienDangChon()
        {
            TreeNode node = trvDS.SelectedNode;
            if (node != null && node.Level == 3)
                node = node.Parent;
            if (node != null && node.Tag as string[] != null)
                return node;
            return null;
        }

        private TreeNode TimNodeLop(string tenLop)
        {
            foreach (TreeNode lop in nodeGoc.Nodes)
                if (lop.Text == tenLop)
                    return lop;
            return null;
        }

        private bool TrungMaSV(string ma)
        {
            foreach (TreeNode lop in nodeGoc.Nodes)
                foreach (TreeNode sv in lop.Nodes)
                {
                    string[] tt = sv.Tag as string[];
                    if (tt != null && string.Compare(tt[0], ma, true) == 0)
                        return true;
                }
            return false;
        }
    }
}
