using System.Collections.Generic;

namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// Class lưu trữ mảng một chiều số nguyên (4d - tại lớp, Bài 2).
    /// Vị trí (index) trong mảng tính từ 0.
    /// </summary>
    public class MangSoNguyen
    {
        private List<int> _ds = new List<int>();

        public int SoPhanTu { get { return _ds.Count; } }
        public bool Rong { get { return _ds.Count == 0; } }

        // ---------- Nhập / xóa toàn bộ ----------
        public void Nhap(IEnumerable<int> danhSach) { _ds = new List<int>(danhSach); }
        public void Reset() { _ds.Clear(); }

        // ---------- Sắp xếp ----------
        public void SapXepTang() { _ds.Sort(); }
        public void SapXepGiam() { _ds.Sort((x, y) => y.CompareTo(x)); }

        // Mảng đã sắp xếp tăng chưa? (Xóa/Thêm yêu cầu điều kiện này)
        public bool DaSapXepTang()
        {
            for (int i = 1; i < _ds.Count; i++)
                if (_ds[i] < _ds[i - 1]) return false;
            return true;
        }

        // ---------- Tìm kiếm ----------
        /// <summary>Tìm giá trị: trả về vị trí xuất hiện đầu tiên, -1 nếu không có.</summary>
        public int TimGiaTri(int giaTri) { return _ds.IndexOf(giaTri); }

        /// <summary>Tìm theo vị trí: trả về false nếu vị trí nằm ngoài mảng.</summary>
        public bool TimViTri(int viTri, out int giaTri)
        {
            giaTri = 0;
            if (viTri < 0 || viTri >= _ds.Count) return false;
            giaTri = _ds[viTri];
            return true;
        }

        // ---------- Xóa ----------
        /// <summary>Xóa mọi phần tử có giá trị này, trả về số phần tử đã xóa.</summary>
        public int XoaGiaTri(int giaTri) { return _ds.RemoveAll(x => x == giaTri); }

        public bool XoaViTri(int viTri)
        {
            if (viTri < 0 || viTri >= _ds.Count) return false;
            _ds.RemoveAt(viTri);
            return true;
        }

        // ---------- Thêm ----------
        /// <summary>Thêm giá trị tại vị trí chỉ định (0..SoPhanTu).</summary>
        public bool ThemTaiViTri(int giaTri, int viTri)
        {
            if (viTri < 0 || viTri > _ds.Count) return false;
            _ds.Insert(viTri, giaTri);
            return true;
        }

        /// <summary>Thêm vào đúng chỗ để mảng (đang tăng dần) vẫn tăng dần. Trả về vị trí đã chèn.</summary>
        public int ThemGiuThuTuTang(int giaTri)
        {
            int i = 0;
            while (i < _ds.Count && _ds[i] <= giaTri) i++;
            _ds.Insert(i, giaTri);
            return i;
        }

        // ---------- Tổng ----------
        public long TongMang()
        {
            long t = 0;
            foreach (int x in _ds) t += x;
            return t;
        }

        public long TongChan()
        {
            long t = 0;
            foreach (int x in _ds) if (x % 2 == 0) t += x;
            return t;
        }

        public long TongLe()
        {
            long t = 0;
            foreach (int x in _ds) if (x % 2 != 0) t += x;
            return t;
        }

        // ---------- Lớn nhất / nhỏ nhất (mảng không được rỗng) ----------
        public int GiaTriLonNhat()
        {
            int max = _ds[0];
            foreach (int x in _ds) if (x > max) max = x;
            return max;
        }

        public int GiaTriNhoNhat()
        {
            int min = _ds[0];
            foreach (int x in _ds) if (x < min) min = x;
            return min;
        }

        // ---------- Thay thế ----------
        /// <summary>Thay mọi phần tử bằng giá trị cũ thành giá trị mới, trả về số phần tử đã thay.</summary>
        public int ThayTheGiaTri(int giaTriCu, int giaTriMoi)
        {
            int dem = 0;
            for (int i = 0; i < _ds.Count; i++)
            {
                if (_ds[i] == giaTriCu) { _ds[i] = giaTriMoi; dem++; }
            }
            return dem;
        }

        public bool ThayTheViTri(int viTri, int giaTriMoi)
        {
            if (viTri < 0 || viTri >= _ds.Count) return false;
            _ds[viTri] = giaTriMoi;
            return true;
        }

        // Chuỗi các phần tử cách nhau bằng dấu cách, để hiển thị lên form
        public override string ToString() { return string.Join(" ", _ds); }
    }
}
