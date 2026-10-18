using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab06_CSDL_Basic
{
    /// <summary>Thực hành 6a: thêm, xóa, sửa bảng MonHoc.</summary>
    public class MonHocForm : Form
    {
        private readonly TextBox txtMa = new TextBox();
        private readonly TextBox txtTen = new TextBox();

        public MonHocForm()
        {
            Text = "6a - Quản lý môn học"; StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(430, 210); Font = new Font("Segoe UI", 10);
            AddLabel("Mã môn học", 25); txtMa.SetBounds(145, 22, 245, 28); Controls.Add(txtMa);
            AddLabel("Tên môn học", 65); txtTen.SetBounds(145, 62, 245, 28); Controls.Add(txtTen);
            AddButton("Thêm", 105, Them); AddButton("Sửa", 175, Sua); AddButton("Xóa", 245, Xoa);
            Button clear = new Button { Text = "Làm mới", Left = 315, Top = 105, Width = 75 };
            clear.Click += (s,e) => { txtMa.Clear(); txtTen.Clear(); txtMa.Focus(); }; Controls.Add(clear);
        }

        private void AddLabel(string text, int top) => Controls.Add(new Label { Text=text, Left=25, Top=top+3, Width=110 });
        private void AddButton(string text, int left, Action action)
        {
            Button b = new Button { Text=text, Left=left, Top=150, Width=85, Height=32 };
            b.Click += (s,e) => { try { action(); } catch (SqlException ex) { MessageBox.Show("Lỗi SQL: " + ex.Message); } catch (Exception ex) { MessageBox.Show(ex.Message); } };
            Controls.Add(b);
        }
        private bool ValidateInput(bool requireName)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text)) { MessageBox.Show("Vui lòng nhập mã môn học."); txtMa.Focus(); return false; }
            if (requireName && string.IsNullOrWhiteSpace(txtTen.Text)) { MessageBox.Show("Vui lòng nhập tên môn học."); txtTen.Focus(); return false; }
            return true;
        }
        private void Them()
        {
            if (!ValidateInput(true)) return;
            // Kiểm tra trùng khóa chính trước khi thêm.
            int count = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM MonHoc WHERE MaMonHoc=@ma",
                Db.P("@ma", SqlDbType.VarChar, txtMa.Text.Trim(), 20)));
            if (count > 0) { MessageBox.Show("Mã môn học đã tồn tại."); return; }
            int rows = Db.Execute("INSERT INTO MonHoc(MaMonHoc, TenMonHoc) VALUES(@ma,@ten)",
                Db.P("@ma", SqlDbType.VarChar, txtMa.Text.Trim(), 20),
                Db.P("@ten", SqlDbType.NVarChar, txtTen.Text.Trim(), 100));
            MessageBox.Show(rows > 0 ? "Thêm môn học thành công." : "Không có dữ liệu được thêm.");
        }
        private void Sua()
        {
            if (!ValidateInput(true)) return;
            int rows = Db.Execute("UPDATE MonHoc SET TenMonHoc=@ten WHERE MaMonHoc=@ma",
                Db.P("@ten", SqlDbType.NVarChar, txtTen.Text.Trim(), 100),
                Db.P("@ma", SqlDbType.VarChar, txtMa.Text.Trim(), 20));
            MessageBox.Show(rows > 0 ? "Cập nhật thành công." : "Không tìm thấy mã môn học.");
        }
        private void Xoa()
        {
            if (!ValidateInput(false)) return;
            if (MessageBox.Show("Bạn chắc chắn muốn xóa môn học này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try {
                int rows = Db.Execute("DELETE FROM MonHoc WHERE MaMonHoc=@ma",
                    Db.P("@ma", SqlDbType.VarChar, txtMa.Text.Trim(), 20));
                MessageBox.Show(rows > 0 ? "Xóa thành công." : "Không tìm thấy mã môn học.");
            } catch (SqlException ex) {
                // Môn học đang được bảng Diem tham chiếu sẽ không xóa được do khóa ngoại.
                MessageBox.Show("Không thể xóa. Có thể môn học đang được sử dụng trong bảng Điểm.\n" + ex.Message);
            }
        }
    }
}
