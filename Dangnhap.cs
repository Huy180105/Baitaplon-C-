using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // 1. QUAN TRỌNG: Thêm thư viện này để kết nối SQL
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitaplon
{
    public partial class Dangnhap : Form
    {
        // 2. Chuỗi kết nối lấy từ máy của bạn
        string connectionString = @"Data Source=DESKTOP-0IEDA9R;Initial Catalog=Baitaplon-C#;Integrated Security=True";

        // Biến này để lưu tên người dùng, sau này Form Chính có thể lấy để hiển thị "Xin chào..."
        public static string NguoiDungHienTai = "";

        public Dangnhap()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút Đăng nhập (button1)
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã nhập đủ chưa (Giả sử textBox1 là User, textBox2 là Pass)
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo kết nối tới SQL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Câu lệnh SQL kiểm tra tài khoản
                    // Lấy ra Hoten nếu tìm thấy Tendangnhap và Matkhau khớp
                    string sql = "SELECT Hoten FROM TaiKhoan WHERE Tendangnhap = @tk AND Matkhau = @mk";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Truyền tham số vào để tránh lỗi và bảo mật hơn
                    cmd.Parameters.AddWithValue("@tk", txtUser.Text.Trim()); // Trim() để xóa khoảng trắng thừa
                    cmd.Parameters.AddWithValue("@mk", txtPass.Text.Trim());

                    // Thực thi và lấy kết quả đầu tiên (Họ tên)
                    object ketQua = cmd.ExecuteScalar();

                    if (ketQua != null)
                    {
                        // Đăng nhập thành công
                        string tenHienThi = ketQua.ToString(); // Lấy tên người dùng từ CSDL

                        MessageBox.Show("Đăng nhập thành công! Xin chào: " + tenHienThi, "Thông báo");

                        // --- SỬA ĐOẠN NÀY ĐỂ MỞ FORM CHÍNH ---

                        // 1. Ẩn form đăng nhập đi
                        this.Hide();

                        // 2. Khởi tạo Form Chính và truyền tên người dùng vào
                        // (Lưu ý: Bạn phải đảm bảo bên Giaodienchinh.cs đã thêm Constructor nhận tham số như tôi hướng dẫn bài trước)
                        Giaodienchinh frmMain = new Giaodienchinh(tenHienThi);

                        // 3. Hiển thị Form Chính
                        frmMain.ShowDialog();

                        // 4. Khi Form Chính đóng lại thì đóng luôn ứng dụng (để tắt hẳn chương trình)
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Thông báo nếu có lỗi kết nối (ví dụ sai tên server, sai tên DB)
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi hệ thống");
            }
        }

        // Sự kiện khi Form load (có thể để trống hoặc cài đặt gì đó tùy ý)
        private void Form1_Load(object sender, EventArgs e)
        {
            // Ví dụ: Đặt mật khẩu thành dấu * để không bị lộ
            txtPass.UseSystemPasswordChar = true;
            //Chinh size
            CanGiuaPanel();
        }

        // Nếu bạn có nút Thoát (button2 chẳng hạn), hãy thêm sự kiện này
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //1. Khởi tạo form đăng ký
            DangKy frm = new DangKy();
            //2. Hiển thị form đăng ký
            frm.ShowDialog();                
        }

        private void txtPass_IconRightClick(object sender, EventArgs e)
        {
            if (txtPass.UseSystemPasswordChar)
            {
                // Hiện mật khẩu
                txtPass.UseSystemPasswordChar = false;
                txtPass.PasswordChar = '\0'; // '\0' nghĩa là không dùng ký tự thay thế nào

                // (Tùy chọn) Đổi icon thành mắt mở
                // txtMatKhau.IconRight = Properties.Resources.icon_eye_open; 
            }
            else
            {
                // Ẩn mật khẩu
                txtPass.UseSystemPasswordChar = true;
                // (Tùy chọn) Đổi icon thành mắt đóng
                // txtMatKhau.IconRight = Properties.Resources.icon_eye_slash;
            }
        }
        // Hàm tự viết để căn giữa
        private void CanGiuaPanel()
        {
            // Tính toán vị trí mới
            // ClientSize là kích thước vùng làm việc bên trong Form (trừ thanh tiêu đề)
            int x = (this.ClientSize.Width - bangdangnhap.Width) / 2;
            int y = (this.ClientSize.Height - bangdangnhap.Height) / 2;

            // Gán vị trí mới cho Panel
            bangdangnhap.Location = new Point(x, y);
        }

        private void Dangnhap_Resize(object sender, EventArgs e)
        {
            CanGiuaPanel();
        }
    }

}