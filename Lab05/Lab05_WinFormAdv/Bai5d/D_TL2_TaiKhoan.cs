using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài tập tại lớp 2: quản lý thông tin tài khoản ngân hàng.
    /// - Thêm: đổi text thành "Hủy", bật nút Lưu, xóa trắng textbox, đặt con trỏ vào Số tài khoản.
    /// - Lưu: lưu dữ liệu từ textbox vào ListView.
    /// - Xóa: xóa dòng đang chọn (bật khi có dòng được chọn).
    /// - Click 1 khách hàng: hiện thông tin lên textbox.
    /// - STT đánh tự động, Tổng tiền tự tính khi Thêm / Xóa.
    /// Số tiền của mỗi dòng được lưu trong Tag (kiểu long) để cộng tổng cho chính xác.
    /// </summary>
    public class FrmTaiKhoan : Form
    {
        private TextBox txtSoTK, txtTenKH, txtDiaChi, txtSoTien, txtTong;
        private Button btnThem, btnLuu, btnXoa, btnThoat;
        private ListView lstv;

        public FrmTaiKhoan()
        {
            Helper.CauHinhForm(this, "frmBaiTap2", 480, 420);

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ THÔNG TIN TÀI KHOẢN", Left = 0, Top = 10, Width = 480, Height = 32,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.Blue
            };

            txtSoTK = Helper.Txt(190, 55, 270);
            txtTenKH = Helper.Txt(190, 85, 270);
            txtDiaChi = Helper.Txt(190, 115, 270);
            txtSoTien = Helper.Txt(190, 145, 270);
            txtSoTien.TextAlign = HorizontalAlignment.Right;

            btnThem = Helper.Btn("Thêm", 190, 180, 62);
            btnLuu = Helper.Btn("Lưu", 258, 180, 62);
            btnXoa = Helper.Btn("Xóa", 326, 180, 62);
            btnThoat = Helper.Btn("Thoát", 394, 180, 66);
            btnLuu.Enabled = false;    // trạng thái ban đầu
            btnXoa.Enabled = false;

            lstv = new ListView { Left = 10, Top = 220, Width = 460, Height = 150 };
            lstv.View = View.Details;
            lstv.GridLines = true;
            lstv.FullRowSelect = true;
            lstv.MultiSelect = false;
            lstv.Columns.Add("STT", 40);
            lstv.Columns.Add("Số tài khoản", 100);
            lstv.Columns.Add("Tên khách hàng", 120);
            lstv.Columns.Add("Địa chỉ", 90);
            ColumnHeader colTien = new ColumnHeader { Text = "Số tiền", Width = 95, TextAlign = HorizontalAlignment.Right };
            lstv.Columns.Add(colTien);

            Label lblTong = Helper.Lbl("Tổng tiền", 300, 380, 70);
            txtTong = Helper.Txt(370, 380, 100);
            txtTong.ReadOnly = true;
            txtTong.TextAlign = HorizontalAlignment.Right;

            btnThem.Click += btnThem_Click;
            btnLuu.Click += btnLuu_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += btnThoat_Click;
            lstv.SelectedIndexChanged += lstv_SelectedIndexChanged;
            this.FormClosing += FrmTaiKhoan_FormClosing;

            this.Controls.AddRange(new Control[] {
                lblTieuDe,
                Helper.Lbl("Số tài khoản", 10, 55, 170), txtSoTK,
                Helper.Lbl("Tên khách hàng", 10, 85, 170), txtTenKH,
                Helper.Lbl("Địa chỉ khách hàng", 10, 115, 170), txtDiaChi,
                Helper.Lbl("Số tiền trong tài khoản", 10, 145, 170), txtSoTien,
                btnThem, btnLuu, btnXoa, btnThoat, lstv, lblTong, txtTong });
            // Căn phải nhãn
            foreach (Control c in this.Controls)
                if (c is Label && c.Left == 10) ((Label)c).TextAlign = ContentAlignment.TopRight;
        }

        private void XoaTrangTextBox()
        {
            txtSoTK.Clear();
            txtTenKH.Clear();
            txtDiaChi.Clear();
            txtSoTien.Clear();
        }

        // Thoát chế độ thêm: nút Thêm = "Thêm", khóa nút Lưu, xóa trắng textbox
        private void VeCheDoThuong()
        {
            btnThem.Text = "Thêm";
            btnLuu.Enabled = false;
            XoaTrangTextBox();
        }

        // Thêm <-> Hủy
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                lstv.SelectedItems.Clear();      // bỏ chọn dòng (nút Xóa sẽ tự khóa)
                btnThem.Text = "Hủy";
                btnLuu.Enabled = true;
                XoaTrangTextBox();
                txtSoTK.Focus();                 // đặt con trỏ vào Số tài khoản
            }
            else
            {
                VeCheDoThuong();                 // bấm "Hủy" -> quay lại trạng thái ban đầu
            }
        }

        // Lưu dữ liệu từ textbox vào listview
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string soTK = txtSoTK.Text.Trim();
            string tenKH = txtTenKH.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            long tien;

            if (soTK == "" || tenKH == "")
            {
                MessageBox.Show("Số tài khoản và Tên khách hàng không được để trống!", "Thông báo");
                return;
            }
            if (!long.TryParse(txtSoTien.Text.Replace(",", "").Trim(), out tien) || tien < 0)
            {
                MessageBox.Show("Số tiền phải là số không âm!", "Thông báo");
                txtSoTien.Focus();
                return;
            }
            foreach (ListViewItem it in lstv.Items)
            {
                if (it.SubItems[1].Text == soTK)
                {
                    MessageBox.Show("Số tài khoản đã tồn tại!", "Thông báo");
                    txtSoTK.Focus();
                    return;
                }
            }

            // STT tự động = số dòng hiện có + 1
            ListViewItem item = new ListViewItem(new string[] {
                (lstv.Items.Count + 1).ToString(), soTK, tenKH, diaChi,
                tien.ToString("N0", CultureInfo.InvariantCulture) });
            item.Tag = tien;                     // giữ số tiền dạng số để cộng tổng
            lstv.Items.Add(item);

            CapNhatTong();
            VeCheDoThuong();
        }

        // Xóa dòng đang chọn -> đánh lại STT, tính lại tổng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lstv.SelectedItems.Count == 0) return;
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                lstv.SelectedItems[0].Remove();
                for (int i = 0; i < lstv.Items.Count; i++)   // đánh lại STT
                    lstv.Items[i].SubItems[0].Text = (i + 1).ToString();
                CapNhatTong();
                XoaTrangTextBox();
                btnXoa.Enabled = false;
            }
        }

        // Chọn 1 khách hàng -> hiện thông tin lên textbox, bật nút Xóa
        private void lstv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstv.SelectedItems.Count == 0)
            {
                btnXoa.Enabled = false;
                return;
            }
            ListViewItem item = lstv.SelectedItems[0];
            VeCheDoThuong();                     // đang ở chế độ thêm mà click dòng -> thoát chế độ thêm
            txtSoTK.Text = item.SubItems[1].Text;
            txtTenKH.Text = item.SubItems[2].Text;
            txtDiaChi.Text = item.SubItems[3].Text;
            txtSoTien.Text = ((long)item.Tag).ToString();
            btnXoa.Enabled = true;
        }

        // Tổng tiền của tất cả khách hàng
        private void CapNhatTong()
        {
            long tong = 0;
            foreach (ListViewItem it in lstv.Items)
                tong += (long)it.Tag;
            txtTong.Text = tong.ToString("N0", CultureInfo.InvariantCulture);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
