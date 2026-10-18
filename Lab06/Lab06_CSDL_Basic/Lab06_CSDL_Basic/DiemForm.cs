using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab06_CSDL_Basic
{
    /// <summary>Thực hành 6b: thêm, xóa, sửa điểm theo khóa chính ghép (MaSinhVien, MaMonHoc).</summary>
    public class DiemForm : Form
    {
        private readonly ComboBox cboSinhVien = new ComboBox();
        private readonly ComboBox cboMonHoc = new ComboBox();
        private readonly TextBox txtDiem = new TextBox();

        public DiemForm()
        {
            Text = "6b - Quản lý điểm"; StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(470, 230); Font = new Font("Segoe UI", 10);
            AddLabel("Sinh viên", 25); cboSinhVien.SetBounds(140,22,290,27); cboSinhVien.DropDownStyle=ComboBoxStyle.DropDownList; Controls.Add(cboSinhVien);
            AddLabel("Môn học", 65); cboMonHoc.SetBounds(140,62,290,27); cboMonHoc.DropDownStyle=ComboBoxStyle.DropDownList; Controls.Add(cboMonHoc);
            AddLabel("Điểm (0–10)", 105); txtDiem.SetBounds(140,102,290,27); Controls.Add(txtDiem);
            AddButton("Thêm", 35, Them); AddButton("Sửa", 135, Sua); AddButton("Xóa", 235, Xoa);
            Button reload = new Button { Text="Tải danh sách", Left=315, Top=150, Width=115, Height=32 };
            reload.Click += (s,e) => LoadCombos(); Controls.Add(reload);
            Load += (s,e) => { try { LoadCombos(); } catch(Exception ex) { MessageBox.Show("Không tải được dữ liệu: "+ex.Message); } };
        }
        private void AddLabel(string text,int top) => Controls.Add(new Label { Text=text,Left=25,Top=top+4,Width=110 });
        private void AddButton(string text,int left,Action action) {
            Button b=new Button { Text=text,Left=left,Top=150,Width=85,Height=32 };
            b.Click+=(s,e)=>{ try { action(); } catch(Exception ex) { MessageBox.Show("Thao tác thất bại: "+ex.Message); } };
            Controls.Add(b);
        }
        private void LoadCombos()
        {
            // Đọc khóa ngoại từ hai bảng gốc rồi hiển thị mã trong ComboBox.
            cboSinhVien.Items.Clear(); cboMonHoc.Items.Clear();
            foreach(DataRow r in Db.Query("SELECT MaSinhVien FROM SinhVien ORDER BY MaSinhVien").Rows) cboSinhVien.Items.Add(r[0].ToString());
            foreach(DataRow r in Db.Query("SELECT MaMonHoc FROM MonHoc ORDER BY MaMonHoc").Rows) cboMonHoc.Items.Add(r[0].ToString());
            if(cboSinhVien.Items.Count>0) cboSinhVien.SelectedIndex=0;
            if(cboMonHoc.Items.Count>0) cboMonHoc.SelectedIndex=0;
        }
        private bool ValidateInput(bool needScore)
        {
            if(cboSinhVien.SelectedItem==null) { MessageBox.Show("Vui lòng chọn sinh viên."); return false; }
            if(cboMonHoc.SelectedItem==null) { MessageBox.Show("Vui lòng chọn môn học."); return false; }
            if(needScore) {
                decimal score;
                if(string.IsNullOrWhiteSpace(txtDiem.Text) || !decimal.TryParse(txtDiem.Text.Trim(),out score)) { MessageBox.Show("Điểm phải là số."); txtDiem.Focus(); return false; }
                if(score<0 || score>10) { MessageBox.Show("Điểm phải nằm trong khoảng 0 đến 10."); txtDiem.Focus(); return false; }
            }
            return true;
        }
        private void Them()
        {
            if(!ValidateInput(true)) return;
            string maSv=cboSinhVien.SelectedItem.ToString(), maMh=cboMonHoc.SelectedItem.ToString();
            int count=Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh",
                Db.P("@sv",SqlDbType.VarChar,maSv,20),Db.P("@mh",SqlDbType.VarChar,maMh,20)));
            if(count>0) { MessageBox.Show("Cặp mã sinh viên/mã môn học đã có điểm."); return; }
            decimal score=decimal.Parse(txtDiem.Text.Trim());
            int rows=Db.Execute("INSERT INTO Diem(MaSinhVien,MaMonHoc,Diem) VALUES(@sv,@mh,@diem)",
                Db.P("@sv",SqlDbType.VarChar,maSv,20),Db.P("@mh",SqlDbType.VarChar,maMh,20),Db.P("@diem",SqlDbType.Decimal,score));
            MessageBox.Show(rows>0?"Thêm điểm thành công.":"Không thêm được điểm.");
        }
        private void Sua()
        {
            if(!ValidateInput(true)) return;
            decimal score=decimal.Parse(txtDiem.Text.Trim());
            int rows=Db.Execute("UPDATE Diem SET Diem=@diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh",
                Db.P("@diem",SqlDbType.Decimal,score),Db.P("@sv",SqlDbType.VarChar,cboSinhVien.SelectedItem.ToString(),20),
                Db.P("@mh",SqlDbType.VarChar,cboMonHoc.SelectedItem.ToString(),20));
            MessageBox.Show(rows>0?"Cập nhật điểm thành công.":"Không tìm thấy dòng điểm cần sửa.");
        }
        private void Xoa()
        {
            if(!ValidateInput(false)) return;
            int rows=Db.Execute("DELETE FROM Diem WHERE MaSinhVien=@sv AND MaMonHoc=@mh",
                Db.P("@sv",SqlDbType.VarChar,cboSinhVien.SelectedItem.ToString(),20),
                Db.P("@mh",SqlDbType.VarChar,cboMonHoc.SelectedItem.ToString(),20));
            MessageBox.Show(rows>0?"Xóa điểm thành công.":"Không tìm thấy dòng điểm cần xóa.");
        }
    }
}
