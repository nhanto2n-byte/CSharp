using System;

namespace Lab02ThucHanh.ThucHanh1
{
    // Bai 10: Phuong thuc kiem tra chuoi co doi xung (palindrome) hay khong
    static class Bai10
    {
        public static bool LaChuoiDoiXung(string s)
        {
            int i = 0;
            int j = s.Length - 1;

            while (i < j)
            {
                if (s[i] != s[j]) return false;
                i++;
                j--;
            }
            return true;
        }

        public static void Run()
        {
            Console.Write("Nhap chuoi: ");
            string chuoi = Console.ReadLine() ?? "";

            if (LaChuoiDoiXung(chuoi))
                Console.WriteLine("Chuoi doi xung.");
            else
                Console.WriteLine("Chuoi khong doi xung.");
        }
    }
}
