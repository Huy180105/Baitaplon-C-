using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baitaplon
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //1. Khoi tao form dang nhap
            Dangnhap dangnhap = new Dangnhap();
            //2. Hien thi form dang nhap
            if (dangnhap.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Giaodienchinh());
            } else {
                Application.Exit();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dangnhap());
        }
    }
}
