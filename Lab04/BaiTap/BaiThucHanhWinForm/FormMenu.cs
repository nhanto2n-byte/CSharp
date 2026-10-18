using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BaiThucHanhWinForm.TH4c;
using BaiThucHanhWinForm.TH4d;

namespace BaiThucHanhWinForm
{
    /// <summary>
    /// Menu chọn bài (giống menu 1..N ở các lab trước).
    /// Bấm vào bài nào thì form của bài đó mới được mở; đóng form bài thì quay lại menu.
    /// </summary>
    public class FormMenu : Form
    {
        // Mỗi mục menu gồm: tên hiển thị + hàm tạo form tương ứng
        private class MucMenu
        {
            public string Ten;
            public Func<Form> TaoForm;
            public MucMenu(string ten, Func<Form> taoForm) { Ten = ten; TaoForm = taoForm; }
        }

        public FormMenu()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Lab 04 - WinForm cơ bản";
            Font = new Font("Tahoma", 10F);
            ClientSize = new Size(760, 440);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "LAB 04 - LẬP TRÌNH WINDOW FORM CƠ BẢN",
                Font = new Font("Tahoma", 14F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 10),
                Size = new Size(760, 36)
            };
            Controls.Add(lblTieuDe);

            // ===== Danh sách bài 4c =====
            var ds4c = new List<MucMenu>
            {
                new MucMenu("Bài mẫu: My Name Project", () => new frmBaiTap1()),
                new MucMenu("Tại lớp 1: Cộng trừ nhân chia", () => new Bai4c_TL1_PhepTinh()),
                new MucMenu("Tại lớp 2: Đăng ký tài khoản", () => new Bai4c_TL2_DangKy()),
                new MucMenu("Tại lớp 3: UCLN - BCNN", () => new Bai4c_TL3_UCLN_BCNN()),
                new MucMenu("Tại lớp 4: Dãy số và tính tổng", () => new Bai4c_TL4_DaySo()),
                new MucMenu("Tại lớp 5: Đọc số thành chữ", () => new Bai4c_TL5_DocSo()),
                new MucMenu("Nâng cao 1: Bán vé rạp chiếu phim", () => new Bai4c_NC1_RapPhim()),
                new MucMenu("Về nhà 1: Máy tính bỏ túi", () => new Bai4c_VN1_MayTinh()),
            };

            // ===== Danh sách bài 4d =====
            var ds4d = new List<MucMenu>
            {
                new MucMenu("Mẫu 1: Cộng trừ nhân chia (Radio)", () => new Bai4d_Mau1_Radio()),
                new MucMenu("Mẫu 2: Định dạng Font Style / Color", () => new Bai4d_Mau2_Format()),
                new MucMenu("Tại lớp 1: Giải phương trình bậc 1-2", () => new Bai4d_TL1_PhuongTrinh()),
                new MucMenu("Tại lớp 2: Mảng số nguyên", () => new Bai4d_TL2_Mang()),
                new MucMenu("Nâng cao 1: Cafe Sinh Viên", () => new Bai4d_NC1_CafeSinhVien()),
                new MucMenu("Về nhà 1: Khách sạn Thanh Thanh", () => new Bai4d_VN1_KhachSan()),
            };

            ThemNhomNut("Thực hành 4c - WinForm Basic 1", ds4c, 25, 60);
            ThemNhomNut("Thực hành 4d - WinForm Basic 2", ds4d, 395, 60);

            var btnThoat = UiHelper.TaoButton("Thoát", 330, 385, 100, 36);
            btnThoat.Click += (s, e) => Close();
            Controls.Add(btnThoat);
        }

        // Vẽ một nhóm nút (mỗi mục menu là một nút) trong GroupBox
        private void ThemNhomNut(string tieuDe, List<MucMenu> ds, int x, int y)
        {
            var grp = UiHelper.TaoGroupBox(tieuDe, x, y, 340, 310);
            for (int i = 0; i < ds.Count; i++)
            {
                MucMenu muc = ds[i];
                var btn = new Button
                {
                    Text = (i + 1) + ". " + muc.Ten,
                    Location = new Point(15, 28 + i * 34),
                    Size = new Size(310, 30),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                // Bấm nút -> mở form của bài đó (ShowDialog: phải đóng form bài mới quay lại menu)
                btn.Click += (s, e) =>
                {
                    using (Form f = muc.TaoForm())
                    {
                        f.ShowDialog(this);
                    }
                };
                grp.Controls.Add(btn);
            }
            Controls.Add(grp);
        }
    }
}
