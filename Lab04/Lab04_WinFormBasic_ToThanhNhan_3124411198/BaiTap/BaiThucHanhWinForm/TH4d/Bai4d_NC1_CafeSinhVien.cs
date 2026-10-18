using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập nâng cao, Bài 1: Quản lý thu tiền quán Cafe Sinh Viên.
    /// Tiền của một nhóm = (giá nước uống + tổng giá các món ăn) x số khách.
    /// Nếu tick "Sinh viên" thì giảm 20%.
    /// </summary>
    public class Bai4d_NC1_CafeSinhVien : Form
    {
        private TextBox txtTenKH, txtSoKH, txtTongKhach, txtTongTien;
        private CheckBox chkSinhVien;
        private Button btnTinhTien, btnNhapLai, btnThanhToan, btnThoat;

        // Nước uống: mỗi radio ứng với một giá
        private readonly Dictionary<RadioButton, int> _giaNuoc = new Dictionary<RadioButton, int>();
        // Thức ăn: mỗi checkbox ứng với một giá
        private readonly Dictionary<CheckBox, int> _giaMon = new Dictionary<CheckBox, int>();

        private const double GiamGiaSinhVien = 0.2;    // giảm 20%

        // Dữ liệu của nhóm khách đang tính tiền
        private long _tienNhomHienTai = 0;
        private int _soKhachHienTai = 0;

        // Tổng cộng của cả buổi (cộng dồn khi bấm Thanh toán)
        private long _tongKhach = 0;
        private long _tongTien = 0;

        public Bai4d_NC1_CafeSinhVien()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Thanh toán tiền";
            Font = new Font("Tahoma", 9.5F);
            ClientSize = new Size(520, 470);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "CAFE SINH VIÊN",
                Font = new Font("Tahoma", 13F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 10),
                Size = new Size(520, 30)
            };

            var lblTen = UiHelper.TaoLabel("Tên khách hàng", 20, 55);
            lblTen.Font = new Font(Font, FontStyle.Bold);
            txtTenKH = UiHelper.TaoTextBox(160, 52, 340);
            var lblSo = UiHelper.TaoLabel("Số khách hàng", 20, 88);
            lblSo.Font = new Font(Font, FontStyle.Bold);
            txtSoKH = UiHelper.TaoTextBox(160, 85, 340);
            chkSinhVien = UiHelper.TaoCheckBox("Sinh viên ?", 260, 120);

            // ===== Nhóm Nước uống (RadioButton) =====
            var grpNuoc = UiHelper.TaoGroupBox("Nước uống", 20, 150, 230, 150);
            ThemRadioNuoc(grpNuoc, "Cafe đen", 20000, 15, 25);
            ThemRadioNuoc(grpNuoc, "Cafe đá", 25000, 120, 25);
            ThemRadioNuoc(grpNuoc, "Cafe sữa", 25000, 15, 60);
            ThemRadioNuoc(grpNuoc, "Cafe kem", 35000, 120, 60);
            ThemRadioNuoc(grpNuoc, "Cafe sữa đá", 30000, 15, 95);

            // ===== Nhóm Thức ăn (CheckBox) =====
            var grpAn = UiHelper.TaoGroupBox("Thức ăn", 260, 150, 240, 150);
            ThemCheckMon(grpAn, "Bánh mỳ trứng", 15000, 12, 25);
            ThemCheckMon(grpAn, "Mỳ xào bò", 30000, 125, 25);
            ThemCheckMon(grpAn, "Bánh mỳ cá", 15000, 12, 60);
            ThemCheckMon(grpAn, "Mỳ cay", 50000, 125, 60);
            ThemCheckMon(grpAn, "Mỳ tôm trứng", 20000, 12, 95);

            btnTinhTien = UiHelper.TaoButton("Tính tiền", 20, 320, 110, 35);
            btnNhapLai = UiHelper.TaoButton("Nhập lại", 145, 320, 110, 35);
            btnThanhToan = UiHelper.TaoButton("Thanh toán", 270, 320, 110, 35);
            btnThoat = UiHelper.TaoButton("Thoát", 395, 320, 105, 35);

            var lblTK = UiHelper.TaoLabel("Tổng khách hàng", 20, 380);
            lblTK.Font = new Font(Font, FontStyle.Bold);
            txtTongKhach = UiHelper.TaoTextBox(180, 377, 320, true);
            var lblTT = UiHelper.TaoLabel("Tổng tiền thanh toán", 20, 415);
            lblTT.Font = new Font(Font, FontStyle.Bold);
            txtTongTien = UiHelper.TaoTextBox(180, 412, 320, true);

            // ===== Sự kiện =====
            txtSoKH.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e);   // số khách chỉ cho nhập số
            txtTenKH.TextChanged += DuLieu_Changed;
            txtSoKH.TextChanged += DuLieu_Changed;
            chkSinhVien.CheckedChanged += DuLieu_Changed;
            foreach (var r in _giaNuoc.Keys) r.CheckedChanged += DuLieu_Changed;
            foreach (var c in _giaMon.Keys) c.CheckedChanged += DuLieu_Changed;

            btnTinhTien.Click += btnTinhTien_Click;
            btnNhapLai.Click += btnNhapLai_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { lblTieuDe, lblTen, txtTenKH, lblSo, txtSoKH, chkSinhVien,
                grpNuoc, grpAn, btnTinhTien, btnNhapLai, btnThanhToan, btnThoat,
                lblTK, txtTongKhach, lblTT, txtTongTien });

            Load += Form_Load;
        }

        private void ThemRadioNuoc(GroupBox grp, string ten, int gia, int x, int y)
        {
            var r = UiHelper.TaoRadio(ten, x, y);
            _giaNuoc[r] = gia;
            grp.Controls.Add(r);
        }

        private void ThemCheckMon(GroupBox grp, string ten, int gia, int x, int y)
        {
            var c = UiHelper.TaoCheckBox(ten, x, y);
            _giaMon[c] = gia;
            grp.Controls.Add(c);
        }

        // Form_Load: con trỏ ở ô tên, 3 nút Tính tiền / Nhập lại / Thanh toán bị mờ
        private void Form_Load(object sender, EventArgs e)
        {
            DatLaiForm();
        }

        // Đưa form về trạng thái ban đầu để nhập nhóm khách mới
        private void DatLaiForm()
        {
            txtTenKH.Clear();
            txtSoKH.Clear();
            chkSinhVien.Checked = false;
            foreach (var r in _giaNuoc.Keys) r.Checked = false;
            foreach (var c in _giaMon.Keys) c.Checked = false;

            _tienNhomHienTai = 0;
            _soKhachHienTai = 0;

            btnTinhTien.Enabled = false;
            btnNhapLai.Enabled = false;
            btnThanhToan.Enabled = false;
            txtTenKH.Focus();
        }

        // Tên khách hàng không được trống, số khách > 0 và đã chọn 1 loại nước uống
        // (thức ăn là tùy chọn vì có nhóm khách chỉ gọi nước)
        private bool DuThongTin()
        {
            bool coTen = txtTenKH.Text.Trim().Length > 0;
            bool coSoKhach = int.TryParse(txtSoKH.Text, out int soKhach) && soKhach > 0;
            bool coNuoc = false;
            foreach (var r in _giaNuoc.Keys) if (r.Checked) coNuoc = true;
            return coTen && coSoKhach && coNuoc;
        }

        // Dữ liệu thay đổi -> bật/tắt nút Tính tiền; kết quả cũ không còn đúng nên khóa Thanh toán
        private void DuLieu_Changed(object sender, EventArgs e)
        {
            btnTinhTien.Enabled = DuThongTin();
            btnThanhToan.Enabled = false;
        }

        // Tính tiền cho nhóm khách vừa nhập, hiển thị bằng MessageBox
        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            int soKhach = int.Parse(txtSoKH.Text);

            long giaMotNguoi = 0;
            string mon = "";
            foreach (var kv in _giaNuoc)
                if (kv.Key.Checked) { giaMotNguoi += kv.Value; mon += "- " + kv.Key.Text + ": " + kv.Value.ToString("N0") + "đ\n"; }
            foreach (var kv in _giaMon)
                if (kv.Key.Checked) { giaMotNguoi += kv.Value; mon += "- " + kv.Key.Text + ": " + kv.Value.ToString("N0") + "đ\n"; }

            double tien = giaMotNguoi * soKhach;
            string giam = "";
            if (chkSinhVien.Checked)
            {
                tien = tien * (1 - GiamGiaSinhVien);
                giam = "Giảm giá sinh viên: 20%\n";
            }

            _tienNhomHienTai = (long)Math.Round(tien);
            _soKhachHienTai = soKhach;

            MessageBox.Show("Khách hàng: " + txtTenKH.Text.Trim() + "\n"
                + "Số khách: " + soKhach + "\n"
                + mon + giam
                + "Thành tiền: " + _tienNhomHienTai.ToString("N0") + "đ",
                "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnNhapLai.Enabled = true;
            btnThanhToan.Enabled = true;
        }

        // Nhập lại: về trạng thái ban đầu
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            DatLaiForm();
        }

        // Thanh toán: cộng dồn vào tổng, hiện lên ô tổng, sẵn sàng cho nhóm khách mới
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            _tongKhach += _soKhachHienTai;
            _tongTien += _tienNhomHienTai;
            txtTongKhach.Text = _tongKhach.ToString();
            txtTongTien.Text = _tongTien.ToString("N0") + "đ";

            DatLaiForm();   // btnThanhToan, btnNhapLai bị mờ
        }
    }
}
