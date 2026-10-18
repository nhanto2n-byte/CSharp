using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập tại lớp, Bài 2: Các thao tác trên mảng một chiều số nguyên.
    /// Dùng class MangSoNguyen. Nút "Thực Hiện" chạy chức năng đang được chọn
    /// (khi chọn radio ở nhóm này thì radio ở các nhóm khác tự bỏ chọn).
    /// Nút "Tổng" và "Tìm" (Max-Min) có chức năng riêng.
    /// </summary>
    public class Bai4d_TL2_Mang : Form
    {
        private readonly MangSoNguyen _mang = new MangSoNguyen();

        private TextBox txtNhapMang, txtKetQuaMang;
        private Button btnNhapMang, btnReset, btnThoat, btnThucHien, btnTong, btnTimMaxMin;

        private RadioButton rdoTang, rdoGiam;
        private RadioButton rdoTimGiaTri, rdoTimViTri;
        private TextBox txtTimGiaTri, txtTimViTri, txtSoTim;
        private RadioButton rdoXoaGiaTri, rdoXoaViTri;
        private TextBox txtXoaGiaTri, txtXoaViTri;
        private RadioButton rdoThem;
        private TextBox txtThemGiaTri, txtThemViTri;
        private TextBox txtTongMang, txtTongChan, txtTongLe;
        private TextBox txtMax, txtMin;
        private RadioButton rdoThayGiaTri, rdoThayViTri;
        private TextBox txtThayGiaTri, txtThayViTri, txtSoThayThe;

        private List<RadioButton> _tatCaRadio;
        private bool _dangDoiRadio = false;

        public Bai4d_TL2_Mang()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Mảng Số Nguyên";
            Font = new Font("Times New Roman", 10F);
            ClientSize = new Size(500, 585);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "Mảng Số Nguyên",
                Font = new Font("Times New Roman", 20F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 8),
                Size = new Size(500, 40)
            };
            Controls.Add(lblTieuDe);

            // ===== Hàng nhập mảng / kết quả mảng =====
            btnNhapMang = UiHelper.TaoButton("Nhập mảng :", 15, 55, 95, 28);
            txtNhapMang = UiHelper.TaoTextBox(118, 58, 270);
            btnReset = UiHelper.TaoButton("Reset", 400, 55, 85, 28);
            Controls.Add(UiHelper.TaoLabel("Kết quả mảng :", 15, 95));
            txtKetQuaMang = UiHelper.TaoTextBox(118, 92, 270, true);
            btnThoat = UiHelper.TaoButton("Thoát", 400, 90, 85, 28);

            // ===== Thực hiện + Sắp xếp =====
            btnThucHien = UiHelper.TaoButton("Thực Hiện", 15, 125, 95, 45);
            var grpSapXep = UiHelper.TaoGroupBox("Sắp Xếp", 118, 122, 367, 50);
            rdoTang = UiHelper.TaoRadio("Sắp xếp Tăng", 40, 20);
            rdoGiam = UiHelper.TaoRadio("Sắp xếp Giảm", 200, 20);
            grpSapXep.Controls.AddRange(new Control[] { rdoTang, rdoGiam });

            // ===== Tìm kiếm =====
            var grpTim = UiHelper.TaoGroupBox("Tìm Kiếm", 15, 180, 230, 125);
            rdoTimGiaTri = UiHelper.TaoRadio("Tìm giá trị cần tìm", 8, 22);
            txtTimGiaTri = UiHelper.TaoTextBox(170, 20, 50);
            rdoTimViTri = UiHelper.TaoRadio("Tìm vị trí cần tìm", 8, 50);
            txtTimViTri = UiHelper.TaoTextBox(170, 48, 50);
            txtSoTim = UiHelper.TaoTextBox(130, 82, 90, true);
            grpTim.Controls.AddRange(new Control[] { rdoTimGiaTri, txtTimGiaTri, rdoTimViTri, txtTimViTri,
                UiHelper.TaoLabel("Số tìm được là :", 15, 85), txtSoTim });

            // ===== Xóa =====
            var grpXoa = UiHelper.TaoGroupBox("Xóa", 255, 180, 230, 125);
            rdoXoaGiaTri = UiHelper.TaoRadio("Tìm giá trị cần xóa", 8, 22);
            txtXoaGiaTri = UiHelper.TaoTextBox(170, 20, 50);
            rdoXoaViTri = UiHelper.TaoRadio("Tìm vị trí cần xóa", 8, 50);
            txtXoaViTri = UiHelper.TaoTextBox(170, 48, 50);
            var lblCanXoa = UiHelper.TaoLabel("Cần sắp xếp tăng", 60, 90);
            lblCanXoa.ForeColor = Color.Red;
            grpXoa.Controls.AddRange(new Control[] { rdoXoaGiaTri, txtXoaGiaTri, rdoXoaViTri, txtXoaViTri, lblCanXoa });

            // ===== Thêm =====
            var grpThem = UiHelper.TaoGroupBox("Thêm", 15, 313, 230, 125);
            rdoThem = UiHelper.TaoRadio("Tìm giá trị cần thêm", 8, 22);
            txtThemGiaTri = UiHelper.TaoTextBox(170, 20, 50);
            txtThemViTri = UiHelper.TaoTextBox(170, 55, 50);
            var lblCanThem = UiHelper.TaoLabel("Cần sắp xếp tăng", 60, 92);
            lblCanThem.ForeColor = Color.Red;
            grpThem.Controls.AddRange(new Control[] { rdoThem, txtThemGiaTri,
                UiHelper.TaoLabel("Tại vị trí cần thêm :", 30, 58), txtThemViTri, lblCanThem });

            // ===== Tổng =====
            var grpTong = UiHelper.TaoGroupBox("Tổng", 255, 313, 230, 125);
            txtTongMang = UiHelper.TaoTextBox(85, 20, 60, true);
            txtTongChan = UiHelper.TaoTextBox(85, 55, 60, true);
            txtTongLe = UiHelper.TaoTextBox(85, 90, 60, true);
            btnTong = UiHelper.TaoButton("Tổng", 155, 20, 65, 95);
            grpTong.Controls.AddRange(new Control[] {
                UiHelper.TaoLabel("Tổng mảng", 10, 23), txtTongMang,
                UiHelper.TaoLabel("Tổng chẵn", 10, 58), txtTongChan,
                UiHelper.TaoLabel("Tổng lẻ", 10, 93), txtTongLe, btnTong });

            // ===== Max - Min =====
            var grpMaxMin = UiHelper.TaoGroupBox("Max - Min", 15, 446, 230, 125);
            txtMax = UiHelper.TaoTextBox(115, 25, 50, true);
            txtMin = UiHelper.TaoTextBox(115, 70, 50, true);
            btnTimMaxMin = UiHelper.TaoButton("Tìm", 172, 25, 50, 70);
            grpMaxMin.Controls.AddRange(new Control[] {
                UiHelper.TaoLabel("Giá trị lớn nhất", 10, 28), txtMax,
                UiHelper.TaoLabel("Giá trị nhỏ nhất", 10, 73), txtMin, btnTimMaxMin });

            // ===== Thay thế =====
            var grpThay = UiHelper.TaoGroupBox("Thay Thế", 255, 446, 230, 125);
            rdoThayGiaTri = UiHelper.TaoRadio("Giá trị cần thay thế", 8, 22);
            txtThayGiaTri = UiHelper.TaoTextBox(170, 20, 50);
            rdoThayViTri = UiHelper.TaoRadio("Vị trí cần thay thế", 8, 50);
            txtThayViTri = UiHelper.TaoTextBox(170, 48, 50);
            txtSoThayThe = UiHelper.TaoTextBox(170, 85, 50);
            grpThay.Controls.AddRange(new Control[] { rdoThayGiaTri, txtThayGiaTri, rdoThayViTri, txtThayViTri,
                UiHelper.TaoLabel("Số thay thế là :", 40, 88), txtSoThayThe });

            Controls.AddRange(new Control[] { btnNhapMang, txtNhapMang, btnReset, txtKetQuaMang, btnThoat,
                btnThucHien, grpSapXep, grpTim, grpXoa, grpThem, grpTong, grpMaxMin, grpThay });

            // ===== Gắn sự kiện =====
            _tatCaRadio = new List<RadioButton> { rdoTang, rdoGiam, rdoTimGiaTri, rdoTimViTri,
                rdoXoaGiaTri, rdoXoaViTri, rdoThem, rdoThayGiaTri, rdoThayViTri };
            foreach (var r in _tatCaRadio) r.CheckedChanged += Radio_CheckedChanged;

            // Mọi ô nhập số: chỉ nhận chữ số và dấu '-'
            var oSo = new[] { txtTimGiaTri, txtTimViTri, txtXoaGiaTri, txtXoaViTri, txtThemGiaTri,
                txtThemViTri, txtThayGiaTri, txtThayViTri, txtSoThayThe };
            foreach (var tb in oSo)
                tb.KeyPress += (s, e) => UiHelper.ChanNhapKhongPhaiSo(s, e, true, false);

            // Ô nhập mảng: chữ số, dấu cách, dấu '-', dấu ',' ';'
            txtNhapMang.KeyPress += txtNhapMang_KeyPress;

            btnNhapMang.Click += btnNhapMang_Click;
            btnReset.Click += btnReset_Click;
            btnThoat.Click += (s, e) => Close();
            btnThucHien.Click += btnThucHien_Click;
            btnTong.Click += btnTong_Click;
            btnTimMaxMin.Click += btnTimMaxMin_Click;
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            rdoTang.Checked = true;   // mặc định chọn sắp xếp tăng
        }

        // Chọn một radio thì bỏ chọn radio ở các nhóm khác -> chỉ có 1 chức năng được chọn
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (_dangDoiRadio) return;
            var rdo = (RadioButton)sender;
            if (!rdo.Checked) return;

            _dangDoiRadio = true;
            foreach (var r in _tatCaRadio)
                if (r != rdo && r.Parent != rdo.Parent) r.Checked = false;
            _dangDoiRadio = false;
        }

        private void txtNhapMang_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (char.IsControl(c) || char.IsDigit(c) || c == ' ' || c == '-' || c == ',' || c == ';') return;
            e.Handled = true;
        }

        private void HienThiMang()
        {
            txtKetQuaMang.Text = _mang.ToString();
        }

        private void BaoLoi(string noiDung, Control focus = null)
        {
            MessageBox.Show(noiDung, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (focus != null) focus.Focus();
        }

        // Đọc 1 số nguyên từ TextBox, nếu sai thì báo lỗi
        private bool DocSo(TextBox tb, string tenO, out int so)
        {
            if (!UiHelper.LaSoNguyen(tb.Text, out so))
            {
                BaoLoi("Vui lòng nhập số nguyên hợp lệ cho ô \"" + tenO + "\"!", tb);
                return false;
            }
            return true;
        }

        // Kiểm tra mảng đã có dữ liệu chưa
        private bool CoMang()
        {
            if (_mang.Rong)
            {
                BaoLoi("Mảng đang rỗng, hãy nhập mảng trước!", txtNhapMang);
                return false;
            }
            return true;
        }

        // ---------- Nhập mảng ----------
        private void btnNhapMang_Click(object sender, EventArgs e)
        {
            string[] phanTu = txtNhapMang.Text.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (phanTu.Length == 0)
            {
                BaoLoi("Vui lòng nhập các số nguyên cách nhau bằng dấu cách!", txtNhapMang);
                return;
            }

            var ds = new List<int>();
            foreach (string s in phanTu)
            {
                if (!UiHelper.LaSoNguyen(s, out int so))
                {
                    BaoLoi("\"" + s + "\" không phải là số nguyên hợp lệ!", txtNhapMang);
                    return;
                }
                ds.Add(so);
            }
            _mang.Nhap(ds);
            HienThiMang();
        }

        // ---------- Reset ----------
        private void btnReset_Click(object sender, EventArgs e)
        {
            _mang.Reset();
            foreach (Control c in Controls) XoaTextBox(c);
            rdoTang.Checked = true;
            txtNhapMang.Focus();
        }

        // Xóa nội dung mọi TextBox (kể cả TextBox nằm trong GroupBox)
        private void XoaTextBox(Control cha)
        {
            if (cha is TextBox tb) tb.Clear();
            foreach (Control con in cha.Controls) XoaTextBox(con);
        }

        // ---------- Thực hiện: chạy chức năng đang chọn ----------
        private void btnThucHien_Click(object sender, EventArgs e)
        {
            if (!CoMang()) return;

            if (rdoTang.Checked) { _mang.SapXepTang(); HienThiMang(); }
            else if (rdoGiam.Checked) { _mang.SapXepGiam(); HienThiMang(); }
            else if (rdoTimGiaTri.Checked) TimTheoGiaTri();
            else if (rdoTimViTri.Checked) TimTheoViTri();
            else if (rdoXoaGiaTri.Checked) XoaTheoGiaTri();
            else if (rdoXoaViTri.Checked) XoaTheoViTri();
            else if (rdoThem.Checked) Them();
            else if (rdoThayGiaTri.Checked) ThayTheoGiaTri();
            else if (rdoThayViTri.Checked) ThayTheoViTri();
            else BaoLoi("Vui lòng chọn một chức năng!");
        }

        private void TimTheoGiaTri()
        {
            if (!DocSo(txtTimGiaTri, "giá trị cần tìm", out int gt)) return;
            int viTri = _mang.TimGiaTri(gt);
            txtSoTim.Text = viTri >= 0 ? "Vị trí " + viTri : "Không tìm thấy";
        }

        private void TimTheoViTri()
        {
            if (!DocSo(txtTimViTri, "vị trí cần tìm", out int vt)) return;
            if (_mang.TimViTri(vt, out int gt)) txtSoTim.Text = gt.ToString();
            else BaoLoi("Vị trí phải từ 0 đến " + (_mang.SoPhanTu - 1) + "!", txtTimViTri);
        }

        private void XoaTheoGiaTri()
        {
            if (!_mang.DaSapXepTang()) { BaoLoi("Cần sắp xếp mảng tăng dần trước khi xóa!"); return; }
            if (!DocSo(txtXoaGiaTri, "giá trị cần xóa", out int gt)) return;
            int soLuong = _mang.XoaGiaTri(gt);
            if (soLuong == 0) BaoLoi("Không có giá trị " + gt + " trong mảng!", txtXoaGiaTri);
            else HienThiMang();
        }

        private void XoaTheoViTri()
        {
            if (!_mang.DaSapXepTang()) { BaoLoi("Cần sắp xếp mảng tăng dần trước khi xóa!"); return; }
            if (!DocSo(txtXoaViTri, "vị trí cần xóa", out int vt)) return;
            if (_mang.XoaViTri(vt)) HienThiMang();
            else BaoLoi("Vị trí phải từ 0 đến " + (_mang.SoPhanTu - 1) + "!", txtXoaViTri);
        }

        // Thêm: nếu bỏ trống ô vị trí thì tự chèn đúng chỗ để mảng vẫn tăng dần
        private void Them()
        {
            if (!_mang.DaSapXepTang()) { BaoLoi("Cần sắp xếp mảng tăng dần trước khi thêm!"); return; }
            if (!DocSo(txtThemGiaTri, "giá trị cần thêm", out int gt)) return;

            if (txtThemViTri.Text.Trim().Length == 0)
            {
                _mang.ThemGiuThuTuTang(gt);
                HienThiMang();
                return;
            }
            if (!DocSo(txtThemViTri, "vị trí cần thêm", out int vt)) return;
            if (_mang.ThemTaiViTri(gt, vt)) HienThiMang();
            else BaoLoi("Vị trí thêm phải từ 0 đến " + _mang.SoPhanTu + "!", txtThemViTri);
        }

        private void ThayTheoGiaTri()
        {
            if (!DocSo(txtThayGiaTri, "giá trị cần thay thế", out int cu)) return;
            if (!DocSo(txtSoThayThe, "số thay thế", out int moi)) return;
            int dem = _mang.ThayTheGiaTri(cu, moi);
            if (dem == 0) BaoLoi("Không có giá trị " + cu + " trong mảng!", txtThayGiaTri);
            else HienThiMang();
        }

        private void ThayTheoViTri()
        {
            if (!DocSo(txtThayViTri, "vị trí cần thay thế", out int vt)) return;
            if (!DocSo(txtSoThayThe, "số thay thế", out int moi)) return;
            if (_mang.ThayTheViTri(vt, moi)) HienThiMang();
            else BaoLoi("Vị trí phải từ 0 đến " + (_mang.SoPhanTu - 1) + "!", txtThayViTri);
        }

        // ---------- Tổng ----------
        private void btnTong_Click(object sender, EventArgs e)
        {
            if (!CoMang()) return;
            txtTongMang.Text = _mang.TongMang().ToString();
            txtTongChan.Text = _mang.TongChan().ToString();
            txtTongLe.Text = _mang.TongLe().ToString();
        }

        // ---------- Max - Min ----------
        private void btnTimMaxMin_Click(object sender, EventArgs e)
        {
            if (!CoMang()) return;
            txtMax.Text = _mang.GiaTriLonNhat().ToString();
            txtMin.Text = _mang.GiaTriNhoNhat().ToString();
        }
    }
}
