using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Thư viện kết nối SQL

namespace Baitaplon
{
    public partial class DangKy : Form
    {
        // 1. Chuỗi kết nối (Đã chỉnh theo máy bạn)
        string connectionString = @"Data Source=DESKTOP-0IEDA9R;Initial Catalog=Baitaplon-C#;Integrated Security=True";

        public DangKy()
        {
            InitializeComponent();
        }

        // 2. Sự kiện khi Form hiện lên (Load)
        // Tác dụng: Ẩn mật khẩu thành dấu chấm tròn
        private void DangKy_Load(object sender, EventArgs e)
        {
            try
            {
                if (txtPass != null) txtPass.UseSystemPasswordChar = true;
                if (txtConfirmPass != null) txtConfirmPass.UseSystemPasswordChar = true;
            }
            catch { }
            CanGiuaPanel();
        }

        // 3. Sự kiện bấm nút Đăng ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Bước A: Kiểm tra nhập thiếu
            if (string.IsNullOrEmpty(txtUser.Text) ||
                string.IsNullOrEmpty(txtPass.Text) ||
                string.IsNullOrEmpty(txtConfirmPass.Text) ||
                string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ: Tên đăng nhập, Mật khẩu, Nhập lại MK và Họ tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bước B: Kiểm tra mật khẩu nhập lại
            if (txtPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Bước C: Kiểm tra xem tên đăng nhập đã tồn tại chưa
                    string checkSql = "SELECT COUNT(*) FROM TaiKhoan WHERE Tendangnhap = @tk";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@tk", txtUser.Text.Trim());

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Tên đăng nhập này đã có người dùng. Vui lòng chọn tên khác!", "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Focus(); // Đưa con trỏ chuột về ô User để nhập lại
                        return;
                    }

                    // Bước D: Thêm tài khoản mới vào Database
                    string insertSql = "INSERT INTO TaiKhoan (Tendangnhap, Matkhau, Hoten) VALUES (@tk, @mk, @ten)";
                    SqlCommand cmd = new SqlCommand(insertSql, conn);

                    // Truyền tham số an toàn
                    cmd.Parameters.AddWithValue("@tk", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@mk", txtPass.Text.Trim());
                    cmd.Parameters.AddWithValue("@ten", txtHoTen.Text.Trim());

                    cmd.ExecuteNonQuery(); // Thực thi lệnh Insert

                    MessageBox.Show("Đăng ký thành công! Vui lòng đăng nhập lại.", "Thông báo");

                    // Bước E: Đóng form Đăng ký -> Form Đăng nhập sẽ tự hiện ra
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi hệ thống");
            }
        }

        // 4. Sự kiện nút Thoát (Nếu có nút btnThoat)
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CanGiuaPanel()
        {
            // Tính toán vị trí mới
            // ClientSize là kích thước vùng làm việc bên trong Form (trừ thanh tiêu đề)
            int x = (this.ClientSize.Width - bangdangky.Width) / 2;
            int y = (this.ClientSize.Height - bangdangky.Height) / 2;

            // Gán vị trí mới cho Panel
            bangdangky.Location = new Point(x, y);
        }

        private void DangKy_Resize(object sender, EventArgs e)
        {
            CanGiuaPanel();
        }
    }
}