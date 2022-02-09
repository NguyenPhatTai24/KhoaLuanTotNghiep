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
    public partial class In : Form
    {
        public In()
        {
            InitializeComponent();
        }
        public DataTable table;
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát giao diện ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                table.Clear();
                this.Close();
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void In_Load(object sender, EventArgs e)
        {
            CrysBarcode ba = new CrysBarcode();
            ba.Database.Tables["Table"].SetDataSource(table);
            crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.ReportSource = ba;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
