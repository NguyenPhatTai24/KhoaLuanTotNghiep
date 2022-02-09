using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SacMauShop.Class.Connection;
using SacMauShop.Class.Code;

namespace SacMauShop
{
    public partial class QuenMatKhau : DevExpress.XtraEditors.XtraForm
    {
        public int bienxacthuc = 0;
        public string TenTK1 = "";
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát giao diện ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                DangNhap dn = new DangNhap();
                dn.Show();
                this.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = KetnoiCSDL.ExcuteQuery("select EMAIL from NHANVIEN where MANV = '" + txttdn.Text + "'");
            if (txttdn.Text == "" || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có tên đăng nhập này !!!");
            }
            else
            {
                XacThuc xt = new XacThuc();
                xt.TenTK = dt.Rows[0][0].ToString();
                xt.TenTK1 = txttdn.Text;
                xt.Show();
                this.Hide();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (bienxacthuc == 0)
            {
                MessageBox.Show("Bạn chưa xác thực tài khoản");
            }
            else
            {
                KetnoiCSDL.ExcuteNonQuery("update NHANVIEN set MATKHAU = '" + txtmkm.Text + "' where MANV = '" + txttdn.Text + "'");
                Same.Success();
                DangNhap f1 = new DangNhap();
                f1.Show();
                this.Close();
            }
        }

        private void QuenMatKhau_Load(object sender, EventArgs e)
        {
            if (bienxacthuc == 0)
            {
                txttdn.Enabled = true;
                txtmkm.Enabled = false;
            }
            else if (bienxacthuc == 1)
            {
                txttdn.Enabled = false;
                txtmkm.Enabled = true;
                simpleButton1.Enabled = false;
                txttdn.Text = TenTK1;
            }
        }
    }
}