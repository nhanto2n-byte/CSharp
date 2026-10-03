using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.3: Viet phuong thuc sap xep mot mang tong quat bang delegate
    // Uu diem so voi Bai 3.2: khong bat buoc lop phai implement IComparable,
    // cach so sanh duoc "tiem" tu ben ngoai qua delegate Comparison<T>.
    public class Bai3_3
    {
        public static void Run()
        {
            Console.WriteLine("=== Bai 3.3: Sap xep mang tong quat bang delegate ===");

            Console.Write("Nhap so luong nhan vien: ");
            int n = int.Parse(Console.ReadLine()!);

            NhanVienDon[] ds = new NhanVienDon[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"-- Nhan vien thu {i + 1} --");
                ds[i] = new NhanVienDon();
                ds[i].Input();
            }

            Console.WriteLine("-- Truoc khi sap xep --");
            foreach (NhanVienDon nv in ds) nv.Output();

            // Delegate Comparison<T>: so sanh theo luong thuc nhan, giam dan
            Comparison<NhanVienDon> soSanhTheoLuongGiamDan = (a, b) =>
                b.TinhLuongThucNhan().CompareTo(a.TinhLuongThucNhan());

            SapXepTongQuat.SapXepBangDelegate(ds, soSanhTheoLuongGiamDan);

            Console.WriteLine("-- Sau khi sap xep bang delegate (luong giam dan) --");
            foreach (NhanVienDon nv in ds) nv.Output();
        }
    }
}
