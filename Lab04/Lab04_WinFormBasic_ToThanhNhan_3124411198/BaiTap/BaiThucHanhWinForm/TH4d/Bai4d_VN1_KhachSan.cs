using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập về nhà, Bài 1: Quản lý thanh toán tiền phòng Khách sạn Thanh Thanh.
    /// Bảng giá:
    ///   Phòng đơn 300.000đ/ngày, đôi 350.000đ/ngày, ba 400.000đ/ngày
    ///   Tiện nghi: mỗi loại cộng thêm 10.000đ (tính một lần)
    ///   Dịch vụ: Karaoke 50.000đ, Ăn sáng 15.000đ/ngày
    /// </summary>
    public class Bai4d_VN1_KhachSan : Form
    {
        private TextBox txtHoTen, txtDiaChi, txtSoNgay, txtThanhTien, txtSoLuot, txtTongTien;
        private RadioButton rdoDon, rdoDoi, rdoBa;
        private CheckBox chkTivi, chkInternet, chkNuocNong, chkKaraoke, chkAnSang;
        private Button btnThanhToan, btnNhapMoi, btnTongKet, btnThoat;

        // Bảng giá
        private const int GiaPhongDon = 300000;
        private const int GiaPhongDoi = 350000;
        private const int GiaPhongBa = 400000;
        private const int GiaTienNghi = 10000;
        private const int GiaKaraoke = 50000;
        private const int GiaAnSangMoiNgay = 15000;

        // Số liệu thống kê trong ngày
        private int _tongLuot = 0;
        private long _tongTien = 0;

        public Bai4d_VN1_KhachSan()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "frmDangkyKS";
            Font = new Font("Tahoma", 9.5F);
            ClientSize = new Size(760, 330);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "KHÁCH SẠN THANH THANH - TRẢ PHÒNG",
                Font = new Font("Tahoma", 14F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(10, 10),
                Size = new Size(740, 38)
            };

            // ===== Bên trái: thông tin khách =====
            var lblTen = UiHelper.TaoLabel("Họ và tên:", 25, 68);
            txtHoTen = UiHelper.TaoTextBox(105, 65, 250);
            var lblDc = UiHelper.TaoLabel("Địa chỉ:", 25, 103);
            txtDiaChi = UiHelper.TaoTextBox(105, 100, 330);
            var lblNgay = UiHelper.TaoLabel("Số ngày ở:", 25, 138);
            txtSoNgay = UiHelper.TaoTextBox(105, 135, 90);
            txtSoNgay.TextAlign = HorizontalAlignment.Right;

            var grpPhong = UiHelper.TaoGroupBox("Loại phòng", 15, 175, 130, 130);
            rdoDon = UiHelper.TaoRadio("Phòng đơn", 12, 25);
            rdoDoi = UiHelper.TaoRadio("Phòng đôi", 12, 60);
            rdoBa = UiHelper.TaoRadio("Phòng ba", 12, 95);
            grpPhong.Controls.AddRange(new Control[] { rdoDon, rdoDoi, rdoBa });

            var grpTienNghi = UiHelper.TaoGroupBox("Tiện nghi", 155, 175, 150, 130);
            chkTivi = UiHelper.TaoCheckBox("Tivi", 12, 25);
            chkInternet = UiHelper.TaoCheckBox("Internet", 12, 60);
            chkNuocNong = UiHelper.TaoCheckBox("Máy nước nóng", 12, 95);
            grpTienNghi.Controls.AddRange(new Control[] { chkTivi, chkInternet, chkNuocNong });

            var grpDichVu = UiHelper.TaoGroupBox("Dịch vụ", 315, 175, 120, 130);
            chkKaraoke = UiHelper.TaoCheckBox("Karaoke", 12, 25);
            chkAnSang = UiHelper.TaoCheckBox("Ăn sáng", 12, 60);
            grpDichVu.Controls.AddRange(new Control[] { chkKaraoke, chkAnSang });

            // ===== Bên phải: thanh toán và tổng kết =====
            btnThanhToan = UiHelper.TaoButton("&Thanh toán", 465, 62, 105, 30);
            btnNhapMoi = UiHelper.TaoButton("&Nhập mới", 585, 62, 105, 30);
            var lblTT = UiHelper.TaoLabel("Thành tiền:", 465, 112);
            txtThanhTien = UiHelper.TaoTextBox(550, 109, 195, true);
            btnTongKet = UiHelper.TaoButton("Tổng &Kết", 465, 150, 105, 30);

            var grpTongKet = UiHelper.TaoGroupBox("Thông tin tổng kết", 465, 190, 285, 90);
            txtSoLuot = UiHelper.TaoTextBox(115, 22, 160, true);
            txtTongTien = UiHelper.TaoTextBox(115, 55, 160, true);
            grpTongKet.Controls.AddRange(new Control[] {
                UiHelper.TaoLabel("Số lượt người:", 10, 25), txtSoLuot,
                UiHelper.TaoLabel("Tổng số tiền:", 10, 58), txtTongTien });

            btnThoat = UiHelper.TaoButton("Th&oát", 465, 290, 105, 30);

            Controls.AddRange(new Control[] { lblTieuDe, lblTen, txtHoTen, lblDc, txtDiaChi, lblNgay, txtSoNgay,
                grpPhong, grpTienNghi, grpDichVu, btnThanhToan, btnNhapMoi, lblTT, txtThanhTien,
                btnTongKet, grpTongKet, btnThoat });

            // ===== Sự kiện =====
            txtSoNgay.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e);   // số ngày chỉ nhập số
            txtHoTen.TextChanged += DuLieu_Changed;
            txtDiaChi.TextChanged += DuLieu_Changed;
            txtSoNgay.TextChanged += DuLieu_Changed;
            rdoDon.CheckedChanged += DuLieu_Changed;
            rdoDoi.CheckedChanged += DuLieu_Changed;
            rdoBa.CheckedChanged += DuLieu_Changed;

            btnThanhToan.Click += btnThanhToan_Click;
            btnNhapMoi.Click += btnNhapMoi_Click;
            btnTongKet.Click += btnTongKet_Click;
            btnThoat.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);
            Load += (s, e) => KhoiTaoTrangThai();
        }

        // Form_Load: con trỏ ở ô tên; TongKet, NhapMoi, ThanhToan bị mờ
        private void KhoiTaoTrangThai()
        {
            btnTongKet.Enabled = false;
            btnNhapMoi.Enabled = false;
            btnThanhToan.Enabled = false;
            txtHoTen.Focus();
        }

        // Đưa form về trạng thái nhập khách mới (không đụng tới số liệu thống kê)
        private void XoaFormNhapKhach()
        {
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSoNgay.Clear();
            txtThanhTien.Clear();
            rdoDon.Checked = rdoDoi.Checked = rdoBa.Checked = false;
            chkTivi.Checked = chkInternet.Checked = chkNuocNong.Checked = false;
            chkKaraoke.Checked = chkAnSang.Checked = false;
            btnThanhToan.Enabled = false;
            btnNhapMoi.Enabled = false;
            txtHoTen.Focus();
        }

        // Nhập đủ: tên không trống, địa chỉ không trống, số ngày > 0, đã chọn loại phòng
        private void DuLieu_Changed(object sender, EventArgs e)
        {
            bool coTen = txtHoTen.Text.Trim().Length > 0;
            bool coDiaChi = txtDiaChi.Text.Trim().Length > 0;
            bool coSoNgay = int.TryParse(txtSoNgay.Text, out int ngay) && ngay > 0;
            bool coPhong = rdoDon.Checked || rdoDoi.Checked || rdoBa.Checked;
            btnThanhToan.Enabled = coTen && coDiaChi && coSoNgay && coPhong;
        }

        // Thanh toán: tính tiền, hiện lên Thành tiền, cộng vào thống kê
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            int soNgay = int.Parse(txtSoNgay.Text);

            long tien = 0;
            if (rdoDon.Checked) tien += (long)GiaPhongDon * soNgay;
            else if (rdoDoi.Checked) tien += (long)GiaPhongDoi * soNgay;
            else tien += (long)GiaPhongBa * soNgay;

            // Mỗi tiện nghi được chọn cộng thêm 10.000đ
            if (chkTivi.Checked) tien += GiaTienNghi;
            if (chkInternet.Checked) tien += GiaTienNghi;
            if (chkNuocNong.Checked) tien += GiaTienNghi;

            // Dịch vụ
            if (chkKaraoke.Checked) tien += GiaKaraoke;
            if (chkAnSang.Checked) tien += (long)GiaAnSangMoiNgay * soNgay;

            txtThanhTien.Text = tien.ToString("N0") + " VNĐ";

            // Lưu lại số liệu thống kê trong ngày
            _tongTien += tien;
            _tongLuot++;

            btnThanhToan.Enabled = false;   // tránh bấm 2 lần cộng trùng
            btnNhapMoi.Enabled = true;
            btnTongKet.Enabled = true;
        }

        // Nhập mới: về trạng thái ban đầu, nút Nhập mới bị mờ
        private void btnNhapMoi_Click(object sender, EventArgs e)
        {
            XoaFormNhapKhach();
        }

        // Tổng kết: ghi số liệu vào ô tương ứng rồi đặt lại bộ đếm về 0
        private void btnTongKet_Click(object sender, EventArgs e)
        {
            txtSoLuot.Text = _tongLuot.ToString();
            txtTongTien.Text = _tongTien.ToString("N0") + " VNĐ";

            _tongLuot = 0;
            _tongTien = 0;
            btnTongKet.Enabled = false;
        }
    }
}
