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
    public partial class QLNhanSu : UserControl
    {
        public bool them = false, sua = false;
        public bool DayDu,xacnhan,kytu;
        public QLNhanSu()
        {
            InitializeComponent();
        }
        void load()
        {
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MANV from NHANVIEN");
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "MANV";
            comboBox2.ValueMember = "MANV";
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MANV as N'Mã nhân viên', MATKHAU as N'Mật khẩu',HOTENNV as N'Họ tên',CONVERT(varchar, NGAYSINHNV, 103) as N'Ngày sinh',SDTNV as N'Số điện thoại',Email,DIACHINV as N'Địa chỉ',CONVERT(varchar, NGAYTAO, 103) as N'Ngày tạo',HIEULUC as N'Hiệu lực' from NHANVIEN");
            dtgnhansu.DataSource = dt;
        }
        void clear()
        {
            txtmnv.Text = "";
            txtemail.Text = "";
            txtnoio.Text = "";
            txtsdt.Text = "";
            txttennv.Text = "";
            txtmk.Text = "";
        }
        void batkytu()
        {
            kytu = false;
            if (txtemail.Text.Length >= 10 && txtnoio.Text.Length > 2 && txtsdt.Text.Length == 10 && txtmk.Text.Length >= 6 && txttennv.Text.Length > 5)
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
            if (comboBox3.Text == "" || txtmnv.Text == "" || txtmk.Text == "" || txtnoio.Text == "" || txtemail.Text == "" || txtsdt.Text == "" || txttennv.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
        void xacnhana()
        {
            xacnhan = false;
            if (comboBox3.Text == "NV" || comboBox3.Text == "QL")
                xacnhan = true;
            else
                xacnhan = false;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtmnv.ReadOnly = false;
            clear();
            them = true;
            sua = false;
            splitContainer1.Panel1Collapsed = false;
            btluu.Enabled = true;
            bthuy.Enabled = true;
            dtxoa.Enabled = false;
            btsua.Enabled = false;
        }

        private void QLNhanSu_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "@gmail.com";
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
            txtmnv.ReadOnly = true;
        }

        private void dtxoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thieudulieu();
            if (DayDu == false)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " + string.Concat(comboBox3.Text, txtmnv.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from NHANVIEN where MANV = '" + string.Concat(comboBox3.Text, txtmnv.Text) + "'");
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
        private void dtgnhansu_MouseClick_1(object sender, MouseEventArgs e)
        {
            string[] a = new string[2];
            a = dtgnhansu.SelectedRows[0].Cells[5].Value.ToString().Split('@');
            if (a[1] == "gmail.com")
            {
                comboBox1.SelectedIndex = 0;
            }      
            else if(a[1] == "yahoo.com")
            {
                comboBox1.SelectedIndex = 1;
            } 
            else
            {
                comboBox1.SelectedIndex = 2;
            }    
            comboBox3.Text = dtgnhansu.SelectedRows[0].Cells[0].Value.ToString().Substring(0,2);
            txtmnv.Text = dtgnhansu.SelectedRows[0].Cells[0].Value.ToString().Substring(2);
            txtmk.Text = dtgnhansu.SelectedRows[0].Cells[1].Value.ToString();
            txttennv.Text = dtgnhansu.SelectedRows[0].Cells[2].Value.ToString();
            dtpngaysinh.Text = dtgnhansu.SelectedRows[0].Cells[3].Value.ToString();
            txtsdt.Text = dtgnhansu.SelectedRows[0].Cells[4].Value.ToString();
            txtemail.Text = a[0];
            txtnoio.Text = dtgnhansu.SelectedRows[0].Cells[6].Value.ToString();
            if (dtgnhansu.SelectedRows[0].Cells[8].Value.ToString() == "True")
                ckhl.Checked = true;
            else
                ckhl.Checked = false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MANV as N'Mã nhân viên', MATKHAU as N'Mật khẩu',HOTENNV as N'Họ tên',CONVERT(varchar, NGAYSINHNV, 103) as N'Ngày sinh',SDTNV as N'Số điện thoại',Email,DIACHINV as N'Địa chỉ',CONVERT(varchar, NGAYTAO, 103) as N'Ngày tạo',HIEULUC as N'Hiệu lực' from NHANVIEN where MANV like '"+comboBox2.SelectedValue+"'");
            dtgnhansu.DataSource = dt;
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }

        private void txtsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtmk_MouseHover(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhân viên : 5 ký tự trở lên \n Mật khẩu : 6 ký tự trở lên \n Email : 10 ký tự trở lên \n Nơi ở : 3 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự");
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".IndexOf(e.KeyChar) != -1)
                e.Handled = false;
            else
                e.Handled = true; 
        }

        private void txtmnv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MANV from NHANVIEN where MANV ='" + string.Concat(comboBox3.Text, txtmnv.Text) + "'");
            int giatri = 0;
            if (ckhl.Checked == true)
            {
                giatri = 1;
            }
            else
            {
                giatri = 0;
            }
            if (a == false || sua == true)
            {
                thieudulieu();
                xacnhana();
                batkytu();
                if (them == true && DayDu == true && xacnhan == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn thêm " + string.Concat(comboBox3.Text, txtmnv.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into NHANVIEN values('" + String.Concat(comboBox3.Text, txtmnv.Text) + "', '" + txtmk.Text + "', '" + String.Format("{0:MM/dd/yyyy}", System.DateTime.Now) + "'," + giatri + ", N'" + txttennv.Text + "','" + String.Format("{0:MM/dd/yyyy}", dtpngaysinh.Value) + "','" + txtsdt.Text + "',N'" + txtnoio.Text + "','" + string.Concat(txtemail.Text, comboBox1.Text) + "')");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else if (sua == true && DayDu == true && xacnhan == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn sửa " + string.Concat(comboBox3.Text, txtmnv.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("update NHANVIEN set MATKHAU = '" + txtmk.Text + "', HIEULUC = " + giatri + ", HOTENNV = N'" + txttennv.Text + "', NGAYSINHNV = '" + String.Format("{0:MM/dd/yyyy}", dtpngaysinh.Value) + "', SDTNV = '" + txtsdt.Text + "', DIACHINV = N'" + txtnoio.Text + "',EMAIL = '" + string.Concat(txtemail.Text, comboBox1.Text) + "' where MANV = '" + string.Concat(comboBox3.Text, txtmnv.Text) + "'");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
                    MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhân viên : 5 ký tự trở lên \n Mật khẩu : 6 ký tự trở lên \n Email : 10 ký tự trở lên \n Nơi ở : 3 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự");

                }
            }
            else if (a == true)
            {
                MessageBox.Show("Bạn đã nhập thiếu hoặc trùng thông tin !!");
            }
        }
    }
}
