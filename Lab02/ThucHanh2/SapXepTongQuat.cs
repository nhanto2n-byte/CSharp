using System;

namespace Lab02ThucHanh.ThucHanh2
{
    // Bai 3.2 va 3.3: sap xep mang tong quat, tu viet thuat toan (khong goi Array.Sort co san)
    public static class SapXepTongQuat
    {
        // Bai 3.2: sap xep bang interface IComparable<T>
        // T phai "biet tu so sanh voi chinh no" thong qua IComparable<T>
        public static void SapXepBangInterface<T>(T[] mang) where T : IComparable<T>
        {
            for (int i = 0; i < mang.Length - 1; i++)
            {
                for (int j = 0; j < mang.Length - 1 - i; j++)
                {
                    if (mang[j].CompareTo(mang[j + 1]) > 0)
                    {
                        T tam = mang[j];
                        mang[j] = mang[j + 1];
                        mang[j + 1] = tam;
                    }
                }
            }
        }

        // Bai 3.3: sap xep bang delegate - cach so sanh duoc truyen vao tu ben ngoai,
        // khong bat buoc T phai implement IComparable
        public static void SapXepBangDelegate<T>(T[] mang, Comparison<T> soSanh)
        {
            for (int i = 0; i < mang.Length - 1; i++)
            {
                for (int j = 0; j < mang.Length - 1 - i; j++)
                {
                    if (soSanh(mang[j], mang[j + 1]) > 0)
                    {
                        T tam = mang[j];
                        mang[j] = mang[j + 1];
                        mang[j + 1] = tam;
                    }
                }
            }
        }
    }
}
