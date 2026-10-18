using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài về nhà 3: Danh bạ. TreeView chứa các node A..Z;
    /// thêm người mới sẽ được đặt vào node ứng với chữ cái đầu của TÊN (First Name).
    /// Hiển thị dạng "First Name, Last Name" (vd: "Bình, Ngô Thanh" nằm ở node B).
    /// </summary>
    public class FrmDanhBa : Form
    {
        private TreeView trvDanhBa;
        private TextBox txtFirstName, txtLastName;
        private Button btnAdd, btnExit;

        public FrmDanhBa()
        {
            Helper.CauHinhForm(this, "frmBTVN2", 540, 400);

            trvDanhBa = new TreeView { Left = 10, Top = 10, Width = 200, Height = 380 };
            // Khi form load: nạp 26 node từ A đến Z
            for (char c = 'A'; c <= 'Z'; c++)
                trvDanhBa.Nodes.Add(c.ToString());

            GroupBox grp = new GroupBox { Left = 225, Top = 10, Width = 300, Height = 140 };
            txtFirstName = Helper.Txt(100, 25, 180);
            txtLastName = Helper.Txt(100, 65, 180);
            btnAdd = Helper.Btn("Add", 190, 100, 90);
            grp.Controls.AddRange(new Control[] {
                Helper.Lbl("First Name", 15, 25, 80), txtFirstName,
                Helper.Lbl("Last Name", 15, 65, 80), txtLastName, btnAdd });

            btnExit = Helper.Btn("Exit", 435, 165, 90);

            btnAdd.Click += btnAdd_Click;
            btnExit.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { trvDanhBa, grp, btnExit });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string first = txtFirstName.Text.Trim();
            string last = txtLastName.Text.Trim();
            if (first == "" || last == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ First Name và Last Name!", "Thông báo");
                txtFirstName.Focus();
                return;
            }

            char chu = LayChuCaiDau(first);
            if (chu == '\0')
            {
                MessageBox.Show("First Name phải bắt đầu bằng một chữ cái!", "Thông báo");
                txtFirstName.Focus();
                return;
            }

            // Tìm node chữ cái tương ứng (node có Text == chữ cái)
            TreeNode nodeChu = null;
            foreach (TreeNode n in trvDanhBa.Nodes)
                if (n.Text == chu.ToString()) { nodeChu = n; break; }

            string hienThi = first + ", " + last;

            // Thêm theo thứ tự A-Z trong node; không cho trùng
            int viTri = nodeChu.Nodes.Count;
            for (int i = 0; i < nodeChu.Nodes.Count; i++)
            {
                int ss = string.Compare(hienThi, nodeChu.Nodes[i].Text, StringComparison.CurrentCultureIgnoreCase);
                if (ss == 0)
                {
                    MessageBox.Show("Người này đã có trong danh bạ!", "Thông báo");
                    return;
                }
                if (ss < 0) { viTri = i; break; }
            }
            TreeNode moi = new TreeNode(hienThi);
            nodeChu.Nodes.Insert(viTri, moi);
            nodeChu.Expand();
            trvDanhBa.SelectedNode = moi;

            txtFirstName.Clear();
            txtLastName.Clear();
            txtFirstName.Focus();
        }

        // Lấy chữ cái đầu (A-Z) của tên, bỏ dấu tiếng Việt: "Bình" -> 'B', "Đức" -> 'D', "Ánh" -> 'A'
        // Trả về '\0' nếu ký tự đầu không phải chữ cái.
        private char LayChuCaiDau(string s)
        {
            char c = char.ToUpper(s[0]);
            if (c == 'Đ') return 'D';
            // FormD: tách chữ và dấu, ký tự đầu tiên là chữ gốc (Á -> A + dấu)
            string chuan = c.ToString().Normalize(NormalizationForm.FormD);
            char goc = chuan[0];
            if (goc >= 'A' && goc <= 'Z') return goc;
            return '\0';
        }
    }
}
