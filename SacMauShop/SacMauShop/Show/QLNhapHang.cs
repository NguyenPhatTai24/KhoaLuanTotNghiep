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
    public partial class QLNhapHang : UserControl
    {
        public QLNhapHang()
        {
            InitializeComponent();
        }
        public string tentk;
        public bool DayDu, kytu,DayDu1,kytu1;
        void batkytu()
        {
            kytu = false;
            if (txtsln.Text.Length > 0 && txtdongia.Text.Length > 3)
            {
                kytu = true;
            }
            else
            {
                kytu = false;
            }
        }
        void clear1()
        {
            txtmn.Text = "";
            txtcpvc.Text = "";
            rgghichu.Text = "";
        }
        void thieudulieu1()
        {
            DayDu1 = false;
            if (txtmn.Text == "" || txtcpvc.Text == "")
                DayDu1 = false;
            else
                DayDu1 = true;
        }
        void thieudulieu()
        {
            DayDu = false;
            if (txtsln.Text == "" || txtdongia.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtdongia.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtdongia.Text, System.Globalization.NumberStyles.AllowThousands);
                txtdongia.Text = String.Format(culture, "{0:N0}", value);
                txtdongia.Select(txtdongia.Text.Length, 0);
            }
        }
        void load1()
        {
            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select MANHAPHANG from DOTNHAPHANG");
            cbmaphieunhap.DataSource = dt2;
            cbmaphieunhap.DisplayMember = "MANHAPHANG";
            cbmaphieunhap.ValueMember = "MANHAPHANG";

            DataTable dt = KetnoiCSDL.ExcuteQuery("select * from DOTNHAPHANG");
            dtgnhaphang1.DataSource = dt;
        }
        private void QLNhapHang_Load(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG");
            cbmsp.DataSource = dt;
            cbmsp.DisplayMember = "MASP";
            cbmsp.ValueMember = "MASP";

            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MANPP from NHAPHANPHOI");
            cbmnpp.DataSource = dt1;
            cbmnpp.DisplayMember = "MANPP";
            cbmnpp.ValueMember = "MANPP";
            load1();
            
        }
        public int i = 0;

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int t = 0;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select top 1 Convert(int,SUBSTRING(MANHAPHANG,3,LEN(MANHAPHANG))) from DOTNHAPHANG order by Convert(int,SUBSTRING(MANHAPHANG,3,LEN(MANHAPHANG))) desc");
            if (dt1.Rows.Count > 0)
            {
                t = int.Parse(dt1.Rows[0][0].ToString());
                t = t + 1;
                txtmn.Text = t.ToString();
            }
            else
            {
                txtmn.Text = t.ToString();
            }    
            MessageBox.Show("Mời bạn qua phiếu nhập hàng");
            int tong = 0;
            for (int a = 0; a < dtgctnh.Rows.Count; a++)
            {
                dtgpnh.Rows.Add(dtgctnh.Rows[a].Cells[0].Value.ToString(), dtgctnh.Rows[a].Cells[1].Value.ToString(), dtgctnh.Rows[a].Cells[2].Value.ToString()); 
            }
            for (int j = 0; j < dtgpnh.Rows.Count; j++)
            {
                tong += (int.Parse(dtgpnh.Rows[j].Cells[2].Value.ToString().Replace(",", string.Empty)) * int.Parse(dtgpnh.Rows[j].Cells[1].Value.ToString()));
            }
            txttongtien.Text = tong.ToString();

        }
        public string vitri;
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dtgctnh.SelectedRows)
            {
                dtgctnh.Rows.RemoveAt(item.Index);
            }
        }

        private void txttongtien_TextChanged(object sender, EventArgs e)
        {
            if (txttongtien.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txttongtien.Text, System.Globalization.NumberStyles.AllowThousands);
                txttongtien.Text = String.Format(culture, "{0:N0}", value);
                txttongtien.Select(txttongtien.Text.Length, 0);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int tongtien = 0;
            thieudulieu1();
            bool a = false;
            a = KetnoiCSDL.ExcuteQueryReader("select MANHAPHANG from DOTNHAPHANG where  MANHAPHANG = '" + string.Concat(label10.Text, txtmn.Text) + "'");
            if (DayDu == true && kytu == true && dtgpnh.Rows.Count > 0 && a == false)
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thêm " + string.Concat(label10.Text, txtmn.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    tongtien = int.Parse(txtcpvc.Text.Replace(",", string.Empty)) + int.Parse(txttongtien.Text.Replace(",", string.Empty));
                    KetnoiCSDL.ExcuteNonQuery("insert into DOTNHAPHANG values('" +string.Concat(label10.Text,txtmn.Text)+ "', '" +cbmnpp.SelectedValue.ToString()+ "','"+tentk+"','" + String.Format("{0:MM/dd/yyyy}", dtpngayxuat.Value) + "', " + int.Parse(txtcpvc.Text.Replace(",", string.Empty)) + ",N'" + rgghichu.Text + "'," + tongtien + ")");
                    Same.Success();
                    for (int f = 0; f < dtgpnh.Rows.Count; f++)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into CHITIETNHAPHANG values('" + string.Concat(label10.Text, txtmn.Text) + "', '" + dtgpnh.Rows[f].Cells[0].Value.ToString() + "',"+int.Parse(dtgpnh.Rows[f].Cells[1].Value.ToString())+","+ int.Parse(dtgpnh.Rows[f].Cells[2].Value.ToString().Replace(",", string.Empty))+")");
                        KetnoiCSDL.ExcuteNonQuery("update MATHANG set SLTON = SLTON + " + int.Parse(dtgpnh.Rows[f].Cells[1].Value.ToString()) + " where MASP = '" + dtgpnh.Rows[f].Cells[0].Value.ToString() + "'");
                    }
                    clear1();
                    load1();
                    dtgctnh.Rows.Clear();
                    dtgpnh.Rows.Clear();
                }
            }
            else
            {
                MessageBox.Show("Bạn nhập sai hoặc thiếu ký tự hoặc chưa có sản phẩm hoặc trùng mã nhập hàng");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbmnpp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select TENNPP from NHAPHANPHOI where MANPP = '" + cbmnpp.SelectedValue.ToString() + "'");
            if (dt1.Rows.Count > 0)
            {
                txttennpp.Text = dt1.Rows[0][0].ToString();
            }
        }

        private void cbmaphieunhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select * from CHITIETNHAPHANG where MANHAPHANG = '"+cbmaphieunhap.SelectedValue.ToString()+"'");
            dtgnhaphang1.DataSource = dt;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn xóa tất cả dữ liệu của đợt nhập " + dtgnhaphang1.SelectedRows[0].Cells[0].Value.ToString() +" ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                KetnoiCSDL.ExcuteNonQuery("delete from CHITIETNHAPHANG where MANHAPHANG = '"+dtgnhaphang1.SelectedRows[0].Cells[0].Value.ToString()+"'");
                KetnoiCSDL.ExcuteNonQuery("delete from DOTNHAPHANG where MANHAPHANG = '" + dtgnhaphang1.SelectedRows[0].Cells[0].Value.ToString() + "'");
                Same.Success();
            }
            load1();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select * from DOTNHAPHANG");
            dtgnhaphang1.DataSource = dt;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (txtcpvc.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtcpvc.Text, System.Globalization.NumberStyles.AllowThousands);
                txtcpvc.Text = String.Format(culture, "{0:N0}", value);
                txtcpvc.Select(txtcpvc.Text.Length, 0);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int a = 0;
            bool bien = false;
            a = int.Parse(txtsln.Text);
            thieudulieu();
            batkytu();
            if(DayDu == true && kytu == true && txtsln.Text != "0")
            {
                DataTable dt = KetnoiCSDL.ExcuteQuery("select TENSP from MATHANG where MASP = '"+cbmsp.SelectedValue.ToString()+"'");
                DialogResult tb = MessageBox.Show("Bạn muốn thêm " + cbmsp.SelectedValue.ToString() + " với tên là "+dt.Rows[0][0].ToString()+" ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    for (int i = 0; i < dtgctnh.Rows.Count; i++)
                    {
                        if (dtgctnh.Rows[i].Cells[0].Value.ToString() == cbmsp.SelectedValue.ToString())
                        {
                            dtgctnh.Rows[i].Cells[1].Value = a + int.Parse(dtgctnh.Rows[i].Cells[1].Value.ToString());

                            bien = true;
                        }
                    }
                    if (bien == false)
                    {
                        dtgctnh.Rows.Add(cbmsp.SelectedValue.ToString(), txtsln.Text, txtdongia.Text);
                    }
                }            
            }  
            else
            {
                MessageBox.Show("Bạn nhập sai hoặc thiếu ký tự");
                MessageBox.Show("Quy tắc nhập : \n Số lượng nhập : 0 ký tự trở lên \n Đơn giá nhập : 4 ký tự trở lên");
            }
        }
    }
}
