using System;
using System.Windows.Forms;

namespace Lab05_WinFormAdv
{
    internal static class Program
    {
        // Điểm vào chương trình: mở form chính, chọn bài ở thanh menu
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
