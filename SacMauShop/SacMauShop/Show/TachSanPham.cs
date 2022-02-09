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
    public partial class TachSanPham : Form
    {
        public TachSanPham()
        {
            InitializeComponent();
        }

        private void TachSanPham_Load(object sender, EventArgs e)
        {
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG where Right(MASP,2) <> '-t' and SLTON > 0");
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "MASP";
            comboBox1.ValueMember = "MASP";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox1.Text.Substring(0, 6) + "-t";
            DataTable dt101 = KetnoiCSDL.ExcuteQuery("select MASP,TENSP,GIABAN,BANHUTANG from MATHANG where MASP = '" + textBox2.Text + "'");
            if(dt101.Rows.Count > 0)
            {
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                comboBox2.Enabled = false;
                textBox4.Text = dt101.Rows[0][1].ToString();
                textBox5.Text = dt101.Rows[0][2].ToString();
                comboBox2.Text = dt101.Rows[0][3].ToString();
            }
            else
            {
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                comboBox2.Enabled = true;
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox2.Text = "";
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt10 = KetnoiCSDL.ExcuteQuery("select SLTON,MASP from MATHANG where MASP = '" + comboBox1.SelectedValue.ToString() + "'");
            if (int.Parse(textBox1.Text) > int.Parse(dt10.Rows[0][0].ToString()) || comboBox1.SelectedValue.ToString() == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Bạn chưa đủ dữ liệu hoặc số lượng tách quá lớn");
            }    
            else
            {
                DataTable dt101 = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG where MASP = '" + textBox2.Text + "'");
                if (dt101.Rows.Count > 0)
                 {
                     KetnoiCSDL.ExcuteNonQuery("update MATHANG set SLTON = SLTON - " + int.Parse(textBox1.Text) + " where MASP = '" + comboBox1.SelectedValue.ToString() + "'");
                     KetnoiCSDL.ExcuteNonQuery("update MATHANG set SLTON = SLTON + " + int.Parse(textBox1.Text) * int.Parse(textBox3.Text) + " where MASP = '" + textBox2.Text + "'");
                     Same.Success();
                 }
                else
                {
                     DataTable dt = KetnoiCSDL.ExcuteQuery("select * from MATHANG where MASP = '" + comboBox1.SelectedValue.ToString() + "'");
                     KetnoiCSDL.ExcuteNonQuery("update MATHANG set SLTON = SLTON - " + int.Parse(textBox1.Text) + " where MASP = '" + comboBox1.SelectedValue.ToString() + "'");
                     KetnoiCSDL.ExcuteNonQuery("insert into MATHANG values('" + textBox2.Text + "', N'" + textBox4.Text + "', " + int.Parse(textBox1.Text) * int.Parse(textBox3.Text) + ",N'" + dt.Rows[0][3].ToString() + "', '" + dt.Rows[0][4].ToString().Substring(0, 10) + "--t" + "'," + int.Parse(textBox5.Text.Replace(",", string.Empty)) + ",N'" + comboBox2.Text + "')");
                     Same.Success();
                 }    
            }    
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(textBox5.Text, System.Globalization.NumberStyles.AllowThousands);
                textBox5.Text = String.Format(culture, "{0:N0}", value);
                textBox5.Select(textBox5.Text.Length, 0);
            }
        }
    }
}
