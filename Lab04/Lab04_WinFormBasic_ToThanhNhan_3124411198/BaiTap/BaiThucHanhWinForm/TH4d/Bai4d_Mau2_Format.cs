using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// 4d - Bài tập mẫu, Bài 2: Form "frmFormat".
    /// Định dạng Font Style (CheckBox) và Color (RadioButton) cho nội dung của Label.
    /// </summary>
    public class Bai4d_Mau2_Format : Form
    {
        private Label label1;
        private CheckBox chkRegular, chkBold, chkItalic, chkBoldItalic;
        private RadioButton rdoAuto, rdo_red, rdoGreen, rdoBlue;
        private Button btnExit;

        // Cờ để tránh sự kiện CheckedChanged chạy lồng nhau khi ta tự đổi Checked bằng code
        private bool _dangCapNhat = false;

        public Bai4d_Mau2_Format()
        {
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "frmFormat";
            Font = new Font("Tahoma", 9F);
            ClientSize = new Size(380, 300);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            label1 = new Label
            {
                Text = "Trường Đại Học Công Nghiệp Thực Phẩm\nKhoa Công Nghệ Thông Tin",
                Font = new Font("Tahoma", 10F, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Location = new Point(15, 15),
                Size = new Size(350, 50)
            };

            // --- Nhóm Font Style (CheckBox) ---
            var grpFont = UiHelper.TaoGroupBox("Font Style", 15, 80, 170, 150);
            chkRegular = UiHelper.TaoCheckBox("Regular", 15, 25);
            chkBold = UiHelper.TaoCheckBox("Bold", 15, 55);
            chkItalic = UiHelper.TaoCheckBox("Italic", 15, 85);
            chkBoldItalic = UiHelper.TaoCheckBox("Bold and Italic", 15, 115);
            grpFont.Controls.AddRange(new Control[] { chkRegular, chkBold, chkItalic, chkBoldItalic });

            // --- Nhóm Color (RadioButton) ---
            // Đặt trong GroupBox riêng nên 4 radio này tự loại trừ nhau
            var grpColor = UiHelper.TaoGroupBox("Color", 195, 80, 170, 150);
            rdoAuto = UiHelper.TaoRadio("AutoColor", 15, 25);
            rdo_red = UiHelper.TaoRadio("Red", 15, 55);
            rdoGreen = UiHelper.TaoRadio("Green", 15, 85);
            rdoBlue = UiHelper.TaoRadio("Blue", 15, 115);
            grpColor.Controls.AddRange(new Control[] { rdoAuto, rdo_red, rdoGreen, rdoBlue });

            btnExit = UiHelper.TaoButton("Exit", 265, 250, 100, 32);

            // --- Sự kiện ---
            chkRegular.CheckedChanged += CheckBox_CheckedChanged;
            chkBold.CheckedChanged += CheckBox_CheckedChanged;
            chkItalic.CheckedChanged += CheckBox_CheckedChanged;
            chkBoldItalic.CheckedChanged += CheckBox_CheckedChanged;

            rdoAuto.CheckedChanged += Radio_CheckedChanged;
            rdo_red.CheckedChanged += Radio_CheckedChanged;
            rdoGreen.CheckedChanged += Radio_CheckedChanged;
            rdoBlue.CheckedChanged += Radio_CheckedChanged;

            btnExit.Click += (s, e) => Close();
            FormClosing += (s, e) => UiHelper.XacNhanDong(e);

            Controls.AddRange(new Control[] { label1, grpFont, grpColor, btnExit });

            // Trạng thái ban đầu giống hình: Italic + Red
            chkItalic.Checked = true;
            rdo_red.Checked = true;
        }

        // Sự kiện CheckedChanged của CheckBox: đổi Font Style của label
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_dangCapNhat) return;
            var cb = (CheckBox)sender;

            if (cb.Checked)
            {
                _dangCapNhat = true;
                if (cb == chkRegular)
                {
                    // Regular = không kiểu nào cả -> bỏ chọn các ô còn lại
                    chkBold.Checked = chkItalic.Checked = chkBoldItalic.Checked = false;
                }
                else
                {
                    chkRegular.Checked = false;
                    if (cb == chkBoldItalic) { chkBold.Checked = false; chkItalic.Checked = false; }
                    else chkBoldItalic.Checked = false;
                }
                _dangCapNhat = false;
            }
            ApDungFontStyle();
        }

        // Ghép các FontStyle bằng toán tử | rồi gán lại Font cho label
        private void ApDungFontStyle()
        {
            FontStyle kieu = FontStyle.Regular;
            if (chkBold.Checked) kieu |= FontStyle.Bold;
            if (chkItalic.Checked) kieu |= FontStyle.Italic;
            if (chkBoldItalic.Checked) kieu |= FontStyle.Bold | FontStyle.Italic;

            label1.Font = new Font(label1.Font, kieu);
        }

        // Sự kiện CheckedChanged của RadioButton: đổi màu chữ
        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = (RadioButton)sender;
            if (!rdo.Checked) return;   // chỉ xử lý radio vừa được chọn

            if (rdo == rdoAuto) label1.ForeColor = SystemColors.ControlText;
            else if (rdo == rdo_red) label1.ForeColor = Color.Red;
            else if (rdo == rdoGreen) label1.ForeColor = Color.Green;
            else label1.ForeColor = Color.Blue;
        }
    }
}
