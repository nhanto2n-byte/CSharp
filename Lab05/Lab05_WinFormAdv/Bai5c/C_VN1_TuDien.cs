using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài về nhà 1: Từ điển Anh - Việt và Việt - Anh (TabControl, 2 tab).
    /// - Gõ vào ComboBox: tự dò tới từ khớp gần nhất trong danh sách.
    /// - Nhấn Enter hoặc double click vào từ: hiện nghĩa ở TextBox bên phải.
    /// - Danh sách từ lưu sẵn trong List.
    /// </summary>
    public class FrmTuDien : Form
    {
        private TabControl tabControl1;

        public FrmTuDien()
        {
            Helper.CauHinhForm(this, "TỪ ĐIỂN ANH VIỆT - VIỆT ANH", 520, 410);

            // Dữ liệu từ điển lưu sẵn
            Dictionary<string, string> anhViet = new Dictionary<string, string>();
            anhViet["student"] = "Sinh viên, học sinh";
            anhViet["teacher"] = "Giáo viên";
            anhViet["worker"] = "Công nhân";
            anhViet["hat"] = "Cái mũ";
            anhViet["head"] = "Cái đầu";
            anhViet["mouse"] = "Con chuột";
            anhViet["cat"] = "Con mèo";
            anhViet["dog"] = "Con chó";
            anhViet["snake"] = "Con rắn";
            anhViet["lamb"] = "Con cừu non";
            anhViet["sheep"] = "Con cừu";
            anhViet["frog"] = "Con ếch";
            anhViet["book"] = "Quyển sách";
            anhViet["house"] = "Ngôi nhà";
            anhViet["water"] = "Nước";

            Dictionary<string, string> vietAnh = new Dictionary<string, string>();
            vietAnh["sinh viên"] = "student";
            vietAnh["giáo viên"] = "teacher";
            vietAnh["công nhân"] = "worker";
            vietAnh["mũ"] = "hat";
            vietAnh["đầu"] = "head";
            vietAnh["chuột"] = "mouse";
            vietAnh["mèo"] = "cat";
            vietAnh["chó"] = "dog";
            vietAnh["rắn"] = "snake";
            vietAnh["cừu"] = "sheep";
            vietAnh["ếch"] = "frog";
            vietAnh["sách"] = "book";
            vietAnh["nhà"] = "house";
            vietAnh["nước"] = "water";

            tabControl1 = new TabControl { Left = 10, Top = 10, Width = 500, Height = 350 };
            tabControl1.TabPages.Add(TaoTab("ANH - VIỆT", "Tiếng Anh", "Tiếng Việt", anhViet));
            tabControl1.TabPages.Add(TaoTab("VIỆT - ANH", "Tiếng Việt", "Tiếng Anh", vietAnh));

            Button btnThoat = Helper.Btn("Thoát", 420, 368, 90);
            btnThoat.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { tabControl1, btnThoat });
        }

        // Dựng 1 tab tra từ cho 1 chiều dịch
        private TabPage TaoTab(string tieuDeTab, string nhanTu, string nhanNghia, Dictionary<string, string> tuDien)
        {
            TabPage tab = new TabPage(tieuDeTab);

            // Danh sách từ lưu sẵn vào List (sắp xếp A-Z)
            List<string> dsTu = new List<string>(tuDien.Keys);
            dsTu.Sort();

            Label lblTu = Helper.Lbl(nhanTu, 15, 10, 200);
            ComboBox cboTu = new ComboBox
            {
                Left = 15, Top = 38, Width = 210,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };
            cboTu.Items.AddRange(dsTu.ToArray());

            ListBox lstTu = new ListBox { Left = 15, Top = 70, Width = 210, Height = 230 };
            lstTu.Items.AddRange(dsTu.ToArray());

            Label lblNghia = Helper.Lbl(nhanNghia, 245, 10, 200);
            TextBox txtNghia = new TextBox { Left = 245, Top = 38, Width = 230, Height = 262, Multiline = true, ReadOnly = true };

            tab.Controls.AddRange(new Control[] { lblTu, cboTu, lstTu, lblNghia, txtNghia });

            // Hàm tra nghĩa của 1 từ (không phân biệt hoa/thường)
            Action<string> tra = tu =>
            {
                string tuKhop = null;
                foreach (string k in dsTu)
                {
                    if (string.Compare(k, tu.Trim(), true) == 0)
                    {
                        tuKhop = k;
                        break;
                    }
                }
                if (tuKhop != null)
                    txtNghia.Text = tuDien[tuKhop];
                else
                    txtNghia.Text = "Không tìm thấy từ \"" + tu.Trim() + "\" trong từ điển";
            };

            // Gõ chữ -> dò tới từ bắt đầu bằng các ký tự đã gõ (gần nhất) trong danh sách
            cboTu.TextChanged += (s, e) =>
            {
                string gv = cboTu.Text.Trim();
                lstTu.SelectedIndex = -1;
                if (gv == "") return;
                for (int i = 0; i < dsTu.Count; i++)
                {
                    if (dsTu[i].StartsWith(gv, StringComparison.CurrentCultureIgnoreCase))
                    {
                        lstTu.SelectedIndex = i;   // chọn (và cuộn tới) từ khớp đầu tiên
                        return;
                    }
                }
            };

            // Nhấn Enter trong combobox -> tra từ
            cboTu.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    tra(cboTu.Text);
                    e.SuppressKeyPress = true;
                }
            };

            // Chọn từ trong danh sách thả xuống của combobox -> tra từ
            cboTu.SelectionChangeCommitted += (s, e) => tra(cboTu.Text);

            // Double click vào từ trong listbox -> tra từ
            lstTu.DoubleClick += (s, e) =>
            {
                if (lstTu.SelectedItem != null)
                {
                    cboTu.Text = lstTu.SelectedItem.ToString();
                    tra(cboTu.Text);
                }
            };

            return tab;
        }
    }
}
