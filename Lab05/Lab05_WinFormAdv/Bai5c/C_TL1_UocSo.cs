using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài tập tại lớp 1: nhập số, lưu vào ComboBox, liệt kê ước số vào ListBox
    /// và tính tổng / đếm ước số chẵn / đếm ước số nguyên tố.
    /// HotKey: Alt + ký tự được gạch chân (dấu & trước ký tự trong Text của button).
    /// </summary>
    public class FrmUocSo : Form
    {
        private TextBox txtSo;
        private ComboBox cboSo;
        private ListBox lstUocSo;
        private Button btnCapNhat, btnTong, btnChan, btnNguyenTo, btnThoat;

        public FrmUocSo()
        {
            Helper.CauHinhForm(this, "Combobox", 520, 320);

            GroupBox grpNhap = new GroupBox { Text = "Nhập Số", Left = 15, Top = 10, Width = 220, Height = 110 };
            txtSo = Helper.Txt(15, 28, 80);
            btnCapNhat = Helper.Btn("&Cập nhật", 105, 26, 100);
            cboSo = new ComboBox { Left = 15, Top = 70, Width = 190, DropDownStyle = ComboBoxStyle.DropDownList };
            grpNhap.Controls.AddRange(new Control[] { txtSo, btnCapNhat, cboSo });

            GroupBox grpUoc = new GroupBox { Text = "Danh sách các ước số", Left = 250, Top = 10, Width = 255, Height = 150 };
            lstUocSo = new ListBox { Left = 15, Top = 22, Width = 225, Height = 115 };
            grpUoc.Controls.Add(lstUocSo);

            btnTong = Helper.Btn("&Tổng các ước số", 250, 170, 255);
            btnChan = Helper.Btn("Số lượng các ước số c&hẵn", 250, 208, 255);
            btnNguyenTo = Helper.Btn("Số lượng các ước số &nguyên tố", 250, 246, 255);
            btnThoat = Helper.Btn("Th&oát", 130, 246, 100);

            btnCapNhat.Click += btnCapNhat_Click;
            cboSo.SelectedIndexChanged += cboSo_SelectedIndexChanged;
            btnTong.Click += btnTong_Click;
            btnChan.Click += btnChan_Click;
            btnNguyenTo.Click += btnNguyenTo_Click;
            btnThoat.Click += btnThoat_Click;
            this.FormClosing += FrmUocSo_FormClosing;

            this.Controls.AddRange(new Control[] { grpNhap, grpUoc, btnTong, btnChan, btnNguyenTo, btnThoat });
        }

        // Kiểm tra dữ liệu nhập -> thêm vào combobox (không trùng) -> xóa textbox, đặt lại con trỏ
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int n;
            if (!int.TryParse(txtSo.Text.Trim(), out n) || n <= 0)
            {
                MessageBox.Show("Hãy nhập một số nguyên dương hợp lệ!", "Thông báo");
                txtSo.SelectAll();
                txtSo.Focus();
                return;
            }
            if (cboSo.Items.Contains(n))     // combobox lưu số nguyên (int)
            {
                MessageBox.Show("Số " + n + " đã tồn tại trong combobox!", "Thông báo");
            }
            else
            {
                cboSo.Items.Add(n);
            }
            txtSo.Clear();
            txtSo.Focus();
        }

        // Chọn 1 số trong combobox -> hiển thị các ước số vào listbox
        private void cboSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstUocSo.Items.Clear();
            if (cboSo.SelectedIndex < 0) return;

            int n = (int)cboSo.SelectedItem;
            List<int> ds = new List<int>();
            // Chỉ duyệt đến căn bậc hai của n: mỗi ước i đi kèm ước n/i
            for (int i = 1; (long)i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    ds.Add(i);
                    if (i != n / i) ds.Add(n / i);
                }
            }
            ds.Sort();
            foreach (int u in ds)
                lstUocSo.Items.Add(u);
        }

        // Kiểm tra đã có ước số trên listbox chưa
        private bool CoUocSo()
        {
            if (lstUocSo.Items.Count == 0)
            {
                MessageBox.Show("Hãy chọn một số trong combobox trước!", "Thông báo");
                return false;
            }
            return true;
        }

        private void btnTong_Click(object sender, EventArgs e)
        {
            if (!CoUocSo()) return;
            long tong = 0;
            foreach (object o in lstUocSo.Items)
                tong += (int)o;
            MessageBox.Show("Tổng các ước số: " + tong, "Kết quả");
        }

        private void btnChan_Click(object sender, EventArgs e)
        {
            if (!CoUocSo()) return;
            int dem = 0;
            foreach (object o in lstUocSo.Items)
                if ((int)o % 2 == 0) dem++;
            MessageBox.Show("Số lượng các ước số chẵn: " + dem, "Kết quả");
        }

        private void btnNguyenTo_Click(object sender, EventArgs e)
        {
            if (!CoUocSo()) return;
            int dem = 0;
            foreach (object o in lstUocSo.Items)
                if (LaNguyenTo((int)o)) dem++;
            MessageBox.Show("Số lượng các ước số nguyên tố: " + dem, "Kết quả");
        }

        private bool LaNguyenTo(int n)
        {
            if (n < 2) return false;
            for (int i = 2; (long)i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();   // FormClosing sẽ hỏi xác nhận
        }

        private void FrmUocSo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
