using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SacMauShop
{
    public partial class QuanLy : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        bool a = false;
        QLNhanSu qlns;
        QLNhaPhanPhoi qlnpp;
        QLMatHang qlmh;
        QLKhachHang qlkh;
        QLNhapHang qlnh;
        KhuyenMai km;
        ThongKe tk;
        public QuanLy()
        {
            InitializeComponent();
        }
        public string tentk;
        private void dmk_Click(object sender, EventArgs e)
        {
            a = true;
            DoiMatKhau dmk = new DoiMatKhau();
            dmk.Show();
            this.Hide();
            barHeaderItem1.Caption = tentk;
        }
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {

            if(qlns == null || Demo.Caption == "Demo")
            {
                qlns = new QLNhanSu();
                qlns.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(qlns);
                qlns.BringToFront();
            }    
            else
                qlns.BringToFront();
            Demo.Caption = accordionControlElement3.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (qlnpp == null || Demo.Caption == "Demo")
            {
                qlnpp = new QLNhaPhanPhoi();
                qlnpp.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(qlnpp);
                qlnpp.BringToFront();
            }
            else
                qlnpp.BringToFront();
            Demo.Caption = accordionControlElement8.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            if (qlmh == null || Demo.Caption == "Demo")
            {
                qlmh = new QLMatHang();
                qlmh.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(qlmh);
                qlmh.BringToFront();
            }
            else
                qlmh.BringToFront();
            Demo.Caption = accordionControlElement6.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            if (qlkh == null || Demo.Caption == "Demo")
            {
                qlkh = new QLKhachHang(); ;
                qlkh.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(qlkh);
                qlkh.BringToFront();
            }
            else
                qlkh.BringToFront();
            Demo.Caption = accordionControlElement7.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (qlnh == null || Demo.Caption == "Demo")
            {
                qlnh = new QLNhapHang(); 
                qlnh.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(qlnh);
                qlnh.BringToFront();
            }
            else
                qlnh.BringToFront();
            Demo.Caption = accordionControlElement4.Text;
            barHeaderItem1.Caption = tentk;
            qlnh.tentk = tentk;
        }
        private void QuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (a == true)
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (tb == DialogResult.OK)
                {
                    e.Cancel = false;
                    DangNhap dn = new DangNhap();
                    dn.Show();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            if (km == null || Demo.Caption == "Demo")
            {
                km = new KhuyenMai();
                km.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(km);
                km.BringToFront();
            }
            else
                km.BringToFront();
            Demo.Caption = accordionControlElement9.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            if (tk == null || Demo.Caption == "Demo")
            {
                tk = new ThongKe();
                tk.Dock = DockStyle.Fill;
                LoadFormNe.Controls.Add(tk);
                tk.BringToFront();
            }
            else
                tk.BringToFront();
            Demo.Caption = accordionControlElement2.Text;
            barHeaderItem1.Caption = tentk;
        }

        private void QuanLy_Load(object sender, EventArgs e)
        {
            a = false;
            barHeaderItem1.Caption = tentk;
        }
    }
}
