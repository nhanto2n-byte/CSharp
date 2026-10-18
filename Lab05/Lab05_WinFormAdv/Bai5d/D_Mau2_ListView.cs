using System;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài mẫu 2: ListView (Details) với các nút Thêm / Xóa / Sửa.
    /// </summary>
    public class FrmListViewMau : Form
    {
        private Button btnthem, btnxoa, btnsua;
        private TextBox txtmssv, txthoten;
        private ListView lstv1;

        public FrmListViewMau()
        {
            Helper.CauHinhForm(this, "ListView", 440, 360);

            btnthem = Helper.Btn("Thêm", 20, 12, 110);
            btnxoa = Helper.Btn("Xóa", 150, 12, 110);
            btnsua = Helper.Btn("Sửa", 280, 12, 110);

            Label lblMa = Helper.Lbl("Mã SV", 20, 60, 60);
            txtmssv = Helper.Txt(90, 60, 300);
            Label lblTen = Helper.Lbl("Họ tên", 20, 95, 60);
            txthoten = Helper.Txt(90, 95, 300);

            // Bước 1-3: thiết kế ListView (View = Details, 2 cột, GridLines, FullRowSelect)
            lstv1 = new ListView { Left = 20, Top = 135, Width = 370, Height = 205 };
            lstv1.View = View.Details;
            lstv1.GridLines = true;
            lstv1.FullRowSelect = true;
            lstv1.Columns.Add("MÃ SỐ SV", 110);
            lstv1.Columns.Add("HỌ VÀ TÊN", 235);

            btnthem.Click += btnthem_Click;
            btnxoa.Click += btnxoa_Click;
            btnsua.Click += btnsua_Click;
            lstv1.SelectedIndexChanged += lstv1_SelectedIndexChanged;

            this.Controls.AddRange(new Control[] { btnthem, btnxoa, btnsua, lblMa, txtmssv, lblTen, txthoten, lstv1 });
        }

        // Kiểm tra trùng mã số sinh viên (boQua: dòng không cần so sánh, dùng khi sửa)
        private bool TrungMaSV(string ma, ListViewItem boQua)
        {
            foreach (ListViewItem it in lstv1.Items)
            {
                if (it != boQua && string.Compare(it.Text, ma, true) == 0)
                    return true;
            }
            return false;
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string ma = txtmssv.Text.Trim();
            string ten = txthoten.Text.Trim();
            if (ma == "" || ten == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ Mã SV và Họ tên!", "Thông báo");
                return;
            }
            if (TrungMaSV(ma, null))
            {
                MessageBox.Show("Mã số sinh viên đã tồn tại!", "Thông báo");
                txtmssv.Focus();
                return;
            }

            // cách 1: lstv1.Items.Add(ma).SubItems.Add(ten);
            // cách 2: tạo ListViewItem + ListViewSubItem rồi Add từng bước
            // cách 3 (đang dùng): tạo ListViewItem từ mảng chuỗi (mỗi phần tử = 1 cột)
            ListViewItem item1 = new ListViewItem(new string[] { ma, ten });
            lstv1.Items.Add(item1);

            txtmssv.Clear();
            txthoten.Clear();
            txtmssv.Focus();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (lstv1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần xóa!", "Thông báo");
                return;
            }
            // Xóa từ dưới lên để chỉ số không bị lệch
            for (int i = lstv1.SelectedItems.Count - 1; i >= 0; i--)
                lstv1.SelectedItems[i].Remove();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (lstv1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần sửa!", "Thông báo");
                return;
            }
            ListViewItem item = lstv1.SelectedItems[0];
            string ma = txtmssv.Text.Trim();
            string ten = txthoten.Text.Trim();
            if (ma == "" || ten == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ Mã SV và Họ tên!", "Thông báo");
                return;
            }
            if (TrungMaSV(ma, item))
            {
                MessageBox.Show("Mã số sinh viên đã tồn tại!", "Thông báo");
                return;
            }
            item.SubItems[0].Text = ma;
            item.SubItems[1].Text = ten;
        }

        // Chọn 1 dòng -> hiển thị lên 2 textbox để tiện sửa
        private void lstv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstv1.SelectedItems.Count > 0)
            {
                txtmssv.Text = lstv1.SelectedItems[0].SubItems[0].Text;
                txthoten.Text = lstv1.SelectedItems[0].SubItems[1].Text;
            }
        }
    }
}
