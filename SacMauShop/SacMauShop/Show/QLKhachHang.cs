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
    public partial class QLKhachHang : UserControl
    {
        public QLKhachHang()
        {
            InitializeComponent();
        }
        public bool them = false, sua = false;
        public bool DayDu, kytu;
        void load()
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MAKH as N'Mã khách hàng', MALOAIKH as N'Mã loại khách hàng', TENKH as N'Tên khách hàng', DIACHIKH as N'Địa chỉ khách hàng', SDTKH as N'Số điện thoại khách hàng' from KHACHHANG");
            dtgkhachhang.DataSource = dt;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MALOAIKH from LOAIKHACHHANG");
            cbmlkh.DataSource = dt1;
            cbmlkh.DisplayMember = "MALOAIKH";
            cbmlkh.ValueMember = "MALOAIKH";
        }
        void clear()
        {
            txttenkh.Text = "";
            txtmkh.Text = "";
            txtdiachi.Text = "";
            txtsdt.Text = "";
            cbmlkh.Text = "";
        }
        void batkytu()
        {
            kytu = false;
            if (txtmkh.Text.Length > 0 && txttenkh.Text.Length > 2 && txtsdt.Text.Length == 10 && txtdiachi.Text.Length > 2)
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
            if (txtmkh.Text == "" || txttenkh.Text == "" || txtsdt.Text == "" || txtdiachi.Text == "" || cbmlkh.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoaiKhachHang lkh = new LoaiKhachHang();
            lkh.Show();
        }

        private void btthem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtmkh.ReadOnly = false;
            clear();
            them = true;
            sua = false;
            splitContainer1.Panel1Collapsed = false;
            btluu.Enabled = true;
            bthuy.Enabled = true;
            dtxoa.Enabled = false;
            btsua.Enabled = false;
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
            txtmkh.ReadOnly = true;
            load();
        }

        private void dtxoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thieudulieu();
            if (DayDu == false)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa");
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " + string.Concat(cbmkh.Text, txtmkh.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from KHACHHANG where MAKH = '" + string.Concat(cbmkh.Text, txtmkh.Text) + "'");
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

        private void dtgkhachhang_MouseClick(object sender, MouseEventArgs e)
        {
            string[] a = new string[2];
            a[0] = dtgkhachhang.SelectedRows[0].Cells[0].Value.ToString().Substring(0, 3);
            a[1] = dtgkhachhang.SelectedRows[0].Cells[0].Value.ToString().Substring(3);
            if (a[0] == "KHS")
            {
                cbmkh.SelectedIndex = 0;
            }
            else if (a[0] == "KHL")
            {
                cbmkh.SelectedIndex = 1;
            }
            txttenkh.Text = dtgkhachhang.SelectedRows[0].Cells[2].Value.ToString();
            txtdiachi.Text = dtgkhachhang.SelectedRows[0].Cells[3].Value.ToString();
            txtsdt.Text = dtgkhachhang.SelectedRows[0].Cells[4].Value.ToString();
            cbmlkh.Text = dtgkhachhang.SelectedRows[0].Cells[1].Value.ToString();
            txtmkh.Text = a[1];
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*DataTable dt = KetnoiCSDL.ExcuteQuery("select MAKH as N'Mã khách hàng', MALOAIKH as N'Mã loại khách hàng', TENKH as N'Tên khách hàng', DIACHIKH as N'Địa chỉ khách hàng', SDTKH as N'Số điện thoại khách hàng' from KHACHHANG where MAKH = '"+string.Concat(cbmkh.Text, txtmkh.Text)+"'");
            dtgkhachhang.DataSource = dt;*/
        }

        private void btluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MAKH from KHACHHANG where MAKH ='" + string.Concat(cbmkh.Text, txtmkh.Text) + "'");
            if (a == false || sua == true)
            {
                thieudulieu();
                batkytu();
                if (them == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn thêm " + string.Concat(cbmkh.Text, txtmkh.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into KHACHHANG values('" + string.Concat(cbmkh.Text, txtmkh.Text) + "', '" + cbmlkh.Text + "', N'" +txttenkh.Text+ "',N'" +txtdiachi.Text+ "', '" + txtsdt.Text + "')");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else if (sua == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn sửa " + string.Concat(cbmkh.Text, txtmkh.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("update KHACHHANG set MALOAIKH = '" + cbmlkh.Text + "', TENKH = N'" + txttenkh.Text + "', DIACHIKH = N'" + txtdiachi.Text + "', SDTKH = '"+txtsdt.Text+"' where MAKH = '" + string.Concat(cbmkh.Text, txtmkh.Text) + "'");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
                    MessageBox.Show("Vui lòng kiểm tra lại \n Mã khách hàng : 0 ký tự trở lên \n Tên khách hàng : 3 ký tự trở lên \n Số điện thoại: Đúng 10 ký tự \n Địa chỉ : 3 ký tự trở lên");
                }
            }
            else if (a == true)
            {
                MessageBox.Show("Bạn đã nhập thiếu hoặc trùng thông tin !!");
            }
        }

        private void txtgiaban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtmkh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MAKH as N'Mã khách hàng', MALOAIKH as N'Mã loại khách hàng', TENKH as N'Tên khách hàng', DIACHIKH as N'Địa chỉ khách hàng', SDTKH as N'Số điện thoại khách hàng' from KHACHHANG where MAKH = '" + comboBox2.SelectedValue.ToString() + "'") ;
            dtgkhachhang.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kiểm tra lại \n Mã khách hàng : 0 ký tự trở lên \n Tên khách hàng : 3 ký tự trở lên \n Số điện thoại: Đúng 10 ký tự \n Địa chỉ : 3 ký tự trở lên");
        }
        private void QLKhachHang_Load(object sender, EventArgs e)
        {
            btluu.Enabled = false;
            bthuy.Enabled = false;
            splitContainer1.Panel1Collapsed = true;
            load();
            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select MAKH from KHACHHANG");
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "MAKH";
            comboBox2.ValueMember = "MAKH";
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }
    }
}
