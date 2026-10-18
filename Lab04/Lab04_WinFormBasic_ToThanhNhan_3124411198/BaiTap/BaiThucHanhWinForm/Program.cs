using System;
using System.Windows.Forms;

namespace BaiThucHanhWinForm
{
    /// <summary>
    /// Điểm bắt đầu của ứng dụng.
    /// Program.cs chỉ làm một việc: mở FormMenu (menu chọn bài).
    /// Chọn bài nào thì form của bài đó mới được mở (giống menu 1..N ở các lab trước).
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMenu());
        }
    }
}
