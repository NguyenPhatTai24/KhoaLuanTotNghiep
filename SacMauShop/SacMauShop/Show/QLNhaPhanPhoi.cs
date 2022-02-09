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
    public partial class QLNhaPhanPhoi : UserControl
    {
        public bool them = false, sua = false;
        public bool DayDu, kytu;
        public QLNhaPhanPhoi()
        {
            InitializeComponent();
        }
        void load()
        {
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MANPP from NHAPHANPHOI");
            comboBox2.DataSource = dt1;
            comboBox2.DisplayMember = "MANPP";
            comboBox2.ValueMember = "MANPP";
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MANPP as N'Mã nhà phân phối', TENNPP as N'Họ tên',DIACHINPP as N'Địa chỉ',SDTNPP as N'Số điện thoại',CHUYENBAN as N'Chuyên bán',STK as N'Số tài khoản',NGANHANG as N'Ngân hàng',CHINHANHNGANHANG as N'Chi nhánh ngân hàng',GHICHU as N'Ghi chú' from NHAPHANPHOI");
            dtgnhaphanphoi.DataSource = dt;
        }
        void clear()
        {
            txtmnpp.Text = "";
            txtchuyenban.Text = "";
            txtnoio.Text = "";
            txtsdt.Text = "";
            txttennpp.Text = "";
            txtsotk.Text = "";
            txtchinhanhnganhang.Text = "";
            txttennganhang.Text = "";
            rbghichu.Text = "";
        }

        private void btthem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtmnpp.ReadOnly = false;
            clear();
            them = true;
            sua = false;
            splitContainer1.Panel1Collapsed = false;
            btluu.Enabled = true;
            bthuy.Enabled = true;
            dtxoa.Enabled = false;
            btsua.Enabled = false;
        }

        private void QLNhaPhanPhoi_Load(object sender, EventArgs e)
        {
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
            txtmnpp.ReadOnly = true;
        }

        private void dtxoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thieudulieu();
            if (DayDu == false)
            {
                MessageBox.Show("Vui lòng chọn nhà phân phối cần xóa");
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " + string.Concat(lblnpp.Text, txtmnpp.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from NHAPHANPHOI where MANPP = '" + string.Concat(lblnpp.Text, txtmnpp.Text) + "'");
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
        private void dtgnhaphanphoi_MouseClick(object sender, MouseEventArgs e)
        {
            txtmnpp.Text = dtgnhaphanphoi.SelectedRows[0].Cells[0].Value.ToString().Substring(3);
            txttennpp.Text = dtgnhaphanphoi.SelectedRows[0].Cells[1].Value.ToString();
            txtnoio.Text = dtgnhaphanphoi.SelectedRows[0].Cells[2].Value.ToString();
            txtsdt.Text = dtgnhaphanphoi.SelectedRows[0].Cells[3].Value.ToString();
            txtchuyenban.Text = dtgnhaphanphoi.SelectedRows[0].Cells[4].Value.ToString();
            txtsotk.Text = dtgnhaphanphoi.SelectedRows[0].Cells[5].Value.ToString();
            txttennganhang.Text = dtgnhaphanphoi.SelectedRows[0].Cells[6].Value.ToString();
            txtchinhanhnganhang.Text = dtgnhaphanphoi.SelectedRows[0].Cells[7].Value.ToString();
            rbghichu.Text = dtgnhaphanphoi.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MANPP as N'Mã nhà phân phối', TENNPP as N'Họ tên',DIACHINPP as N'Địa chỉ',SDTNPP as N'Số điện thoại',CHUYENBAN as N'Chuyên bán',STK as N'Số tài khoản',NGANHANG as N'Ngân hàng',CHINHANHNGANHANG as N'Chi nhánh ngân hàng',GHICHU as N'Ghi chú' from NHAPHANPHOI where MANPP like '"+string.Concat(lblnpp.Text,txtmnpp.Text)+"'");
            dtgnhaphanphoi.DataSource = dt;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }

        private void txtsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MANPP from NHAPHANPHOI where MANPP ='" + string.Concat(lblnpp.Text, txtmnpp.Text) + "'");
            if (a == false || sua == true)
            {
                thieudulieu();
                batkytu();
                if (them == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn thêm " + string.Concat(lblnpp.Text, txtmnpp.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into NHAPHANPHOI values('" + String.Concat(lblnpp.Text, txtmnpp.Text) + "', N'" + txttennpp.Text + "', N'" + txtnoio.Text + "','" + txtsdt.Text + "', N'" +txtchuyenban.Text + "','" + txtsotk.Text + "',N'" + txttennganhang.Text + "',N'" + txtchinhanhnganhang.Text + "',N'" + rbghichu.Text + "')");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else if (sua == true && DayDu == true && kytu == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn sửa " + string.Concat(lblnpp.Text, txtmnpp.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("update NHAPHANPHOI set TENNPP = N'" + txttennpp.Text + "', DIACHINPP = N'" + txtnoio.Text + "', SDTNPP = '" + txtsdt.Text + "', CHUYENBAN = N'" + txtchuyenban.Text + "', STK = '" + txtsotk.Text + "', NGANHANG = N'" + txttennganhang.Text + "',CHINHANHNGANHANG = N'" + txtchinhanhnganhang.Text+ "',GHICHU = '" + rbghichu.Text + "' where MANPP = '" + string.Concat(lblnpp.Text, txtmnpp.Text) + "'");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
                    MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhà phân phối : 5 ký tự trở lên \n Chuyên bán : 1 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự \n Nơi ở : 3 ký tự trở lên \n Tên ngân hàng : 5 ký tự trở lên \n Chi nhánh ngân hàng : 5 ký tự trở lên \n Số tài khoản : 5 ký tự trở lên");

                }
            }
            else if (a == true)
            {
                MessageBox.Show("Bạn đã nhập thiếu hoặc trùng thông tin !!");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng kiểm tra lại \n Tên nhà phân phối : 5 ký tự trở lên \n Chuyên bán : 1 ký tự trở lên \n Số điện thoại : Đúng 10 ký tự \n Nơi ở : 3 ký tự trở lên \n Tên ngân hàng : 5 ký tự trở lên \n Chi nhánh ngân hàng : 5 ký tự trở lên \n Số tài khoản : 5 ký tự trở lên");
        }

        void batkytu()
        {
            kytu = false;
            if (txtchuyenban.Text.Length > 2 && txtnoio.Text.Length > 2 && txtsdt.Text.Length == 10 && txttennpp.Text.Length > 4 && txttennganhang.Text.Length > 4 && txtchinhanhnganhang.Text.Length > 4 && txtsotk.Text.Length > 4)
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
            if (txtmnpp.Text == "" || txttennganhang.Text == "" || txtnoio.Text == "" || txtchuyenban.Text == "" || txtsdt.Text == "" || txttennpp.Text == "" || txtsotk.Text == "" || txtchinhanhnganhang.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
    }
}
