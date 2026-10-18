using System; using System.Data; using System.Data.SqlClient; using System.Drawing; using System.Windows.Forms;
namespace Lab06_CSDL_Basic {
public class LopForm:Form {
TextBox ma=new TextBox(),ten=new TextBox(); ComboBox khoa=new ComboBox();
public LopForm(){Text="Quản lý lớp";StartPosition=FormStartPosition.CenterParent;ClientSize=new Size(450,235);Font=new Font("Segoe UI",10);
L("Mã lớp",20);ma.SetBounds(130,17,275,27);Controls.Add(ma);L("Tên lớp",58);ten.SetBounds(130,55,275,27);Controls.Add(ten);
L("Mã khoa",96);khoa.SetBounds(130,93,275,27);khoa.DropDownStyle=ComboBoxStyle.DropDownList;Controls.Add(khoa);
B("Thêm",25,Them);B("Sửa",125,Sua);B("Xóa",225,Xoa);var r=new Button{Text="Tải khoa",Left=325,Top=145,Width=80};r.Click+=(s,e)=>LoadKhoa();Controls.Add(r);
Load+=(s,e)=>{try{LoadKhoa();}catch(Exception x){MessageBox.Show(x.Message);}};}
void L(string t,int y){Controls.Add(new Label{Text=t,Left=25,Top=y+4,Width=95});}
void B(string t,int x,Action a){var b=new Button{Text=t,Left=x,Top=145,Width=85,Height=32};b.Click+=(s,e)=>{try{a();}catch(Exception z){MessageBox.Show(z.Message);}};Controls.Add(b);}
void LoadKhoa(){khoa.Items.Clear();foreach(DataRow r in Db.Query("SELECT MaKhoa FROM Khoa ORDER BY MaKhoa").Rows)khoa.Items.Add(r[0].ToString());if(khoa.Items.Count>0)khoa.SelectedIndex=0;}
bool V(bool tn,bool tk){if(string.IsNullOrWhiteSpace(ma.Text)){MessageBox.Show("Nhập mã lớp.");return false;}if(tn&&string.IsNullOrWhiteSpace(ten.Text)){MessageBox.Show("Nhập tên lớp.");return false;}if(tk&&khoa.SelectedItem==null){MessageBox.Show("Chọn mã khoa.");return false;}return true;}
void Them(){if(!V(true,true))return;if(Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM Lop WHERE MaLop=@m",Db.P("@m",SqlDbType.VarChar,ma.Text.Trim(),20)))>0){MessageBox.Show("Mã lớp đã tồn tại.");return;}Db.Execute("INSERT INTO Lop(MaLop,TenLop,MaKhoa) VALUES(@m,@t,@k)",Db.P("@m",SqlDbType.VarChar,ma.Text.Trim(),20),Db.P("@t",SqlDbType.NVarChar,ten.Text.Trim(),100),Db.P("@k",SqlDbType.VarChar,khoa.Text,20));MessageBox.Show("Thêm lớp thành công.");}
void Sua(){if(!V(false,false))return;if(ten.Text.Trim()==""&&khoa.SelectedItem==null){MessageBox.Show("Nhập tên lớp hoặc chọn khoa để sửa.");return;}int n=Db.Execute("UPDATE Lop SET TenLop=CASE WHEN @t=N'' THEN TenLop ELSE @t END,MaKhoa=CASE WHEN @k='' THEN MaKhoa ELSE @k END WHERE MaLop=@m",Db.P("@t",SqlDbType.NVarChar,ten.Text.Trim(),100),Db.P("@k",SqlDbType.VarChar,khoa.SelectedItem==null?"":khoa.Text,20),Db.P("@m",SqlDbType.VarChar,ma.Text.Trim(),20));MessageBox.Show(n>0?"Cập nhật thành công.":"Không tìm thấy mã lớp.");}
void Xoa(){if(!V(false,false))return;try{int n=Db.Execute("DELETE FROM Lop WHERE MaLop=@m",Db.P("@m",SqlDbType.VarChar,ma.Text.Trim(),20));MessageBox.Show(n>0?"Xóa thành công.":"Không tìm thấy mã lớp.");}catch(SqlException e){MessageBox.Show("Không thể xóa lớp đang được sinh viên tham chiếu. "+e.Message);}}
}}
