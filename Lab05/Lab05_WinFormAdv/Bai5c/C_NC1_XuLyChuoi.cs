using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài tập nâng cao 1: xử lý chuỗi trên ListBox (50 tên ngẫu nhiên).
    /// </summary>
    public class FrmXuLyChuoi : Form
    {
        private ListBox lstTen;
        private Button btnNgauNhien, btnXoaChon, btnXoaSon, btnXoaLe, btnHoa, btnThuong, btnHoaDau, btnXoaTatCa;

        // Các mảng dữ liệu để ghép tên ngẫu nhiên
        private readonly string[] HO = { "Lê", "Nguyễn", "Lý", "Trần", "Lâm", "Hồ", "Lai", "Huỳnh", "La" };
        private readonly string[] TENLOT = { "Quang", "Thành", "Ngọc", "Anh", "Xuân", "Bảo", "Cẩm", "Thị", "Kim", "Thái", "Hồng" };
        private readonly string[] TEN = { "Hà", "Danh", "Sơn", "Mai", "Thắng", "Kỳ", "Thành", "Lâm", "Tâm", "Phụng", "Thắm" };
        private Random rd = new Random();

        public FrmXuLyChuoi()
        {
            Helper.CauHinhForm(this, "frmChuoi", 560, 380);

            btnNgauNhien = Helper.Btn("Nhập tên ngẫu nhiên", 20, 15, 190);
            lstTen = new ListBox { Left = 20, Top = 55, Width = 190, Height = 310, SelectionMode = SelectionMode.MultiExtended };

            btnXoaChon = Helper.Btn("Xóa phần tử đang chọn", 225, 55, 315);
            btnXoaSon = Helper.Btn("Xóa phần tử có tên là Sơn", 225, 100, 315);
            btnXoaLe = Helper.Btn("Xóa phần tử có họ là Lê", 225, 145, 315);
            btnHoa = Helper.Btn("Chuyển PT đang chọn thành chữ HOA", 225, 190, 315);
            btnThuong = Helper.Btn("Chuyển PT đang chọn thành chữ thường", 225, 235, 315);
            btnHoaDau = Helper.Btn("Chuyển PT đang chọn thành viết Hoa đầu mỗi từ", 225, 280, 315);
            btnXoaTatCa = Helper.Btn("Xóa tất cả các phần tử", 225, 325, 315);

            btnNgauNhien.Click += btnNgauNhien_Click;
            btnXoaChon.Click += btnXoaChon_Click;
            btnXoaSon.Click += btnXoaSon_Click;
            btnXoaLe.Click += btnXoaLe_Click;
            btnHoa.Click += btnHoa_Click;
            btnThuong.Click += btnThuong_Click;
            btnHoaDau.Click += btnHoaDau_Click;
            btnXoaTatCa.Click += btnXoaTatCa_Click;
            lstTen.DoubleClick += lstTen_DoubleClick;

            this.Controls.AddRange(new Control[] { btnNgauNhien, lstTen, btnXoaChon, btnXoaSon, btnXoaLe, btnHoa, btnThuong, btnHoaDau, btnXoaTatCa });
        }

        // Thêm 50 tên = Họ + Tên lót + Tên, chọn ngẫu nhiên từ 3 mảng
        private void btnNgauNhien_Click(object sender, EventArgs e)
        {
            lstTen.BeginUpdate();
            for (int i = 0; i < 50; i++)
            {
                string ten = HO[rd.Next(HO.Length)] + " " + TENLOT[rd.Next(TENLOT.Length)] + " " + TEN[rd.Next(TEN.Length)];
                lstTen.Items.Add(ten);
            }
            lstTen.EndUpdate();
        }

        // Lấy danh sách chỉ số các dòng đang chọn (copy ra List để thao tác an toàn)
        private List<int> LayChiSoDangChon()
        {
            List<int> ds = new List<int>();
            foreach (int i in lstTen.SelectedIndices)
                ds.Add(i);
            return ds;
        }

        private bool CoChon()
        {
            if (lstTen.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất một phần tử!", "Thông báo");
                return false;
            }
            return true;
        }

        // Tách tên thành các từ
        private string[] TachTu(string ten)
        {
            return ten.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnXoaChon_Click(object sender, EventArgs e)
        {
            if (!CoChon()) return;
            List<int> ds = LayChiSoDangChon();
            // Xóa từ cuối lên đầu để không bị lệch chỉ số
            for (int k = ds.Count - 1; k >= 0; k--)
                lstTen.Items.RemoveAt(ds[k]);
        }

        // Xóa các phần tử có TÊN (từ cuối) là "Sơn"
        private void btnXoaSon_Click(object sender, EventArgs e)
        {
            for (int i = lstTen.Items.Count - 1; i >= 0; i--)
            {
                string[] t = TachTu(lstTen.Items[i].ToString());
                if (t.Length > 0 && string.Compare(t[t.Length - 1], "Sơn", true) == 0)
                    lstTen.Items.RemoveAt(i);
            }
        }

        // Xóa các phần tử có HỌ (từ đầu) là "Lê"
        private void btnXoaLe_Click(object sender, EventArgs e)
        {
            for (int i = lstTen.Items.Count - 1; i >= 0; i--)
            {
                string[] t = TachTu(lstTen.Items[i].ToString());
                if (t.Length > 0 && string.Compare(t[0], "Lê", true) == 0)
                    lstTen.Items.RemoveAt(i);
            }
        }

        // Đổi các phần tử đang chọn: kiểu = 1 (HOA), 2 (thường), 3 (Hoa đầu mỗi từ)
        private void DoiKieuChu(int kieu)
        {
            if (!CoChon()) return;
            List<int> ds = LayChiSoDangChon();
            TextInfo ti = new CultureInfo("vi-VN").TextInfo;

            lstTen.BeginUpdate();
            foreach (int i in ds)
            {
                string s = lstTen.Items[i].ToString();
                if (kieu == 1) s = s.ToUpper();
                else if (kieu == 2) s = s.ToLower();
                else s = ti.ToTitleCase(s.ToLower());
                lstTen.Items[i] = s;          // gán lại sẽ làm mất trạng thái chọn
            }
            foreach (int i in ds)
                lstTen.SetSelected(i, true);  // chọn lại các dòng cũ
            lstTen.EndUpdate();
        }

        private void btnHoa_Click(object sender, EventArgs e) { DoiKieuChu(1); }
        private void btnThuong_Click(object sender, EventArgs e) { DoiKieuChu(2); }
        private void btnHoaDau_Click(object sender, EventArgs e) { DoiKieuChu(3); }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            if (lstTen.Items.Count == 0) return;
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa tất cả?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
                lstTen.Items.Clear();
        }

        // Double click vào tên đang chọn -> hộp nhập để đổi tên mới tại vị trí đó
        private void lstTen_DoubleClick(object sender, EventArgs e)
        {
            int vitri = lstTen.SelectedIndex;
            if (vitri < 0) return;
            string moi = Helper.InputBox("Nhập tên mới:", "Đổi tên", lstTen.Items[vitri].ToString());
            if (moi != null && moi.Trim() != "")
                lstTen.Items[vitri] = moi.Trim();
        }
    }
}
