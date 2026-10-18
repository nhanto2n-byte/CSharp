using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài tập tại lớp 1: quản lý thông tin sinh viên trên ListView.
    /// - Form Load: nạp dữ liệu cho ComboBox dân tộc.
    /// - Thêm / Xóa / Sửa; thêm không trùng mã SV, không cho sửa mã SV.
    /// - ContextMenuStrip "Xóa" khi click phải vào ListView.
    /// - Hỏi xác nhận trước khi đóng form.
    /// </summary>
    public class FrmThongTinSV : Form
    {
        private TextBox txtHoTen, txtMaSV;
        private RadioButton rdoNam, rdoNu;
        private CheckBox chkAnh, chkPhap, chkHoa;
        private ComboBox cboDanToc;
        private ListView lstv;
        private Button btnThem, btnXoa, btnSua;
        private ContextMenuStrip ctxMenu;

        public FrmThongTinSV()
        {
            Helper.CauHinhForm(this, "frmBaitap1", 560, 470);

            txtHoTen = Helper.Txt(170, 18, 260);
            txtMaSV = Helper.Txt(170, 53, 260);
            rdoNam = new RadioButton { Text = "Nam", Left = 170, Top = 90, Width = 70, Checked = true };
            rdoNu = new RadioButton { Text = "Nữ", Left = 270, Top = 90, Width = 70 };
            chkAnh = new CheckBox { Text = "Anh", Left = 170, Top = 125, Width = 70 };
            chkPhap = new CheckBox { Text = "Pháp", Left = 270, Top = 125, Width = 70 };
            chkHoa = new CheckBox { Text = "Hoa", Left = 370, Top = 125, Width = 70 };
            cboDanToc = new ComboBox { Left = 170, Top = 158, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };

            lstv = new ListView { Left = 15, Top = 200, Width = 530, Height = 200 };
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
            lstv.MultiSelect = false;
            lstv.Columns.Add("Họ tên", 150);
            lstv.Columns.Add("Mã sinh viên", 100);
            lstv.Columns.Add("Giới tính", 70);
            lstv.Columns.Add("Ngoại ngữ", 120);
            lstv.Columns.Add("Dân tộc", 85);

            btnThem = Helper.Btn("Thêm", 120, 418, 100);
            btnXoa = Helper.Btn("Xóa", 230, 418, 100);
            btnSua = Helper.Btn("Sửa", 340, 418, 100);

            // ContextMenuStrip: click phải vào ListView -> hiện menu "Xóa"
            ctxMenu = new ContextMenuStrip();
            ToolStripMenuItem mnuXoa = new ToolStripMenuItem("Xóa");
            mnuXoa.Click += btnXoa_Click;
            ctxMenu.Items.Add(mnuXoa);
            lstv.ContextMenuStrip = ctxMenu;

            this.Load += FrmThongTinSV_Load;
            this.FormClosing += FrmThongTinSV_FormClosing;
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnSua.Click += btnSua_Click;
            lstv.SelectedIndexChanged += lstv_SelectedIndexChanged;

            this.Controls.AddRange(new Control[] {
                Helper.Lbl("Họ và Tên", 40, 18, 120), txtHoTen,
                Helper.Lbl("Mã sinh viên", 40, 53, 120), txtMaSV,
                Helper.Lbl("Giới tính", 40, 90, 120), rdoNam, rdoNu,
                Helper.Lbl("Ngoại ngữ", 40, 125, 120), chkAnh, chkPhap, chkHoa,
                Helper.Lbl("Dân tộc", 40, 158, 120), cboDanToc,
                lstv, btnThem, btnXoa, btnSua });
        }

        // Form Load: thêm dữ liệu vào combobox dân tộc
        private void FrmThongTinSV_Load(object sender, EventArgs e)
        {
            cboDanToc.Items.AddRange(new object[] { "Kinh", "Hoa", "K’Me", "H’Mong", "Khác" });
            cboDanToc.SelectedIndex = 0;
        }

        // Ghép các ngoại ngữ được tick thành chuỗi "Anh, Pháp"
        private string LayNgoaiNgu()
        {
            List<string> ds = new List<string>();
            if (chkAnh.Checked) ds.Add("Anh");
            if (chkPhap.Checked) ds.Add("Pháp");
            if (chkHoa.Checked) ds.Add("Hoa");
            return string.Join(", ", ds.ToArray());
        }

        private bool TrungMaSV(string ma)
        {
            foreach (ListViewItem it in lstv.Items)
                if (string.Compare(it.SubItems[1].Text, ma, true) == 0)
                    return true;
            return false;
        }

        // Đưa form về trạng thái nhập mới
        private void LamMoiForm()
        {
            lstv.SelectedItems.Clear();   // bỏ chọn dòng -> mở khóa ô Mã SV (xem lstv_SelectedIndexChanged)
            txtHoTen.Clear();
            txtMaSV.Clear();
            rdoNam.Checked = true;
            chkAnh.Checked = false;
            chkPhap.Checked = false;
            chkHoa.Checked = false;
            cboDanToc.SelectedIndex = 0;
            txtHoTen.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtHoTen.Text.Trim();
            string ma = txtMaSV.Text.Trim();
            if (ten == "" || ma == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ Họ tên và Mã sinh viên!", "Thông báo");
                return;
            }
            if (TrungMaSV(ma))   // thêm không cho trùng mã sinh viên
            {
                MessageBox.Show("Mã sinh viên \"" + ma + "\" đã tồn tại!", "Thông báo");
                txtMaSV.Focus();
                return;
            }
            string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            ListViewItem item = new ListViewItem(new string[] { ten, ma, gioiTinh, LayNgoaiNgu(), cboDanToc.Text });
            lstv.Items.Add(item);
            LamMoiForm();
        }

        // Dùng chung cho button Xóa và menu chuột phải "Xóa"
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstv.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần xóa!", "Thông báo");
                return;
            }
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                lstv.SelectedItems[0].Remove();
                LamMoiForm();
            }
        }

        // Sửa: cập nhật các cột trừ Mã sinh viên (không cho sửa mã)
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lstv.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần sửa!", "Thông báo");
                return;
            }
            if (txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo");
                txtHoTen.Focus();
                return;
            }
            ListViewItem item = lstv.SelectedItems[0];
            item.SubItems[0].Text = txtHoTen.Text.Trim();
            item.SubItems[2].Text = rdoNam.Checked ? "Nam" : "Nữ";
            item.SubItems[3].Text = LayNgoaiNgu();
            item.SubItems[4].Text = cboDanToc.Text;
        }

        // Chọn dòng -> hiện thông tin lên các control và khóa ô Mã SV; bỏ chọn -> mở khóa
        private void lstv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstv.SelectedItems.Count == 0)
            {
                txtMaSV.ReadOnly = false;
                return;
            }
            ListViewItem item = lstv.SelectedItems[0];
            txtHoTen.Text = item.SubItems[0].Text;
            txtMaSV.Text = item.SubItems[1].Text;
            txtMaSV.ReadOnly = true;             // không cho sửa mã sinh viên
            rdoNam.Checked = item.SubItems[2].Text == "Nam";
            rdoNu.Checked = item.SubItems[2].Text == "Nữ";
            string nn = item.SubItems[3].Text;
            chkAnh.Checked = nn.Contains("Anh");
            chkPhap.Checked = nn.Contains("Pháp");
            chkHoa.Checked = nn.Contains("Hoa");
            cboDanToc.SelectedIndex = cboDanToc.FindStringExact(item.SubItems[4].Text);
        }

        private void FrmThongTinSV_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
