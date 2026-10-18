using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05_WinFormAdv.Bai5c
{
    /// <summary>
    /// 5c - Bài mẫu 1: chuyển phần tử qua lại giữa hai ListBox.
    /// Nút >, >>, <, <<, "Chuyển tùy ý" và hỏi xác nhận khi đóng form.
    /// </summary>
    public class FrmListBoxMau : Form
    {
        private ListBox lst_Trai, lst_Phai;
        private Button btnQuaPhai, btnQuaPhaiAll, btnQuaTrai, btnQuaTraiAll, btnTuyY;

        public FrmListBoxMau()
        {
            Helper.CauHinhForm(this, "Sử dụng listbox", 400, 300);

            // ListBox trái cho chọn nhiều dòng (phục vụ nút "Chuyển tùy ý")
            lst_Trai = new ListBox { Name = "lst_Trai", Left = 15, Top = 15, Width = 140, Height = 220, SelectionMode = SelectionMode.MultiExtended };
            lst_Trai.Items.AddRange(new object[] { "Cốc", "Ổi", "Xoài", "Me", "Bưởi", "Cam" });
            lst_Phai = new ListBox { Name = "lst_Phai", Left = 245, Top = 15, Width = 140, Height = 220 };

            btnQuaPhai = Helper.Btn(">", 165, 15, 70);
            btnQuaPhaiAll = Helper.Btn(">>", 165, 55, 70);
            btnQuaTrai = Helper.Btn("<", 165, 95, 70);
            btnQuaTraiAll = Helper.Btn("<<", 165, 135, 70);
            btnTuyY = Helper.Btn("Chuyển tùy ý", 15, 250, 140);

            btnQuaPhai.Click += btnQuaPhai_Click;
            btnQuaPhaiAll.Click += btnQuaPhaiAll_Click;
            btnQuaTrai.Click += btnQuaTrai_Click;
            btnQuaTraiAll.Click += btnQuaTraiAll_Click;
            btnTuyY.Click += btnTuyY_Click;
            this.FormClosing += FrmListBoxMau_FormClosing;

            this.Controls.AddRange(new Control[] { lst_Trai, lst_Phai, btnQuaPhai, btnQuaPhaiAll, btnQuaTrai, btnQuaTraiAll, btnTuyY });
        }

        // Chuyển 1 item đang chọn từ listbox trái sang phải
        private void btnQuaPhai_Click(object sender, EventArgs e)
        {
            if (lst_Trai.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn một phần tử bên trái!", "Thông báo");
                return;
            }
            object item = lst_Trai.SelectedItem;
            lst_Phai.Items.Add(item);      // thêm sang phải
            lst_Trai.Items.Remove(item);   // xóa bên trái
        }

        // Chuyển tất cả item từ trái sang phải
        private void btnQuaPhaiAll_Click(object sender, EventArgs e)
        {
            lst_Phai.Items.AddRange(lst_Trai.Items);
            lst_Trai.Items.Clear();
        }

        // Chuyển 1 item đang chọn từ phải sang trái
        private void btnQuaTrai_Click(object sender, EventArgs e)
        {
            if (lst_Phai.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn một phần tử bên phải!", "Thông báo");
                return;
            }
            object item = lst_Phai.SelectedItem;
            lst_Trai.Items.Add(item);
            lst_Phai.Items.Remove(item);
        }

        // Chuyển tất cả item từ phải sang trái
        private void btnQuaTraiAll_Click(object sender, EventArgs e)
        {
            lst_Trai.Items.AddRange(lst_Phai.Items);
            lst_Phai.Items.Clear();
        }

        // Chuyển tùy ý: mọi item đang được chọn (có thể nhiều item) ở listbox trái sang phải
        private void btnTuyY_Click(object sender, EventArgs e)
        {
            if (lst_Trai.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất một phần tử bên trái!", "Thông báo");
                return;
            }
            // Copy danh sách chọn ra trước, vì vừa duyệt vừa xóa sẽ làm thay đổi tập hợp
            List<object> dsChon = new List<object>();
            foreach (object o in lst_Trai.SelectedItems)
                dsChon.Add(o);
            foreach (object o in dsChon)
            {
                lst_Phai.Items.Add(o);
                lst_Trai.Items.Remove(o);
            }
        }

        private void FrmListBoxMau_FormClosing(object sender, FormClosingEventArgs e)
        {
            Helper.XacNhanDong(e);
        }
    }
}
