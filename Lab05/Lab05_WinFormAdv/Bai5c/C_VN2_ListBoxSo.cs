using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài về nhà 2: thêm các số tự nhiên N (nhập từ textbox) vào ListBox chọn nhiều dòng
    /// và xử lý bằng các button lệnh. Giá trị lưu trong ListBox kiểu long.
    /// </summary>
    public class FrmListBoxSo : Form
    {
        private TextBox txtN;
        private ListBox lstSo;
        private Button btnNhap, btnTong, btnXoaDauCuoi, btnXoaChon, btnTang2, btnBinhPhuong, btnChonChan, btnChonLe, btnKetThuc;

        public FrmListBoxSo()
        {
            Helper.CauHinhForm(this, "frmListbox", 450, 400);

            Label lblTitle = new Label
            {
                Text = "LISTBOX", Left = 0, Top = 8, Width = 450, Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.DarkOrange
            };

            GroupBox grpList = new GroupBox { Text = "Listbox", Left = 15, Top = 50, Width = 190, Height = 280 };
            txtN = Helper.Txt(15, 25, 160);
            btnNhap = Helper.Btn("&Nhập", 15, 58, 160);
            lstSo = new ListBox { Left = 15, Top = 98, Width = 160, Height = 170, SelectionMode = SelectionMode.MultiExtended };
            grpList.Controls.AddRange(new Control[] { txtN, btnNhap, lstSo });

            GroupBox grpXuLy = new GroupBox { Text = "Xử lý Listbox", Left = 215, Top = 50, Width = 220, Height = 280 };
            btnTong = Helper.Btn("Tổng các phần tử trong List", 10, 22, 200);
            btnXoaDauCuoi = Helper.Btn("Xóa Phần tử đầu và cuối", 10, 58, 200);
            btnXoaChon = Helper.Btn("Xóa Phần tử đang chọn", 10, 94, 200);
            btnTang2 = Helper.Btn("Tăng mỗi phần tử lên 2", 10, 130, 200);
            btnBinhPhuong = Helper.Btn("Thay bằng bình phương", 10, 166, 200);
            btnChonChan = Helper.Btn("Chọn số chẵn", 10, 202, 200);
            btnChonLe = Helper.Btn("Chọn số lẻ", 10, 238, 200);
            grpXuLy.Controls.AddRange(new Control[] { btnTong, btnXoaDauCuoi, btnXoaChon, btnTang2, btnBinhPhuong, btnChonChan, btnChonLe });

            btnKetThuc = Helper.Btn("&KẾT THÚC", 15, 345, 420);

            btnNhap.Click += btnNhap_Click;
            btnTong.Click += btnTong_Click;
            btnXoaDauCuoi.Click += btnXoaDauCuoi_Click;
            btnXoaChon.Click += btnXoaChon_Click;
            btnTang2.Click += btnTang2_Click;
            btnBinhPhuong.Click += btnBinhPhuong_Click;
            btnChonChan.Click += btnChonChan_Click;
            btnChonLe.Click += btnChonLe_Click;
            btnKetThuc.Click += (s, e) => this.Close();
            this.AcceptButton = btnNhap;   // nhấn Enter = bấm Nhập

            this.Controls.AddRange(new Control[] { lblTitle, grpList, grpXuLy, btnKetThuc });
        }

        // Thêm số tự nhiên N vào listbox
        private void btnNhap_Click(object sender, EventArgs e)
        {
            long n;
            if (!long.TryParse(txtN.Text.Trim(), out n) || n < 0 || n > 1000000)
            {
                MessageBox.Show("Hãy nhập một số tự nhiên từ 0 đến 1.000.000!", "Thông báo");
                txtN.SelectAll();
                txtN.Focus();
                return;
            }
            lstSo.Items.Add(n);
            txtN.Clear();
            txtN.Focus();
        }

        private bool CoDuLieu()
        {
            if (lstSo.Items.Count == 0)
            {
                MessageBox.Show("Listbox đang rỗng!", "Thông báo");
                return false;
            }
            return true;
        }

        // Tính tổng các phần tử -> MessageBox
        private void btnTong_Click(object sender, EventArgs e)
        {
            if (!CoDuLieu()) return;
            long tong = 0;
            foreach (object o in lstSo.Items)
                tong += (long)o;
            MessageBox.Show("Tổng các phần tử: " + tong, "Kết quả");
        }

        // Xóa phần tử đầu và cuối
        private void btnXoaDauCuoi_Click(object sender, EventArgs e)
        {
            if (!CoDuLieu()) return;
            lstSo.Items.RemoveAt(lstSo.Items.Count - 1);   // xóa cuối trước để không lệch chỉ số
            if (lstSo.Items.Count > 0)
                lstSo.Items.RemoveAt(0);                    // (nếu chỉ có 1 phần tử thì đã xóa xong)
        }

        // Xóa các phần tử đang chọn
        private void btnXoaChon_Click(object sender, EventArgs e)
        {
            if (lstSo.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Hãy chọn phần tử cần xóa!", "Thông báo");
                return;
            }
            List<int> ds = new List<int>();
            foreach (int i in lstSo.SelectedIndices)
                ds.Add(i);
            for (int k = ds.Count - 1; k >= 0; k--)
                lstSo.Items.RemoveAt(ds[k]);
        }

        // Tăng mỗi phần tử lên 2
        private void btnTang2_Click(object sender, EventArgs e)
        {
            if (!CoDuLieu()) return;
            for (int i = 0; i < lstSo.Items.Count; i++)
                lstSo.Items[i] = (long)lstSo.Items[i] + 2;
        }

        // Thay mỗi phần tử bằng bình phương của chính nó (checked: báo lỗi nếu tràn số)
        private void btnBinhPhuong_Click(object sender, EventArgs e)
        {
            if (!CoDuLieu()) return;
            try
            {
                for (int i = 0; i < lstSo.Items.Count; i++)
                {
                    long x = (long)lstSo.Items[i];
                    lstSo.Items[i] = checked(x * x);
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("Giá trị quá lớn, không thể bình phương tiếp!", "Thông báo");
            }
        }

        // Chọn các phần tử là số chẵn
        private void btnChonChan_Click(object sender, EventArgs e)
        {
            ChonTheoTinhChan(0);
        }

        // Chọn các phần tử là số lẻ
        private void btnChonLe_Click(object sender, EventArgs e)
        {
            ChonTheoTinhChan(1);
        }

        // du = 0: chọn số chẵn; du = 1: chọn số lẻ
        private void ChonTheoTinhChan(int du)
        {
            if (!CoDuLieu()) return;
            lstSo.ClearSelected();
            for (int i = 0; i < lstSo.Items.Count; i++)
                if ((long)lstSo.Items[i] % 2 == du)
                    lstSo.SetSelected(i, true);
        }
    }
}
