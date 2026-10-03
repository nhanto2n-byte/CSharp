using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Menu con cho Thuc hanh 1, liet ke 17 bai tap va goi ham Run() tuong ung
    static class MenuThucHanh1
    {
        public static void Hien()
        {
            bool dangChay = true;

            while (dangChay)
            {
                Console.WriteLine();
                Console.WriteLine("----------- THUC HANH 1 -----------");
                Console.WriteLine(" 1. Bai 1  - Nhap/xuat ho ten");
                Console.WriteLine(" 2. Bai 2  - Xuat va nhap chuoi");
                Console.WriteLine(" 3. Bai 3  - Nhap so nguyen, tinh x^y");
                Console.WriteLine(" 4. Bai 4  - Bai 3 co kiem tra loi nhap");
                Console.WriteLine(" 5. Bai 5  - Menu tinh toan so thuc");
                Console.WriteLine(" 6. Bai 6  - Gia tri lon nhat cua 3 so nguyen");
                Console.WriteLine(" 7. Bai 7  - Kiem tra so nguyen to");
                Console.WriteLine(" 8. Bai 8  - Hoan vi 2 so thuc (ref)");
                Console.WriteLine(" 9. Bai 9  - Max/Min cua 3 so thuc (out)");
                Console.WriteLine("10. Bai 10 - Kiem tra chuoi doi xung");
                Console.WriteLine("11. Bai 11 - Dao nguoc chuoi");
                Console.WriteLine("12. Bai 12 - Chuoi thuong/hoa, dem so tu");
                Console.WriteLine("13. Bai 13 - Nhap xuat thong tin sinh vien");
                Console.WriteLine("14. Bai 14 - Tinh luong nhan vien");
                Console.WriteLine("15. Bai 15 - Cac phuong thuc voi mang 1 chieu");
                Console.WriteLine("16. Bai 16 - Sap xep mang ho ten");
                Console.WriteLine("17. Bai 17 - Mang 2 chieu ngau nhien");
                Console.WriteLine(" 0. Quay lai menu chinh");
                Console.Write("Chon bai: ");

                string? luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1": Bai1.Run(); break;
                    case "2": Bai2.Run(); break;
                    case "3": Bai3.Run(); break;
                    case "4": Bai4.Run(); break;
                    case "5": Bai5.Run(); break;
                    case "6": Bai6.Run(); break;
                    case "7": Bai7.Run(); break;
                    case "8": Bai8.Run(); break;
                    case "9": Bai9.Run(); break;
                    case "10": Bai10.Run(); break;
                    case "11": Bai11.Run(); break;
                    case "12": Bai12.Run(); break;
                    case "13": Bai13.Run(); break;
                    case "14": Bai14.Run(); break;
                    case "15": Bai15.Run(); break;
                    case "16": Bai16.Run(); break;
                    case "17": Bai17.Run(); break;
                    case "0": dangChay = false; break;
                    default:
                        Console.WriteLine("Lua chon khong hop le, vui long chon lai.");
                        break;
                }
            }
        }
    }
}
