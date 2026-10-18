using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab06_CSDL_Basic
{
    /// <summary>Thực hành 6b: quản lý SinhVien và kiểm tra khóa ngoại MaLop.</summary>
    public class SinhVienForm : Form
    {
        private readonly TextBox txtMa = new TextBox();
        private readonly TextBox txtHoTen = new TextBox();
        private readonly DateTimePicker dtNgaySinh = new DateTimePicker();
        private readonly ComboBox cboLop = new ComboBox();

        public SinhVienForm()
        {
            Text = "6b - Quản lý sinh viên"; StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(480, 270); Font = new Font("Segoe UI", 10);
            AddLabel("Mã sinh viên", 20); txtMa.SetBounds(155, 17, 280, 27); Controls.Add(txtMa);
            AddLabel("Họ tên", 58); txtHoTen.SetBounds(155, 55, 280, 27); Controls.Add(txtHoTen);
            AddLabel("Ngày sinh", 96); dtNgaySinh.SetBounds(155, 93, 280, 27); dtNgaySinh.Format = DateTimePickerFormat.Short; dtNgaySinh.ShowCheckBox = true; dtNgaySinh.Checked = true; Controls.Add(dtNgaySinh);
            AddLabel("Mã lớp", 134); cboLop.SetBounds(155, 131, 280, 27); cboLop.DropDownStyle = ComboBoxStyle.DropDownList; Controls.Add(cboLop);
            AddButton("Thêm", 35, Them); AddButton("Sửa", 135, Sua); AddButton("Xóa", 235, Xoa);
            Button reload = new Button { Text="Tải lớp", Left=335, Top=180, Width=100, Height=32 };
            reload.Click += (s,e) => LoadLop(); Controls.Add(reload);
            Load += (s,e) => { try { LoadLop(); } catch(Exception ex) { MessageBox.Show("Không tải được lớp: "+ex.Message); } };
        }
        private void AddLabel(string text, int top) => Controls.Add(new Label { Text=text, Left=25, Top=top+4, Width=120 });
        private void AddButton(string text, int left, Action action) {
            Button b = new Button { Text=text, Left=left, Top=180, Width=85, Height=32 };
            b.Click += (s,e) => { try { action(); } catch(Exception ex) { MessageBox.Show("Thao tác thất bại: "+ex.Message); } };
            Controls.Add(b);
        }
        private void LoadLop()
        {
            // ComboBox hiển thị MaLop lấy từ bảng Lop bằng truy vấn SELECT.
            DataTable dt = Db.Query("SELECT MaLop FROM Lop ORDER BY MaLop");
            cboLop.Items.Clear();
            foreach (DataRow row in dt.Rows) cboLop.Items.Add(row["MaLop"].ToString());
            if (cboLop.Items.Count > 0 && cboLop.SelectedIndex < 0) cboLop.SelectedIndex = 0;
        }
        private bool Valid(bool needName, bool needDate, bool needClass)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text)) { MessageBox.Show("Vui lòng nhập mã sinh viên."); txtMa.Focus(); return false; }
            if (needName && string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Vui lòng nhập họ tên."); txtHoTen.Focus(); return false; }
            if (needDate && dtNgaySinh.Value.Date == DateTime.MinValue) { MessageBox.Show("Vui lòng nhập ngày sinh."); return false; }
            if (needClass && cboLop.SelectedItem == null) { MessageBox.Show("Vui lòng chọn lớp."); return false; }
            return true;
        }
        private void Them()
        {
            if (!Valid(true,true,true)) return;
            // Đề 6b yêu cầu tuổi >= 17, chỉ tính theo năm sinh.
            if (DateTime.Now.Year - dtNgaySinh.Value.Year < 17) { MessageBox.Show("Sinh viên phải từ 17 tuổi trở lên (tính theo năm sinh)."); return; }
            int count = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM SinhVien WHERE MaSinhVien=@ma",
                Db.P("@ma", SqlDbType.VarChar, txtMa.Text.Trim(), 20)));
            if (count > 0) { MessageBox.Show("Mã sinh viên đã tồn tại."); return; }
            // Khóa ngoại MaLop được chọn từ dữ liệu thực trong bảng Lop.
            int rows = Db.Execute("INSERT INTO SinhVien(MaSinhVien,HoTen,NgaySinh,MaLop) VALUES(@ma,@ten,@ngay,@lop)",
                Db.P("@ma",SqlDbType.VarChar,txtMa.Text.Trim(),20),
                Db.P("@ten",SqlDbType.NVarChar,txtHoTen.Text.Trim(),100),
                Db.P("@ngay",SqlDbType.Date,dtNgaySinh.Value.Date),
                Db.P("@lop",SqlDbType.VarChar,cboLop.SelectedItem.ToString(),20));
            MessageBox.Show(rows > 0 ? "Thêm sinh viên thành công." : "Không thêm được dữ liệu.");
        }
        private void Sua()
        {
            if (!Valid(false,false,false)) return;
            // Theo yêu cầu 6a nâng cao/6b, trường nhập mới được cập nhật; trường bỏ trống giữ nguyên.
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) && cboLop.SelectedItem == null && !dtNgaySinh.Checked) {
                MessageBox.Show("Nhập ít nhất một trường cần cập nhật (họ tên/lớp/ngày sinh)."); return;
            }
            int rows = Db.Execute(@"UPDATE SinhVien SET
                HoTen = CASE WHEN @ten = N'' THEN HoTen ELSE @ten END,
                NgaySinh = CASE WHEN @coNgay = 0 THEN NgaySinh ELSE @ngay END,
                MaLop = CASE WHEN @lop = '' THEN MaLop ELSE @lop END
                WHERE MaSinhVien=@ma",
                Db.P("@ten",SqlDbType.NVarChar,txtHoTen.Text.Trim(),100),
                Db.P("@coNgay",SqlDbType.Bit,dtNgaySinh.Checked),
                Db.P("@ngay",SqlDbType.Date,dtNgaySinh.Value.Date),
                Db.P("@lop",SqlDbType.VarChar,cboLop.SelectedItem == null ? "" : cboLop.SelectedItem.ToString(),20),
                Db.P("@ma",SqlDbType.VarChar,txtMa.Text.Trim(),20));
            MessageBox.Show(rows > 0 ? "Cập nhật sinh viên thành công." : "Không tìm thấy mã sinh viên.");
        }
        private void Xoa()
        {
            if (!Valid(false,false,false)) return;
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập họ tên để xác nhận theo yêu cầu bài."); return; }
            int rows = Db.Execute("DELETE FROM SinhVien WHERE MaSinhVien=@ma AND HoTen=@ten",
                Db.P("@ma",SqlDbType.VarChar,txtMa.Text.Trim(),20),
                Db.P("@ten",SqlDbType.NVarChar,txtHoTen.Text.Trim(),100));
            MessageBox.Show(rows > 0 ? "Xóa sinh viên thành công." : "Không tìm thấy sinh viên khớp mã và họ tên.");
        }
    }
}
