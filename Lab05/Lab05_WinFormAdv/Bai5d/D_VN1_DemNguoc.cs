using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài về nhà 1: đồng hồ đếm ngược thời gian bằng Timer.
    /// Chọn phút + giây -> Bắt đầu (đổi thành Tạm dừng / Tiếp tục) -> hết giờ thì báo.
    /// </summary>
    public class FrmDemNguoc : Form
    {
        private Label lblThoiGian;
        private NumericUpDown nudPhut, nudGiay;
        private Button btnBatDau, btnDatLai;
        private Timer timer1;
        private int conLai = 0;   // số giây còn lại

        public FrmDemNguoc()
        {
            Helper.CauHinhForm(this, "Đếm ngược thời gian", 360, 230);

            lblThoiGian = new Label
            {
                Left = 10, Top = 15, Width = 340, Height = 70, Text = "00:00",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 36, FontStyle.Bold), ForeColor = Color.Crimson
            };

            nudPhut = new NumericUpDown { Left = 70, Top = 100, Width = 70, Minimum = 0, Maximum = 99 };
            nudGiay = new NumericUpDown { Left = 230, Top = 100, Width = 70, Minimum = 0, Maximum = 59 };

            btnBatDau = Helper.Btn("Bắt đầu", 60, 150, 120);
            btnDatLai = Helper.Btn("Đặt lại", 190, 150, 110);

            timer1 = new Timer { Interval = 1000 };   // 1 giây / lần Tick
            timer1.Tick += timer1_Tick;

            btnBatDau.Click += btnBatDau_Click;
            btnDatLai.Click += btnDatLai_Click;
            nudPhut.ValueChanged += nud_ValueChanged;
            nudGiay.ValueChanged += nud_ValueChanged;
            this.FormClosed += (s, e) => timer1.Dispose();

            this.Controls.AddRange(new Control[] {
                lblThoiGian,
                Helper.Lbl("Phút", 20, 100, 45), nudPhut,
                Helper.Lbl("Giây", 180, 100, 45), nudGiay,
                btnBatDau, btnDatLai });
        }

        // Hiển thị số giây dưới dạng mm:ss
        private void HienThi(int giay)
        {
            lblThoiGian.Text = (giay / 60).ToString("00") + ":" + (giay % 60).ToString("00");
        }

        // Đổi giá trị phút/giây khi chưa chạy -> cập nhật đồng hồ xem trước
        private void nud_ValueChanged(object sender, EventArgs e)
        {
            if (!timer1.Enabled && conLai <= 0)
                HienThi((int)nudPhut.Value * 60 + (int)nudGiay.Value);
        }

        // Bắt đầu / Tạm dừng / Tiếp tục
        private void btnBatDau_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                if (conLai <= 0)   // bắt đầu mới
                    conLai = (int)nudPhut.Value * 60 + (int)nudGiay.Value;
                if (conLai <= 0)
                {
                    MessageBox.Show("Hãy chọn thời gian lớn hơn 0!", "Thông báo");
                    return;
                }
                nudPhut.Enabled = false;
                nudGiay.Enabled = false;
                timer1.Start();
                btnBatDau.Text = "Tạm dừng";
            }
            else
            {
                timer1.Stop();
                btnBatDau.Text = "Tiếp tục";
            }
        }

        // Mỗi giây giảm 1; về 0 thì dừng và báo hết giờ
        private void timer1_Tick(object sender, EventArgs e)
        {
            conLai--;
            HienThi(conLai);
            if (conLai <= 0)
            {
                timer1.Stop();
                conLai = 0;
                btnBatDau.Text = "Bắt đầu";
                nudPhut.Enabled = true;
                nudGiay.Enabled = true;
                MessageBox.Show("Hết giờ!", "Thông báo");
            }
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            conLai = 0;
            btnBatDau.Text = "Bắt đầu";
            nudPhut.Enabled = true;
            nudGiay.Enabled = true;
            HienThi((int)nudPhut.Value * 60 + (int)nudGiay.Value);
        }
    }
}
