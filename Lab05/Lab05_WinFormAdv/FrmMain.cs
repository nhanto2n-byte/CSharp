using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv
{
    /// <summary>
    /// Form chính (Bài về nhà 2 - 5d): MenuStrip gắn tất cả bài tập trong tuần,
    /// chọn menu nào thì hiển thị form tương ứng.
    /// </summary>
    public class FrmMain : Form
    {
        private MenuStrip menuStrip1;

        public FrmMain()
        {
            Helper.CauHinhForm(this, "Lab05 - Lập trình Windows Form nâng cao", 640, 400);
            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            menuStrip1 = new MenuStrip();

            // ----- Menu Bài 5c: ListBox / ComboBox / TreeView -----
            ToolStripMenuItem mnu5c = new ToolStripMenuItem("Bài 5c");
            ThemMuc(mnu5c, "Mẫu 1 - ListBox chuyển phần tử", () => new Bai5c.FrmListBoxMau());
            ThemMuc(mnu5c, "Mẫu 2 - ComboBox dân tộc", () => new Bai5c.FrmCboDanToc());
            ThemMuc(mnu5c, "Mẫu 3 - TreeView phòng ban - nhân viên", () => new Bai5c.FrmTreeView());
            mnu5c.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5c, "Tại lớp 1 - Ước số", () => new Bai5c.FrmUocSo());
            ThemMuc(mnu5c, "Tại lớp 2 - Quản lý sinh viên (TreeView)", () => new Bai5c.FrmQuanLySV());
            mnu5c.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5c, "Nâng cao 1 - Xử lý chuỗi", () => new Bai5c.FrmXuLyChuoi());
            mnu5c.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5c, "Về nhà 1 - Từ điển Anh - Việt", () => new Bai5c.FrmTuDien());
            ThemMuc(mnu5c, "Về nhà 2 - ListBox số tự nhiên", () => new Bai5c.FrmListBoxSo());
            ThemMuc(mnu5c, "Về nhà 3 - Danh bạ", () => new Bai5c.FrmDanhBa());

            // ----- Menu Bài 5d: Timer / ListView / Menu / Dialog -----
            ToolStripMenuItem mnu5d = new ToolStripMenuItem("Bài 5d");
            ThemMuc(mnu5d, "Mẫu 1 - Timer đồng hồ", () => new Bai5d.FrmTimer());
            ThemMuc(mnu5d, "Mẫu 2 - ListView", () => new Bai5d.FrmListViewMau());
            ThemMuc(mnu5d, "Mẫu 3 - Save/Open File Dialog", () => new Bai5d.FrmFileDialog());
            mnu5d.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5d, "Tại lớp 1 - Thông tin sinh viên", () => new Bai5d.FrmThongTinSV());
            ThemMuc(mnu5d, "Tại lớp 2 - Quản lý tài khoản ngân hàng", () => new Bai5d.FrmTaiKhoan());
            mnu5d.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5d, "Nâng cao 1 - Quản lý học viên", () => new Bai5d.FrmQuanLyHocVien());
            mnu5d.DropDownItems.Add(new ToolStripSeparator());
            ThemMuc(mnu5d, "Về nhà 1 - Đếm ngược thời gian", () => new Bai5d.FrmDemNguoc());

            ToolStripMenuItem mnuThoat = new ToolStripMenuItem("Thoát");
            mnuThoat.Click += (s, e) => this.Close();

            menuStrip1.Items.AddRange(new ToolStripItem[] { mnu5c, mnu5d, mnuThoat });
            this.MainMenuStrip = menuStrip1;

            Label lbl = new Label
            {
                Text = "LAB 05 - WINDOWS FORM NÂNG CAO\n\nChọn bài tập ở thanh menu phía trên",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.SteelBlue
            };

            this.Controls.Add(lbl);
            this.Controls.Add(menuStrip1);
            this.FormClosing += FrmMain_FormClosing;
        }

        // Thêm 1 mục vào menu cha; khi click thì tạo form mới và hiển thị
        private void ThemMuc(ToolStripMenuItem cha, string ten, Func<Form> taoForm)
        {
            ToolStripMenuItem muc = new ToolStripMenuItem(ten);
            muc.Click += (s, e) => taoForm().Show();
            cha.DropDownItems.Add(muc);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
