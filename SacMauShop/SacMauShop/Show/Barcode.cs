using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SacMauShop.Class.Code;
using SacMauShop.Class.Connection;

namespace SacMauShop
{
    public partial class Barcode : Form
    {
        public Barcode()
        {
            InitializeComponent();
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
                this.Close();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int q = 0;
            for(int i = 0;i<dtgbarcode.Rows.Count;i++)
            {
                if(dtgbarcode.Rows[i].Cells[0].Value != null && dtgbarcode.Rows[i].Cells["checkchon"].Value.ToString() == "True")
                {
                    q++;
                }    
            }
            if (q != 0)
            {
                DataTable table = new DataTable();
                table.Clear();
                table.Columns.Add("MaSP", typeof(string));
                table.Columns.Add("NhanDan", typeof(byte[]));
                table.Columns.Add("Barcode", typeof(string));
                table.Columns.Add("Gia", typeof(string));
                for (int i = 0; i < dtgbarcode.Rows.Count; i++)
                {
                    if (dtgbarcode.Rows[i].Cells[0].Value != null && dtgbarcode.Rows[i].Cells["checkchon"].Value.ToString() == "True")
                    {
                        for (int j = 1; j <= Convert.ToInt32(dtgbarcode.Rows[i].Cells[3].Value.ToString()); j++)
                        {
                            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                            Image img = barcode.Draw(dtgbarcode.Rows[i].Cells[5].Value.ToString(), 100);
                            MemoryStream ms = new MemoryStream();
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            table.Rows.Add(dtgbarcode.Rows[i].Cells[1].Value.ToString(), ms.ToArray(), dtgbarcode.Rows[i].Cells[5].Value.ToString(), string.Format(new CultureInfo("vi-VN"), "{0:N0}", dtgbarcode.Rows[i].Cells[6].Value.ToString()));
                        }
                    }
                }
                In eaa = new In();
                eaa.table = table;
                eaa.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần in nhãn dán !!!");
            }    
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgbarcode.Rows.Count; i++)
            {
                dtgbarcode.Rows[i].Cells["checkchon"].Value = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtgbarcode.Rows.Count; i++)
            {
                dtgbarcode.Rows[i].Cells["checkchon"].Value = true;
            }
        }

        private void Barcode_Load(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG");
            dtgbarcode.DataSource = dt;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select Distinct(MANHAPHANG) from CHITIETNHAPHANG");
            cbmanhap.DataSource = dt1;
            cbmanhap.DisplayMember = "MANHAPHANG";
            cbmanhap.ValueMember = "MANHAPHANG";
            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG");
            cbmasp.DataSource = dt2;
            cbmasp.DisplayMember = "MASP";
            cbmasp.ValueMember = "MASP";
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select TENSP from MATHANG");
            cbtensp.DataSource = dt3;
            cbtensp.DisplayMember = "TENSP";
            cbtensp.ValueMember = "TENSP";
            cbmanhap.Text = "Chọn";
            cbtensp.Text = "Chọn";
            cbmasp.Text = "Chọn";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            cbmanhap.Text = "Chọn";
            cbtensp.Text = "Chọn";
            cbmasp.Text = "Chọn";
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG");
            dtgbarcode.DataSource = dt;
        }

        private void cbmanhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbtensp.Text = "Chọn";
            cbmasp.Text = "Chọn";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (cbmanhap.Text != "Chọn" && cbmasp.Text == "Chọn" && cbtensp.Text == "Chọn")
            {
                DataTable dt = KetnoiCSDL.ExcuteQuery("select MATHANG.MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG, CHITIETNHAPHANG where MATHANG.MASP = CHITIETNHAPHANG.MASP and MANHAPHANG = '" + cbmanhap.SelectedValue.ToString() + "'");
                dtgbarcode.DataSource = dt;
            }
            else if(cbmasp.Text != "Chọn" && cbmanhap.Text == "Chọn" && cbtensp.Text == "Chọn")
            {
                DataTable dt = KetnoiCSDL.ExcuteQuery("select " +
                    "MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where MASP = '"+cbmasp.SelectedValue.ToString()+"'");
                dtgbarcode.DataSource = dt;
            }    
            else if(cbtensp.Text != "Chọn" && cbmasp.Text == "Chọn" && cbmanhap.Text == "Chọn")
            {
                DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where TENSP = N'" + cbtensp.SelectedValue.ToString() + "'");
                dtgbarcode.DataSource = dt;
            }    
            else
            {
                MessageBox.Show("Vui lòng chọn đúng mặt hàng bạn cần tìm");
            }    
        }

        private void cbmasp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbtensp.Text = "Chọn";
            cbmanhap.Text = "Chọn";
        }

        private void cbtensp_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbmanhap.Text = "Chọn";
            cbmasp.Text = "Chọn";
        }
    }
}
