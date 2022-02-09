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
    public partial class DoiMatKhau : DevExpress.XtraEditors.XtraForm
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool a = KetnoiCSDL.ExcuteQueryReader("select * from NHANVIEN where MANV ='" + txttdn.Text + "' and MATKHAU ='" + txtmk.Text + "'");
            if (a == true && txtmkm.Text != "")
            {
                DialogResult tb = MessageBox.Show("Bạn muốn đổi mật khẩu tài khoản " + txttdn.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("update NHANVIEN set MATKHAU = '" + txtmkm.Text + "' where MANV = '" + txttdn.Text + "' and MATKHAU = '" + txtmk.Text + "'");
                    Same.Success();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin !");
            }
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

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            
        }
    }
}