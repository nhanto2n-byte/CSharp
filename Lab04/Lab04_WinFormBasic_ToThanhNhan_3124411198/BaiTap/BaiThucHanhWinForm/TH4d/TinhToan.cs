namespace BaiThucHanhWinForm.TH4d
{
    /// <summary>
    /// Class TinhToan (theo đề 4d, bài mẫu 1): lưu 2 số a, b và các phương thức tính toán.
    /// Có thuộc tính (property get/set) và 2 phương thức khởi tạo.
    /// </summary>
    public class TinhToan
    {
        // Trường (field) riêng tư
        private float _a, _b;

        // Property get/set để truy cập an toàn từ bên ngoài
        public float a
        {
            get { return _a; }
            set { _a = value; }
        }

        public float b
        {
            get { return _b; }
            set { _b = value; }
        }

        // Phương thức khởi tạo mặc định: a = b = 0
        public TinhToan()
        {
            a = b = 0;
        }

        // Phương thức khởi tạo có tham số
        public TinhToan(float a, float b)
        {
            _a = a;
            _b = b;
        }

        // Các phương thức tính toán
        public float Cong() { return _a + _b; }
        public float Tru() { return _a - _b; }
        public float Nhan() { return _a * _b; }
        public float Chia() { return _a / _b; }   // nhớ kiểm tra b != 0 trước khi gọi
    }
}
