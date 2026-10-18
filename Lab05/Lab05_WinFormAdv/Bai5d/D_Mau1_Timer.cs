using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài mẫu 1: đồng hồ dùng Timer. Mỗi giây sự kiện Tick phát sinh,
    /// lấy giờ hệ thống và hiển thị lên Label.
    /// </summary>
    public class FrmTimer : Form
    {
        private Label label1;
        private Timer timer1;

        public FrmTimer()
        {
            Helper.CauHinhForm(this, "Demo Timer", 360, 160);

            label1 = new Label
            {
                Left = 10, Top = 40, Width = 340, Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Red,
                Text = DateTime.Now.ToString()      // hiện giờ ngay từ đầu, không đợi 1 giây
            };

            timer1 = new Timer();
            timer1.Interval = 1000;   // khoảng chờ giữa 2 lần Tick (mili giây)
            timer1.Enabled = true;    // bật sự kiện Tick
            timer1.Tick += timer1_Tick;

            this.FormClosed += (s, e) => timer1.Dispose();
            this.Controls.Add(label1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Lấy thời gian hệ thống.
            // Lưu ý: phải dùng DateTime.Now (new DateTime() chỉ cho ngày 01/01/0001 00:00:00)
            DateTime now = DateTime.Now;
            label1.Text = now.ToString();
        }
    }
}
