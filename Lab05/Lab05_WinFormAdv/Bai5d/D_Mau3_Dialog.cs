using System;
using System.IO;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5d
{
    /// <summary>
    /// 5d - Bài mẫu 3: lưu nội dung nhập vào file *.txt (SaveFileDialog)
    /// và mở file *.txt hiển thị lên RichTextBox (OpenFileDialog).
    /// </summary>
    public class FrmFileDialog : Form
    {
        private TextBox textBox1;            // hiển thị đường dẫn file
        private RichTextBox richTextBox1;    // nội dung văn bản
        private Button btnLuu, btnMo;

        public FrmFileDialog()
        {
            Helper.CauHinhForm(this, "Save File Dialog", 440, 340);

            Label lblPath = Helper.Lbl("Đường dẫn", 15, 10, 80);
            textBox1 = Helper.Txt(100, 10, 325);
            textBox1.ReadOnly = true;

            Label lblNhap = new Label { Text = "Nhập đoạn text", Left = 15, Top = 48, Width = 200, Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold) };
            richTextBox1 = new RichTextBox { Left = 15, Top = 72, Width = 410, Height = 200 };

            btnMo = Helper.Btn("Mở file", 235, 290, 90);
            btnLuu = Helper.Btn("Lưu file", 335, 290, 90);

            btnLuu.Click += btnLuu_Click;
            btnMo.Click += btnMo_Click;

            this.Controls.AddRange(new Control[] { lblPath, textBox1, lblNhap, richTextBox1, btnMo, btnLuu });
        }

        // Lưu file: hộp thoại Save -> ghi nội dung richTextBox ra file *.txt
        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveFileDialog svg = new SaveFileDialog();
            svg.Filter = "Text files | *.txt";
            if (svg.ShowDialog() == DialogResult.OK)
            {
                string filename = svg.FileName;
                textBox1.Text = filename;
                try
                {
                    // using: tự động đóng luồng ghi (sw.Close()) kể cả khi có lỗi
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        sw.Write(richTextBox1.Text);
                    }
                    MessageBox.Show("Đã lưu file thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể lưu file: " + ex.Message, "Lỗi");
                }
            }
        }

        // Mở file: hộp thoại Open -> đọc nội dung file *.txt đưa lên richTextBox
        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Lọc loại đuôi file theo mong muốn
            openFileDialog1.Filter = "All Text File (*.txt)|*.txt";
            // Nếu dialog chọn OK
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Gán đường dẫn lên textbox
                textBox1.Text = openFileDialog1.FileName;
                try
                {
                    // Đọc dữ liệu text và gán vào richTextBox; using tự đóng luồng đọc
                    using (StreamReader f = new StreamReader(openFileDialog1.FileName))
                    {
                        richTextBox1.Text = f.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở file: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}
