using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacMauShop
{
    public partial class InHoaDon : Form
    {
        public InHoaDon()
        {
            InitializeComponent();
        }
        public DataTable table24;
        public DataTable table25;
        public DataTable table26;
        private void InHoaDon_Load(object sender, EventArgs e)
        {
            CrysHD hd = new CrysHD();
            hd.Database.Tables["TTHD"].SetDataSource(table25);
            hd.Database.Tables["SANPHAM"].SetDataSource(table24);
            hd.Database.Tables["KHUYENMAI"].SetDataSource(table26);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = hd;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát giao diện ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                table24.Clear();
                table25.Clear();
                this.Close();
            }
        }
    }
}
