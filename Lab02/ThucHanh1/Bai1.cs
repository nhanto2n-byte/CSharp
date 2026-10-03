using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 1: Nhap ho ten va xuat ho ten da nhap ra man hinh console.
    //
    // Ghi chu: cac buoc B2 (dung ildasm.exe de xem ma MSIL cua file PE)
    // va B3 (dung ilasm.exe de bien dich MSIL nguoc lai thanh file PE)
    // la thao tac thuc hien thu cong tren Developer Command Prompt /
    // Visual Studio, khong phai code C#, nen khong the hien thuc bang
    // ham trong file nay. Sau khi build project (dotnet build), file .dll
    // sinh ra trong thu muc bin/Debug/net8.0 chinh la file PE can dung
    // cho hai buoc do.
    static class Bai1
    {
        public static void Run()
        {
            Console.Write("Nhap ho ten cua ban: ");
            string? hoTen = Console.ReadLine();

            Console.WriteLine("Ho ten ban vua nhap la: " + hoTen);
        }
    }
}
