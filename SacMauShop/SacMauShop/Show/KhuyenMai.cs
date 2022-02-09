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
    public partial class KhuyenMai : UserControl
    {
        public bool them = false, sua = false;
        public bool DayDu, kytu,ngaythang;
        public KhuyenMai()
        {
            InitializeComponent();
        }
        void load()
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MADOTKM as N'Mã đợt khuyến mãi',NGAYBATDAUKM as N'Ngày bắt đầu khuyến mãi', NGAYKETTHUCKM as N'Ngày kết thúc khuyến mãi', SOPHANTRAMKM as N'Số phần trăm khuyến mãi' from DOTKHUYENMAI");
            dtgkhuyenmai.DataSource = dt;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MADOTKM from DOTKHUYENMAI");
            cbmakm.DataSource = dt1;
            cbmakm.DisplayMember = "MADOTKM";
            cbmakm.ValueMember = "MADOTKM";
        }
        void kiemtrangaythang()
        {
            if(dtpngaybatdau.Value.Date < DateTime.Now.Date || dtpngayketthuc.Value.Date < dtpngaybatdau.Value.Date)
            {
                ngaythang = false;
            }    
            else
            {
                ngaythang = true;
            }    
        }
        private void btthem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtmadotkm.ReadOnly = false;
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
            txtmadotkm.ReadOnly = true;
            load();
        }

        private void dtxoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            thieudulieu();
            if (DayDu == false)
            {
                MessageBox.Show("Vui lòng chọn đợt khuyến mãi cần xóa");
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " + txtmadotkm.Text+ " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from DOTKHUYENMAI where MADOTKM = '" + txtmadotkm.Text + "'");
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
            txtmadotkm.Text = dtgkhuyenmai.SelectedRows[0].Cells[0].Value.ToString();
            dtpngaybatdau.Text = dtgkhuyenmai.SelectedRows[0].Cells[1].Value.ToString();
            dtpngayketthuc.Text = dtgkhuyenmai.SelectedRows[0].Cells[2].Value.ToString();
            txtsophantram.Text = dtgkhuyenmai.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btluu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MADOTKM from DOTKHUYENMAI where MADOTKM ='" + txtmadotkm.Text + "'");
            if (a == false || sua == true)
            {
                kiemtrangaythang();
                thieudulieu();
                batkytu();
                if (them == true && DayDu == true && kytu == true && ngaythang == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn thêm " + txtmadotkm.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into DOTKHUYENMAI values('" + txtmadotkm.Text + "', '" + String.Format("{0:MM/dd/yyyy}", dtpngaybatdau.Value) + "', '" + String.Format("{0:MM/dd/yyyy}",dtpngayketthuc.Value) + "'," + txtsophantram.Text + ")");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else if (sua == true && DayDu == true && kytu == true && ngaythang == true)
                {
                    DialogResult tb = MessageBox.Show("Bạn muốn sửa " + txtmadotkm.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (tb == DialogResult.OK)
                    {
                        KetnoiCSDL.ExcuteNonQuery("update DOTKHUYENMAI set NGAYBATDAUKM = '" + String.Format("{0:MM/dd/yyyy}", dtpngaybatdau.Value) + "', NGAYKETTHUCKM = '" + String.Format("{0:MM/dd/yyyy}", dtpngayketthuc.Value) + "', SOPHANTRAMKM = " + txtsophantram.Text + " where MADOTKM = '" + txtmadotkm.Text + "'");
                        load();
                        Same.Success();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã đợt khuyến mãi hoặc ngày bắt đầu nhỏ hơn ngày hiện tại hoặc ngày kết thúc nhỏ hơn ngày bắt đầu");
                }
            }
            else if (a == true)
            {
                MessageBox.Show("Bạn đã nhập thiếu hoặc trùng thông tin !!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void KhuyenMai_Load(object sender, EventArgs e)
        {
            btluu.Enabled = false;
            bthuy.Enabled = false;
            splitContainer1.Panel1Collapsed = true;
            load();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mã đợt khuyến mãi : 2 ký tự trở lên \n Ngày bắt đầu : Lớn hơn ngày hiện tại \n Ngày kết thúc: Lớn hơn ngày bắt đầu \n Số phần trăm : tối đa 2 ký tự và không được nhập 0");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MADOTKM as N'Mã đợt khuyến mãi',NGAYBATDAUKM as N'Ngày bắt đầu khuyến mãi', NGAYKETTHUCKM as N'Ngày kết thúc khuyến mãi', SOPHANTRAMKM as N'Số phần trăm khuyến mãi' from DOTKHUYENMAI where MADOTKM = '" + cbmakm.SelectedValue.ToString() + "'");
            dtgkhuyenmai.DataSource = dt;
        }

        void clear()
        {
            txtmadotkm.Text = "";
            txtsophantram.Text = "";
        }
        void batkytu()
        {
            kytu = false;
            if (txtmadotkm.Text.Length > 2)
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
            if (txtmadotkm.Text == "" || txtsophantram.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
    }
}
