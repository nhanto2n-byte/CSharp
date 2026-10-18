using System; using System.Data; using System.Data.SqlClient; using System.Drawing; using System.Windows.Forms;
namespace Lab06_CSDL_Basic {
public class ListDiemForm:Form {
ComboBox mon=new ComboBox();ListView v=new ListView();TextBox tong=new TextBox();
public ListDiemForm(){Text="Danh sách điểm sinh viên";StartPosition=FormStartPosition.CenterParent;ClientSize=new Size(800,470);Controls.Add(new Label{Text="Môn học",Left=25,Top=22,Width=80});mon.SetBounds(105,18,180,27);mon.DropDownStyle=ComboBoxStyle.DropDownList;Controls.Add(mon);
var b=new Button{Text="Lọc dữ liệu",Left=305,Top=17,Width=110};b.Click+=(s,e)=>LoadData();Controls.Add(b);v.SetBounds(25,60,750,330);v.View=View.Details;v.FullRowSelect=true;v.GridLines=true;v.Columns.Add("STT",50);v.Columns.Add("Tên sinh viên",190);v.Columns.Add("Ngày sinh",110);v.Columns.Add("Tên môn học",230);v.Columns.Add("Điểm",80);Controls.Add(v);
Controls.Add(new Label{Text="Tổng điểm",Left=500,Top=410,Width=80});tong.SetBounds(585,405,100,27);tong.ReadOnly=true;Controls.Add(tong);
Load+=(s,e)=>{try{mon.Items.Add("");foreach(DataRow r in Db.Query("SELECT MaMonHoc FROM MonHoc ORDER BY MaMonHoc").Rows)mon.Items.Add(r[0].ToString());mon.SelectedIndex=0;LoadData();}catch(Exception x){MessageBox.Show(x.Message);}};}
void LoadData(){string q="SELECT sv.HoTen,sv.NgaySinh,mh.TenMonHoc,d.Diem FROM Diem d JOIN SinhVien sv ON sv.MaSinhVien=d.MaSinhVien JOIN MonHoc mh ON mh.MaMonHoc=d.MaMonHoc";
DataTable dt=string.IsNullOrWhiteSpace(mon.Text)?Db.Query(q+" ORDER BY sv.HoTen,mh.TenMonHoc"):Db.Query(q+" WHERE d.MaMonHoc=@mh ORDER BY sv.HoTen",Db.P("@mh",SqlDbType.VarChar,mon.Text,20));
v.Items.Clear();decimal sum=0;int n=1;foreach(DataRow r in dt.Rows){var i=new ListViewItem((n++).ToString());i.SubItems.Add(r["HoTen"].ToString());i.SubItems.Add(Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy"));i.SubItems.Add(r["TenMonHoc"].ToString());i.SubItems.Add(Convert.ToDecimal(r["Diem"]).ToString("0.##"));v.Items.Add(i);sum+=Convert.ToDecimal(r["Diem"]);}tong.Text=sum.ToString("0.##");}
}}
