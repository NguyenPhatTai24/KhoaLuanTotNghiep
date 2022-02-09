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
using SacMauShop.Class.Code;
using SacMauShop.Class.Connection;

namespace SacMauShop
{
    public partial class DangNhap : DevExpress.XtraEditors.XtraForm
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void lbluser_Click(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            if (lbl.Location.X == 50)
            {
                lbl.Font = new Font("Microsoft Sans Serif", 12);
                lbl.Cursor = Cursors.Arrow;
                lbl.Location = new Point(lbl.Location.X - 3, lbl.Location.Y - 25);
                foreach (Control txt in panel2.Controls)
                {
                    if (txt.GetType() == typeof(TextBox) && txt.Name == "txt" + lbl.Name.Remove(0, 3))
                    {
                        txt.Focus();
                    }
                }
            }
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = Color.DodgerBlue;
                }
                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
                {
                    ctrl.ForeColor = Color.DodgerBlue;
                    if (ctrl.Location.X != 47)
                    {
                        ctrl.Font = new Font("Microsoft Sans Serif", 12);
                        ctrl.Cursor = Cursors.Arrow;
                        ctrl.Location = new Point(ctrl.Location.X - 3, ctrl.Location.Y - 25);
                    }
                }
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Panel) && ctrl.Name == "pnl" + txt.Name.Remove(0, 3))
                {
                    ctrl.BackColor = Color.Silver;
                }
                if (ctrl.GetType() == typeof(Label) && ctrl.Name == "lbl" + txt.Name.Remove(0, 3))
                {
                    ctrl.ForeColor = Color.Silver;
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        txt.Clear();
                        ctrl.Font = new Font("Microsoft Sans Serif", 12);
                        ctrl.Cursor = Cursors.IBeam;
                        ctrl.Location = new Point(ctrl.Location.X + 3, ctrl.Location.Y + 25);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (tb == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool a = KetnoiCSDL.ExcuteQueryReader("select MANV , MATKHAU from NHANVIEN where  MANV = '"+txtuser.Text+"' and MATKHAU = '"+txtpassword.Text+"' and HIEULUC = 1");
            if (txtuser.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu");
            }
            else if (a == false)
            {
                txtuser.Clear();
                txtpassword.Clear();
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu hoặc tài khoản đã hết hiệu lực, vui lòng kiểm tra lại !");
            }
            else if (a == true)
            {
                if (txtuser.Text.Substring(0, 2) == "QL")
                {
                    MessageBox.Show("Quản lý " + txtuser.Text);
                    this.Hide();
                    QuanLy ql = new QuanLy();
                    ql.tentk = txtuser.Text;
                    ql.Show();
                }
                else
                {
                    MessageBox.Show("Nhân viên " + txtuser.Text);
                    this.Hide();
                    NhanVien nv = new NhanVien();
                    nv.tentk = txtuser.Text;
                    nv.Show();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            QuenMatKhau qmk = new QuenMatKhau();
            qmk.Show();
            this.Hide();
        }
    }
}