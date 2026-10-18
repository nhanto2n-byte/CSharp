using System; using System.Data; using System.Drawing; using System.Windows.Forms;
namespace Lab06_CSDL_Basic {
public class ListLopForm:Form {
ListView v=new ListView();
public ListLopForm(){Text="Danh sách lớp";StartPosition=FormStartPosition.CenterParent;ClientSize=new Size(650,390);v.SetBounds(20,55,610,270);v.View=View.Details;v.FullRowSelect=true;v.GridLines=true;v.Columns.Add("STT",55);v.Columns.Add("Mã lớp",130);v.Columns.Add("Tên lớp",260);v.Columns.Add("Mã khoa",130);Controls.Add(v);
var b=new Button{Text="Hiển thị dữ liệu",Left=20,Top=15,Width=140};b.Click+=(s,e)=>LoadData();Controls.Add(b);var c=new Button{Text="Thoát",Left=520,Top=335,Width=110};c.Click+=(s,e)=>Close();Controls.Add(c);}
void LoadData(){try{v.Items.Clear();int n=1;foreach(DataRow r in Db.Query("SELECT MaLop,TenLop,MaKhoa FROM Lop ORDER BY MaLop").Rows){var i=new ListViewItem((n++).ToString());i.SubItems.Add(r["MaLop"].ToString());i.SubItems.Add(r["TenLop"].ToString());i.SubItems.Add(r["MaKhoa"].ToString());v.Items.Add(i);}}catch(Exception e){MessageBox.Show(e.Message);}}
}}
