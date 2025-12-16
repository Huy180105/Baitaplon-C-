using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitaplon
{
    public partial class Giaodienchinh : Form
    {
        // --- KHAI BÁO BIẾN DÙNG CHUNG ---

        // 1. Màu sắc
        Color normalColor = Color.FromArgb(72, 89, 132); // Màu xanh tím than (Mặc định)
        Color activeColor = Color.FromArgb(255, 107, 53); // Màu cam (Khi đang chọn)

        // 2. Biến lưu tên người dùng
        private string _tenNguoiDung;

        // --- CONSTRUCTOR (HÀM KHỞI TẠO) ---

        // Constructor 1: Mặc định (Bắt buộc phải có để Designer chạy)
        public Giaodienchinh()
        {
            InitializeComponent();
        }

        // Constructor 2: Nhận tên từ Form Đăng nhập
        // ": this()" nghĩa là nó sẽ chạy Constructor 1 trước để vẽ giao diện, sau đó mới chạy lệnh trong này
        public Giaodienchinh(string tenNguoiDung) : this()
        {
            _tenNguoiDung = tenNguoiDung;
        }

        // 1. MENU QUẢN LÝ CƠ SỞ (Panel + 4 Nút con)
        bool isCoSoCollapsed = true;

        private void MenuTransition_Tick(object sender, EventArgs e)
        {
            // Chiều cao nút cha (45) + 4 nút con (4*40) = 205
            int chieuCaoToiDa = 205;
            int chieuCaoToiThieu = 45;

            if (isCoSoCollapsed) // MỞ RA
            {
                pnlCoSoContainer.Height += 10;
                if (pnlCoSoContainer.Height >= chieuCaoToiDa)
                {
                    pnlCoSoContainer.Height = chieuCaoToiDa;
                    MenuTransition.Stop();
                    isCoSoCollapsed = false;
                }
            }
            else // ĐÓNG LẠI
            {
                pnlCoSoContainer.Height -= 10;
                if (pnlCoSoContainer.Height <= chieuCaoToiThieu)
                {
                    pnlCoSoContainer.Height = chieuCaoToiThieu;
                    MenuTransition.Stop();
                    isCoSoCollapsed = true;

                    // Đóng xong -> Trả về màu cũ
                    btnQuanLyCoSo.FillColor = normalColor;
                }
            }
        }

        private void btnQuanLyCoSo_Click(object sender, EventArgs e)
        {
            // Bắt đầu mở -> Đổi màu CAM
            if (isCoSoCollapsed)
            {
                btnQuanLyCoSo.FillColor = activeColor;
            }
            MenuTransition.Start();
        }

        // 2. MENU QUẢN LÝ KHÁCH VÀ HỢP ĐỒNG
        bool isKhachCollapsed = true;

        private void KhachTransition_Tick(object sender, EventArgs e)
        {
            int chieuCaoToiDa = 180; // Tùy chỉnh theo số nút con thực tế
            int chieuCaoToiThieu = 45;

            if (isKhachCollapsed)
            {
                pnlKhachContainer.Height += 10;
                if (pnlKhachContainer.Height >= chieuCaoToiDa)
                {
                    pnlKhachContainer.Height = chieuCaoToiDa;
                    KhachTransition.Stop();
                    isKhachCollapsed = false;
                }
            }
            else
            {
                pnlKhachContainer.Height -= 10;
                if (pnlKhachContainer.Height <= chieuCaoToiThieu)
                {
                    pnlKhachContainer.Height = chieuCaoToiThieu;
                    KhachTransition.Stop();
                    isKhachCollapsed = true;

                    // Trả về màu cũ
                    btnQuanLyKhach.FillColor = normalColor;
                }
            }
        }

        private void btnQuanLyKhach_Click(object sender, EventArgs e)
        {
            if (isKhachCollapsed)
            {
                btnQuanLyKhach.FillColor = activeColor;
            }
            KhachTransition.Start();
        }

        // 3. MENU BÁO CÁO VÀ HỆ THỐNG
        bool isBaoCaoCollapsed = true;

        private void BaoCaoTransition_Tick(object sender, EventArgs e)
        {
            int chieuCaoToiDa = 180;
            int chieuCaoToiThieu = 45;

            if (isBaoCaoCollapsed)
            {
                pnlBaoCaoContainer.Height += 10;
                if (pnlBaoCaoContainer.Height >= chieuCaoToiDa)
                {
                    pnlBaoCaoContainer.Height = chieuCaoToiDa;
                    BaoCaoTransition.Stop();
                    isBaoCaoCollapsed = false;
                }
            }
            else
            {
                pnlBaoCaoContainer.Height -= 10;
                if (pnlBaoCaoContainer.Height <= chieuCaoToiThieu)
                {
                    pnlBaoCaoContainer.Height = chieuCaoToiThieu;
                    BaoCaoTransition.Stop();
                    isBaoCaoCollapsed = true;

                    // Trả về màu cũ
                    btnBaoCao.FillColor = normalColor;
                }
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            if (isBaoCaoCollapsed)
            {
                btnBaoCao.FillColor = activeColor;
            }
            BaoCaoTransition.Start();
        }

        // 4. MENU CÀI ĐẶT
        bool isCaiDatCollapsed = true;

        private void CaiDatTransition_Tick(object sender, EventArgs e)
        {
            int chieuCaoToiDa = 95; // 45 (Cha) + 45 (Con) + Padding
            int chieuCaoToiThieu = 45;

            if (isCaiDatCollapsed)
            {
                pnlCaiDatContainer.Height += 10;
                if (pnlCaiDatContainer.Height >= chieuCaoToiDa)
                {
                    pnlCaiDatContainer.Height = chieuCaoToiDa;
                    CaiDatTransition.Stop();
                    isCaiDatCollapsed = false;
                }
            }
            else
            {
                pnlCaiDatContainer.Height -= 10;
                if (pnlCaiDatContainer.Height <= chieuCaoToiThieu)
                {
                    pnlCaiDatContainer.Height = chieuCaoToiThieu;
                    CaiDatTransition.Stop();
                    isCaiDatCollapsed = true;

                    // Trả về màu cũ
                    btnCaiDat.FillColor = normalColor;
                }
            }
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            if (isCaiDatCollapsed)
            {
                btnCaiDat.FillColor = activeColor;
            }
            CaiDatTransition.Start();
        }
        // SỰ KIỆN LOAD FORM (Khởi tạo ban đầu)
        private void Giaodienchinh_Load(object sender, EventArgs e)
        {
            // 1. Hiển thị tên người dùng (Nếu có)
            // LƯU Ý: Bạn phải tạo Label tên là lblTenNguoiDung bên Design trước
            if (!string.IsNullOrEmpty(_tenNguoiDung))
            {
                // Kiểm tra xem lblTenNguoiDung có null không để tránh lỗi
                if (lblTenNguoiDung != null)
                {
                    lblTenNguoiDung.Text = "Xin chào, " + _tenNguoiDung;
                }
            }

            // 2. Thu gọn tất cả các Menu về trạng thái đóng
            pnlCoSoContainer.Height = 45;
            isCoSoCollapsed = true;

            pnlKhachContainer.Height = 45;
            isKhachCollapsed = true;

            pnlBaoCaoContainer.Height = 45;
            isBaoCaoCollapsed = true;

            pnlCaiDatContainer.Height = 45;
            isCaiDatCollapsed = true;
        }

        
    }
}