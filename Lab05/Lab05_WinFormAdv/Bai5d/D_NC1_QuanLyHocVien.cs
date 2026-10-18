using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài tập nâng cao 1: Quản lý học viên (MenuStrip + 2 ListBox chọn nhiều dòng).
    /// Menu "Cập nhật": Nhập học viên mới / Chuyển sang lớp B / Chuyển sang lớp A / Xóa học viên.
    /// Menu "Kết thúc": hỏi xác nhận rồi mới đóng.
    /// </summary>
    public class FrmQuanLyHocVien : Form
    {
        private MenuStrip menuStrip1;
        private ListBox lstLopA, lstLopB;

        public FrmQuanLyHocVien()
        {
            Helper.CauHinhForm(this, "Quản lý học viên", 400, 300);

            // ----- Menu -----
            menuStrip1 = new MenuStrip();
            ToolStripMenuItem mnuCapNhat = new ToolStripMenuItem("Cập nhật");
            ToolStripMenuItem mnuNhap = new ToolStripMenuItem("Nhập học viên mới");
            ToolStripMenuItem mnuSangB = new ToolStripMenuItem("Chuyển sang lớp B");
            ToolStripMenuItem mnuSangA = new ToolStripMenuItem("Chuyển sang lớp A");
            ToolStripMenuItem mnuXoa = new ToolStripMenuItem("Xóa học viên");
            mnuCapNhat.DropDownItems.AddRange(new ToolStripItem[] { mnuNhap, mnuSangB, mnuSangA, mnuXoa });
            ToolStripMenuItem mnuKetThuc = new ToolStripMenuItem("Kết thúc");
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuCapNhat, mnuKetThuc });
            this.MainMenuStrip = menuStrip1;

            // ----- 2 ListBox trong 2 GroupBox -----
            GroupBox grpA = new GroupBox { Text = "Lớp A", Left = 20, Top = 40, Width = 170, Height = 235 };
            lstLopA = new ListBox { Left = 10, Top = 22, Width = 150, Height = 200, SelectionMode = SelectionMode.MultiExtended };
            lstLopA.Items.AddRange(new object[] { "Trần Hùng", "Nguyễn Hưng", "Lâm Tứ" });
            grpA.Controls.Add(lstLopA);

            GroupBox grpB = new GroupBox { Text = "Lớp B", Left = 205, Top = 40, Width = 170, Height = 235 };
            lstLopB = new ListBox { Left = 10, Top = 22, Width = 150, Height = 200, SelectionMode = SelectionMode.MultiExtended };
            lstLopB.Items.AddRange(new object[] { "Tuấn Hưng", "Hồng Ngọc", "Anh Minh", "Bảo Lâm" });
            grpB.Controls.Add(lstLopB);

            mnuNhap.Click += mnuNhap_Click;
            mnuSangB.Click += mnuSangB_Click;
            mnuSangA.Click += mnuSangA_Click;
            mnuXoa.Click += mnuXoa_Click;
            mnuKetThuc.Click += mnuKetThuc_Click;
            this.FormClosing += FrmQuanLyHocVien_FormClosing;

            this.Controls.Add(grpA);
            this.Controls.Add(grpB);
            this.Controls.Add(menuStrip1);
        }

        // Mở form nhập học viên mới (dạng hộp thoại)
        private void mnuNhap_Click(object sender, EventArgs e)
        {
            FrmNhapHocVien frm = new FrmNhapHocVien(lstLopA, lstLopB);
            frm.ShowDialog(this);
        }

        // Chuyển các mục đang chọn từ listbox nguồn sang listbox đích
        private void ChuyenLop(ListBox nguon, ListBox dich)
        {
            if (nguon.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn học viên trước!", "Thông báo");
                return;
            }
            // Copy danh sách chọn ra List trước rồi mới thêm/xóa
            List<object> ds = new List<object>();
            foreach (object o in nguon.SelectedItems)
                ds.Add(o);
            foreach (object o in ds)
            {
                dich.Items.Add(o);
                nguon.Items.Remove(o);
            }
        }

        private void mnuSangB_Click(object sender, EventArgs e)
        {
            ChuyenLop(lstLopA, lstLopB);   // Lớp A -> Lớp B
        }

        private void mnuSangA_Click(object sender, EventArgs e)
        {
            ChuyenLop(lstLopB, lstLopA);   // Lớp B -> Lớp A
        }

        // Xóa tất cả học viên đang chọn ở cả Lớp A và Lớp B
        private void mnuXoa_Click(object sender, EventArgs e)
        {
            if (lstLopA.SelectedItems.Count == 0 && lstLopB.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn học viên cần xóa!", "Thông báo");
                return;
            }
            XoaDangChon(lstLopA);
            XoaDangChon(lstLopB);
        }

        private void XoaDangChon(ListBox lst)
        {
            List<int> ds = new List<int>();
            foreach (int i in lst.SelectedIndices)
                ds.Add(i);
            for (int k = ds.Count - 1; k >= 0; k--)   // xóa từ cuối lên
                lst.Items.RemoveAt(ds[k]);
        }

        // Kết thúc: Close() -> sự kiện FormClosing hiện MessageBox hỏi xác nhận
        private void mnuKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmQuanLyHocVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }

    /// <summary>
    /// Form "Nhập học viên mới": nhận 2 ListBox của form chính để thêm học viên vào đúng lớp.
    /// </summary>
    public class FrmNhapHocVien : Form
    {
        private TextBox txtHoTen;
        private ComboBox cboLop;
        private Button btnCapNhat, btnTroVe;
        private ListBox lstLopA, lstLopB;

        public FrmNhapHocVien(ListBox lopA, ListBox lopB)
        {
            lstLopA = lopA;
            lstLopB = lopB;

            Helper.CauHinhForm(this, "Nhập học viên mới", 360, 170);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;

            txtHoTen = Helper.Txt(100, 20, 230);
            cboLop = new ComboBox { Left = 100, Top = 60, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };
            cboLop.Items.AddRange(new object[] { "Lớp A", "Lớp B" });
            cboLop.SelectedIndex = 0;
            btnCapNhat = Helper.Btn("Cập nhật", 100, 105, 110);
            btnTroVe = Helper.Btn("Trở về", 220, 105, 110);

            btnCapNhat.Click += btnCapNhat_Click;
            btnTroVe.Click += btnTroVe_Click;

            this.Controls.AddRange(new Control[] {
                Helper.Lbl("Họ và Tên", 20, 20, 75), txtHoTen,
                Helper.Lbl("Lớp", 20, 60, 75), cboLop, btnCapNhat, btnTroVe });
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string ten = txtHoTen.Text.Trim();
            if (ten == "")
            {
                MessageBox.Show("Bạn phải nhập họ tên!", "Thông báo");
                txtHoTen.Focus();          // đặt con trỏ vào textbox Họ và Tên
                return;
            }
            // Tùy lớp đã chọn trong combobox -> thêm vào listbox tương ứng trên form chính
            if (cboLop.SelectedIndex == 0)
                lstLopA.Items.Add(ten);
            else
                lstLopB.Items.Add(ten);

            txtHoTen.Clear();
            txtHoTen.Focus();
        }

        // Trở về form chính
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
