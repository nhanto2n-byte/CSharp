using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4c
{
    /// <summary>
    /// 4c - Bài tập nâng cao, Bài 1: Quản lý bán vé rạp chiếu phim.
    /// 3 hàng ghế x 5 ghế = 15 ghế, đánh số 1..15, chia thành 3 lô (mỗi hàng là một lô):
    ///   Lô A (ghế 1-5): 1000/vé, Lô B (ghế 6-10): 1500/vé, Lô C (ghế 11-15): 2000/vé.
    /// Màu ghế: trắng = chưa bán, xanh = đang chọn, vàng = đã bán.
    /// </summary>
    public class Bai4c_NC1_RapPhim : Form
    {
        // Ba trạng thái của một ghế
        private enum TrangThai { ChuaBan, DangChon, DaBan }

        private const int SoHang = 3;
        private const int SoGheMoiHang = 5;
        private readonly int[] _giaLo = { 1000, 1500, 2000 };   // giá vé theo lô A, B, C
        private readonly string[] _tenLo = { "Lô A", "Lô B", "Lô C" };

        private readonly Button[] _ghe = new Button[SoHang * SoGheMoiHang];
        private readonly TrangThai[] _trangThai = new TrangThai[SoHang * SoGheMoiHang];

        private Label lblThanhTien;
        private Button btnChon, btnHuyBo;

        private readonly Color MauChuaBan = Color.White;
        private readonly Color MauDangChon = Color.LimeGreen;
        private readonly Color MauDaBan = Color.Gold;

        public Bai4c_NC1_RapPhim()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Bán vé rạp chiếu phim";
            Font = new Font("Tahoma", 10F);
            ClientSize = new Size(470, 380);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTieuDe = new Label
            {
                Text = "SƠ ĐỒ CHỖ NGỒI",
                Font = new Font("Tahoma", 13F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 10),
                Size = new Size(470, 30)
            };
            Controls.Add(lblTieuDe);

            // --- Dựng 15 nút ghế ---
            for (int hang = 0; hang < SoHang; hang++)
            {
                // Nhãn tên lô + giá vé ở đầu mỗi hàng
                Controls.Add(UiHelper.TaoLabel($"{_tenLo[hang]}\n{_giaLo[hang]:N0}đ", 15, 62 + hang * 60, 80));

                for (int cot = 0; cot < SoGheMoiHang; cot++)
                {
                    int viTri = hang * SoGheMoiHang + cot;   // chỉ số 0..14
                    var btn = new Button
                    {
                        Text = (viTri + 1).ToString(),        // số ghế 1..15
                        Size = new Size(55, 45),
                        Location = new Point(105 + cot * 68, 55 + hang * 60),
                        BackColor = MauChuaBan,
                        FlatStyle = FlatStyle.Flat,
                        Tag = viTri                           // lưu chỉ số ghế trong Tag
                    };
                    btn.Click += Ghe_Click;
                    _ghe[viTri] = btn;
                    _trangThai[viTri] = TrangThai.ChuaBan;
                    Controls.Add(btn);
                }
            }

            // --- Chú thích màu ---
            Controls.Add(TaoChuThich("Chưa bán", MauChuaBan, 105, 245));
            Controls.Add(TaoChuThich("Đang chọn", MauDangChon, 215, 245));
            Controls.Add(TaoChuThich("Đã bán", MauDaBan, 335, 245));

            // --- Các nút và thành tiền ---
            btnChon = UiHelper.TaoButton("CHỌN", 105, 285, 110, 36);
            btnHuyBo = UiHelper.TaoButton("HỦY BỎ", 235, 285, 110, 36);
            btnChon.Click += btnChon_Click;
            btnHuyBo.Click += btnHuyBo_Click;

            Controls.Add(UiHelper.TaoLabel("Thành Tiền:", 105, 340));
            lblThanhTien = new Label
            {
                Text = "0",
                Font = new Font("Tahoma", 11F, FontStyle.Bold),
                ForeColor = Color.Red,
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(200, 335),
                Size = new Size(145, 28)
            };

            FormClosing += (s, e) => UiHelper.XacNhanDong(e);
            Controls.AddRange(new Control[] { btnChon, btnHuyBo, lblThanhTien });
        }

        // Tạo một ô màu nhỏ kèm chữ làm chú thích
        private Control TaoChuThich(string chu, Color mau, int x, int y)
        {
            var panel = new Panel { Location = new Point(x, y), Size = new Size(105, 24) };
            panel.Controls.Add(new Label { BackColor = mau, BorderStyle = BorderStyle.FixedSingle, Location = new Point(0, 3), Size = new Size(18, 18) });
            panel.Controls.Add(UiHelper.TaoLabel(chu, 24, 2));
            return panel;
        }

        // Click vào một ghế
        private void Ghe_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int viTri = (int)btn.Tag;

            switch (_trangThai[viTri])
            {
                case TrangThai.ChuaBan:      // chưa bán -> đang chọn (xanh)
                    _trangThai[viTri] = TrangThai.DangChon;
                    btn.BackColor = MauDangChon;
                    break;
                case TrangThai.DangChon:     // đang chọn -> bỏ chọn, về trắng
                    _trangThai[viTri] = TrangThai.ChuaBan;
                    btn.BackColor = MauChuaBan;
                    break;
                case TrangThai.DaBan:        // đã bán -> thông báo
                    MessageBox.Show($"Ghế số {viTri + 1} đã bán rồi, vui lòng chọn ghế khác!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        // CHỌN: các ghế xanh -> vàng (đã bán), xuất tổng tiền
        private void btnChon_Click(object sender, EventArgs e)
        {
            int tongTien = 0;
            int soVe = 0;
            for (int i = 0; i < _ghe.Length; i++)
            {
                if (_trangThai[i] == TrangThai.DangChon)
                {
                    int lo = i / SoGheMoiHang;           // ghế thuộc hàng/lô nào
                    tongTien += _giaLo[lo];
                    soVe++;
                    _trangThai[i] = TrangThai.DaBan;
                    _ghe[i].BackColor = MauDaBan;
                }
            }

            if (soVe == 0)
            {
                MessageBox.Show("Bạn chưa chọn ghế nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lblThanhTien.Text = tongTien.ToString("N0") + " đ";
        }

        // HỦY BỎ: các ghế xanh -> trắng, thành tiền = 0
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _ghe.Length; i++)
            {
                if (_trangThai[i] == TrangThai.DangChon)
                {
                    _trangThai[i] = TrangThai.ChuaBan;
                    _ghe[i].BackColor = MauChuaBan;
                }
            }
            lblThanhTien.Text = "0";
        }
    }
}
