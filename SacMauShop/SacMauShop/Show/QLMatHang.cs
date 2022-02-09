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
    public partial class QLMatHang : UserControl
    {
        public bool them = false, sua = false;
        public bool DayDu, kytu;
        public QLMatHang()
        {
            InitializeComponent();
        }
        void load()
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG");
            dtgsanpham.DataSource = dt;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG");
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "MASP";
            comboBox2.ValueMember = "MASP";
        }

        private void btthem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtmsp.ReadOnly = false;
            clear();
            them = true;
            sua = false;
            splitContainer1.Panel1Collapsed = false;
            btluu.Enabled = true;
            bthuy.Enabled = true;
            dtxoa.Enabled = false;
            btsua.Enabled = false;
        }

        private void QLMatHang_Load(object sender, EventArgs e)
        {
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select NHOMSP from MATHANG");
            cbnsp.DataSource = dt1;
            cbnsp.DisplayMember = "NHOMSP";
            cbnsp.ValueMember = "NHOMSP";


            cbbanhutang.Text = "Bán";
            btluu.Enabled = false;
            bthuy.Enabled = false;
            splitContainer1.Panel1Collapsed = true;
            load();
        }

        private void btsua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            sua = true;
            them = false;
            splitContainer1.Panel1Collapsed = false;
            btluu.Enabled = true;
            bthuy.Enabled = true;
            dtxoa.Enabled = false;
            btthem.Enabled = false;
            txtmsp.ReadOnly = true;
        }

        private void dtxoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thieudulieu();
            if (DayDu == false)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần xóa");
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " +txtmsp.Text+ " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from MATHANG where MANV = '" + txtmsp.Text + "'");
                    load();
                    clear();
                    Same.Success();
                }
            }
        }

        private void bthuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btluu.Enabled = false;
            bthuy.Enabled = false;
            btsua.Enabled = true;
            btthem.Enabled = true;
            dtxoa.Enabled = true;
            splitContainer1.Panel1Collapsed = true;
            clear();
        }

        private void dtgnhansu_MouseClick(object sender, MouseEventArgs e)
        {
            /*string a = "";
            a = dtgsanpham.SelectedRows[0].Cells[5].Value.ToString();
            if (a == "Bán")
            {
                cbbanhutang.SelectedIndex = 0;
            }
            else if ( a == "Hư")
            {
                cbbanhutang.SelectedIndex = 1;
            }
            else
            {
                cbbanhutang.SelectedIndex = 2;
            }          
            txtmsp.Text = dtgsanpham.SelectedRows[0].Cells[0].Value.ToString();
            txttensp.Text = dtgsanpham.SelectedRows[0].Cells[1].Value.ToString();
            txtslton.Text = dtgsanpham.SelectedRows[0].Cells[2].Value.ToString();
            cbnsp.Text = dtgsanpham.SelectedRows[0].Cells[3].Value.ToString();
            txtnhandan.Text = dtgsanpham.SelectedRows[0].Cells[4].Value.ToString();
            txtgiaban.Text = dtgsanpham.SelectedRows[0].Cells[5].Value.ToString(); */
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }

        private void txtmk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MASP from MATHANG where MASP ='" + txtmsp.Text + "'");
            if (a == false || sua == true)
            {
                thieudulieu();
                batkytu();
                if (them == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn thêm " + txtmsp.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into MATHANG values('" + txtmsp.Text + "', N'" + txttensp.Text + "', " + int.Parse(txtslton.Text) + ",N'" + cbnsp.Text + "', '" + txtnhandan.Text + "'," + int.Parse(txtgiaban.Text.Replace(",", string.Empty)) + ",N'" + cbbanhutang.Text + "')");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else if (sua == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn sửa " + txtmsp.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("update MATHANG set TENSP = N'" + txttensp.Text + "', SLTON = " + int.Parse(txtslton.Text) + ", NHOMSP = N'" + cbnsp.Text + "', MABC = '" + txtnhandan.Text + "', GIABAN = " + int.Parse(txtgiaban.Text.Replace(",", string.Empty)) + ", BANHUTANG = N'" + cbbanhutang.Text + "' where MASP = '" + txtmsp.Text + "'");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã sản phẩm");
                    //MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhân viên : 5 ký tự trở lên \n Mật khẩu : 6 ký tự trở lên \n Email : 10 ký tự trở lên \n Nơi ở : 3 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự");

                }
            }
            else if (a == true)
            {
                MessageBox.Show("Bạn đã nhập thiếu hoặc trùng thông tin !!");
            }
        }

        void clear()
        {
            txtmsp.Text = "";
            txtnhandan.Text = "";
            txtgiaban.Text = "";
            txttensp.Text = "";
            txtslton.Text = "";
            cbbanhutang.Text = "";
            cbnsp.Text = "";
        }

        private void txtgiaban_TextChanged(object sender, EventArgs e)
        {
            if (txtgiaban.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtgiaban.Text, System.Globalization.NumberStyles.AllowThousands);
                txtgiaban.Text = String.Format(culture, "{0:N0}", value);
                txtgiaban.Select(txtgiaban.Text.Length, 0);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhân viên : 5 ký tự trở lên \n Mật khẩu : 6 ký tự trở lên \n Email : 10 ký tự trở lên \n Nơi ở : 3 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',FORMAT(GIABAN, 'N0','en-US') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where MASP = '" + comboBox2.SelectedValue.ToString() + "'");
            dtgsanpham.DataSource = dt;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Barcode bc = new Barcode();
            bc.Show();
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XuatExcel xe = new XuatExcel();
            xe.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NhapExcel ne = new NhapExcel();
            ne.Show();
        }

        private void cbbanhutang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbanhutang.Text == "Tặng")
            {
                txtgiaban.Text = "0";
                txtgiaban.ReadOnly = true;
            }    
            else
            {
                txtgiaban.ReadOnly = false;
                txtgiaban.Text = "";
            }    
        }

        private void txtgiaban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtslton_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtgsanpham_MouseClick(object sender, MouseEventArgs e)
        {
            string a = "";
            a = dtgsanpham.SelectedRows[0].Cells[6].Value.ToString();
            if (a == "Bán")
            {
                cbbanhutang.SelectedIndex = 0;
            }
            else if ( a == "Hư")
            {
                cbbanhutang.SelectedIndex = 1;
            }
            else
            {
                cbbanhutang.SelectedIndex = 2;
            }          
            txtmsp.Text = dtgsanpham.SelectedRows[0].Cells[0].Value.ToString();
            txttensp.Text = dtgsanpham.SelectedRows[0].Cells[1].Value.ToString();
            txtslton.Text = dtgsanpham.SelectedRows[0].Cells[2].Value.ToString();
            cbnsp.Text = dtgsanpham.SelectedRows[0].Cells[3].Value.ToString();
            txtnhandan.Text = dtgsanpham.SelectedRows[0].Cells[4].Value.ToString();
            txtgiaban.Text = dtgsanpham.SelectedRows[0].Cells[5].Value.ToString(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TachSanPham tsp = new TachSanPham();
            tsp.Show();
        }

        void batkytu()
        {
            kytu = false;
            if (txtmsp.Text.Length == 8 && txttensp.Text.Length > 2 && txtnhandan.Text.Length == 13 && txtgiaban.Text.Length > 0 && txtslton.Text.Length > 0 && cbnsp.Text.Length > 2)
            {
                kytu = true;
            }
            else
            {
                kytu = false;
            }
        }
        void thieudulieu()
        {
            DayDu = false;
            if (txtmsp.Text == "" || txtslton.Text == "" || txtnhandan.Text == "" || cbnsp.Text == "" || txtgiaban.Text == "" || txttensp.Text == "" || cbbanhutang.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
    }
}
