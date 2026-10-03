using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Menu con cho Thuc hanh 2 (Lap trinh huong doi tuong)
    // Da cai dat day du: muc 1 (1.1-1.5), muc 2 (2.1-2.7), muc 3 (3.1-3.6)
    public class MenuThucHanh2
    {
        public static void Hien()
        {
            bool dangChay = true;

            while (dangChay)
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("     THUC HANH 2 - LAP TRINH OOP        ");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("-- 1. Thiet ke lop co ban --");
                Console.WriteLine(" 1. Bai 1.1 - Tinh tuoi sinh vien");
                Console.WriteLine(" 2. Bai 1.2 - Lop Point (khoang cach, trung diem)");
                Console.WriteLine(" 3. Bai 1.3 - Lop Person");
                Console.WriteLine(" 4. Bai 1.4 - Lop Phan so");
                Console.WriteLine(" 5. Bai 1.5 - Lop Don thuc");
                Console.WriteLine("-- 2. Thiet ke lop nang cao --");
                Console.WriteLine(" 6. Bai 2.1 - Lop ArrayPoint (ArrayList + Indexer)");
                Console.WriteLine(" 7. Bai 2.2 - Lop PersonList");
                Console.WriteLine(" 8. Bai 2.3 - Lop chua mang 1 chieu (DaySo)");
                Console.WriteLine(" 9. Bai 2.4 - Lop chua mang 2 chieu (Mang2Chieu)");
                Console.WriteLine("10. Bai 2.5 - Da thuc (n+1 don thuc)");
                Console.WriteLine("11. Bai 2.6 - Day phan so (tinh tong)");
                Console.WriteLine("12. Bai 2.7 - Tinh luong phong ban");
                Console.WriteLine("-- 3. Ke thua va da hinh --");
                Console.WriteLine("13. Bai 3.1 - Array.Sort voi SinhVien (IComparable)");
                Console.WriteLine("14. Bai 3.2 - Sap xep mang tong quat bang interface");
                Console.WriteLine("15. Bai 3.3 - Sap xep mang tong quat bang delegate");
                Console.WriteLine("16. Bai 3.4 - Lop ConsoleMenu tong quat (giai PT bac 2)");
                Console.WriteLine("17. Bai 3.5 - Tinh luong nhan vien (ke thua, da hinh)");
                Console.WriteLine("18. Bai 3.6 - Tinh diem thi sinh (ke thua, da hinh)");
                Console.WriteLine(" 0. Quay lai menu chinh");
                Console.Write("Chon bai: ");

                string? luaChon = Console.ReadLine();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "1": Bai1_1.Run(); break;
                    case "2": Bai1_2.Run(); break;
                    case "3": Bai1_3.Run(); break;
                    case "4": Bai1_4.Run(); break;
                    case "5": Bai1_5.Run(); break;

                    case "6": Bai2_1.Run(); break;
                    case "7": Bai2_2.Run(); break;
                    case "8": Bai2_3.Run(); break;
                    case "9": Bai2_4.Run(); break;
                    case "10": Bai2_5.Run(); break;
                    case "11": Bai2_6.Run(); break;
                    case "12": Bai2_7.Run(); break;

                    case "13": Bai3_1.Run(); break;
                    case "14": Bai3_2.Run(); break;
                    case "15": Bai3_3.Run(); break;
                    case "16": Bai3_4.Run(); break;
                    case "17": Bai3_5.Run(); break;
                    case "18": Bai3_6.Run(); break;

                    case "0":
                        dangChay = false;
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le, vui long chon lai.");
                        break;
                }

                if (dangChay)
                {
                    Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                    Console.ReadKey();
                }
            }
        }
    }
}
