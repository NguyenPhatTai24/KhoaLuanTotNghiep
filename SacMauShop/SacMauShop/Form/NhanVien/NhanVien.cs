using DevExpress.XtraEditors;
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
using System.Globalization;

namespace SacMauShop
{
    public partial class NhanVien : DevExpress.XtraEditors.XtraForm
    {
        public string tentk;
        public bool DayDu,daydu1,a =false;
        public NhanVien()
        {
            InitializeComponent();
        }
        void load()
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MASP from MATHANG where BANHUTANG <> N'Hư' and SLTON > 0");
            cbmasp.DataSource = dt;
            cbmasp.DisplayMember = "MASP";
            cbmasp.ValueMember = "MASP";

            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select MADOTKM from DOTKHUYENMAI where NGAYKETTHUCKM >= '"+ String.Format("{0:MM/dd/yyyy}", System.DateTime.Now.Date)+"'");
            cbmadotkm.DataSource = dt1;
            cbmadotkm.DisplayMember = "MADOTKM";
            cbmadotkm.ValueMember = "MADOTKM";

            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select MAKH from KHACHHANG");
            cbmakh.DataSource = dt2;
            cbmakh.DisplayMember = "MAKH";
            cbmakh.ValueMember = "MAKH";

            DataTable dt10 = KetnoiCSDL.ExcuteQuery("select top 1 * from HOADON order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
            dataGridView1.DataSource = dt10;
            DataTable dt11 = KetnoiCSDL.ExcuteQuery("select * from CTHD where MAHD = '" + dt10.Rows[0][0].ToString() + "'");
            dtgctnh.DataSource = dt11;

            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select MABC from MATHANG where BANHUTANG <> N'Hư' and SLTON > 0 ");
            textBox1.DataSource = dt3;
            textBox1.DisplayMember = "MABC";
            textBox1.ValueMember = "MABC";

            sl.Enabled = false;
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            cbmasp.Enabled = false;
            textBox1.Enabled = false;
            barHeaderItem1.Caption = tentk;
            load();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            load();
        }

        private void cbmasp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select * from MATHANG where MASP = '"+cbmasp.SelectedValue.ToString()+"'");
            if (dt3.Rows.Count > 0)
            {
                txttensp.Text = dt3.Rows[0][1].ToString();
                textBox1.Text = dt3.Rows[0][4].ToString();
                txtgia.Text = dt3.Rows[0][5].ToString();
            }
            sl.Enabled = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            txtongtiencuoi.Text = txttongtien.Text;
            cbmadotkm.SelectedIndex = 0;
            cbmakh.SelectedIndex = 0;
            dtgsanphamsapxuat.Rows.Clear();
            int t = 0;
            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select top 1 Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) from HOADON order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
            if (dt1.Rows.Count > 0)
            {
                t = int.Parse(dt1.Rows[0][0].ToString());
                t = t + 1;
                txtmhd.Text = t.ToString();
            }
            else
            {
                txtmhd.Text = t.ToString();
            }
            MessageBox.Show("Mời bạn qua phiếu nhập hàng");
            int tong = 0;
            for (int a = 0; a < dtgsanpham.Rows.Count; a++)
            {
               dtgsanphamsapxuat.Rows.Add(dtgsanpham.Rows[a].Cells[0].Value.ToString(), dtgsanpham.Rows[a].Cells[1].Value.ToString(), dtgsanpham.Rows[a].Cells[2].Value.ToString(), dtgsanpham.Rows[a].Cells[3].Value.ToString(), dtgsanpham.Rows[a].Cells[4].Value.ToString());
            }
            for (int j = 0; j < dtgsanphamsapxuat.Rows.Count; j++)
            {
                tong += (int.Parse(dtgsanphamsapxuat.Rows[j].Cells[4].Value.ToString().Replace(",", string.Empty)) * int.Parse(dtgsanphamsapxuat.Rows[j].Cells[1].Value.ToString()));
            }
            txttongtien.Text = tong.ToString();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            a = true;
            DoiMatKhau dmk = new DoiMatKhau();
            dmk.Show();
            this.Close();
        }
       void thieudulieu()
        {
            DayDu = false;
            if (txttensp.Text == "" || sl.Value == 0)
                DayDu = false;
            else
                DayDu = true;
        }
        void clear()
        {
            txtgia.Text = "";
            textBox1.Text = "";
            txttensp.Text = "";
            sl.Value = 0;
            sl.Enabled = false;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int a = 0;
            bool bien = false;
            a = int.Parse(sl.Text);
            thieudulieu();
            if (DayDu == true)
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thêm " + cbmasp.SelectedValue.ToString() +  "vào hóa đơn ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    for (int i = 0; i < dtgsanpham.Rows.Count; i++)
                    {
                        if (dtgsanpham.Rows[i].Cells[0].Value.ToString() == cbmasp.SelectedValue.ToString())
                        {
                            dtgsanpham.Rows[i].Cells[1].Value = a + int.Parse(dtgsanpham.Rows[i].Cells[1].Value.ToString());
                            bien = true;
                            clear();
                        }
                    }
                    if (bien == false)
                    {
                        dtgsanpham.Rows.Add(cbmasp.SelectedValue.ToString(), sl.Value.ToString(), txttensp.Text,textBox1.Text,txtgia.Text);
                        clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn mã sản phẩm hoặc số lượng đã tới giới hạn kho khi nó bằng 0");
            }
        }

        private void sl_ValueChanged(object sender, EventArgs e)
        {
            bool bien = false;
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select SLTON from MATHANG where MASP = '" + cbmasp.SelectedValue.ToString() + "'");
            if(txtgia.Text.Length > 0 && dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dtgsanpham.Rows.Count; i++)
                {
                    if (dtgsanpham.Rows[i].Cells[0].Value.ToString() == cbmasp.SelectedValue.ToString())
                    {
                        sl.Maximum = int.Parse(dt3.Rows[0][0].ToString()) - int.Parse(dtgsanpham.Rows[i].Cells[1].Value.ToString());
                    }
                }
                if (bien == false)
                {
                    sl.Maximum = int.Parse(dt3.Rows[0][0].ToString());
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dtgsanpham.SelectedRows)
            {
                dtgsanpham.Rows.RemoveAt(item.Index);
            }
        }

        private void txtgia_TextChanged(object sender, EventArgs e)
        {
            if (txtgia.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtgia.Text, System.Globalization.NumberStyles.AllowThousands);
                txtgia.Text = String.Format(culture, "{0:N0}", value);
                txtgia.Select(txtgia.Text.Length, 0);
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
        void thieudulieu1()
        {
            daydu1 = false;
            if(txtmhd.Text == "" || cbmadotkm.Text == "" || cbmakh.Text == "" || dtpngayxuat.Value.Date < System.DateTime.Now.Date)
            {
                daydu1 = false;
            }   
            else
            {
                daydu1 = true;
            }    
        }

        private void NhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (a == true)
            {
                this.Dispose();
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                if (tb == DialogResult.OK)
                {
                    e.Cancel = false;
                    DangNhap dn = new DangNhap();
                    dn.Show();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void cbmakh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txttongtien.Text != "")
            {
                DataTable dt11 = KetnoiCSDL.ExcuteQuery("select TILECHIETKHAU,TENKH from LOAIKHACHHANG, KHACHHANG where MAKH = '" + cbmakh.SelectedValue.ToString() + "' and LOAIKHACHHANG.MALOAIKH = KHACHHANG.MALOAIKH");
                DataTable dt12 = KetnoiCSDL.ExcuteQuery("select SOPHANTRAMKM from DOTKHUYENMAI where MADOTKM = '" + cbmadotkm.SelectedValue.ToString() + "'");
                if (dt11.Rows.Count > 0)
                {
                    txttenkhachhang.Text = dt11.Rows[0][1].ToString();
                    if(cbmadotkm.Text == "Không")
                    {
                        txtongtiencuoi.Text = (int.Parse(txttongtien.Text.Replace(",", string.Empty)) - (int.Parse(txttongtien.Text.Replace(",", string.Empty)) * int.Parse(dt11.Rows[0][0].ToString()) / 100)).ToString();
                        if (cbmadotkm.SelectedValue.ToString().Substring(0, 3) == "KHS")
                        {
                            lblgiamkh.Text = cbmakh.Text+ " : " + dt11.Rows[0][0].ToString() + "%";
                        }
                        else
                        {
                            lblgiamkh.Text = cbmakh.Text+ " : " + dt11.Rows[0][0].ToString() + "%";
                        }
                        lblgiamkm.Text = "KM 0 %";
                    }
                    else if(cbmadotkm.SelectedValue.ToString() != "Không" && dt12.Rows.Count > 0)
                    {
                        txtongtiencuoi.Text = (int.Parse(txttongtien.Text.Replace(",", string.Empty)) - (int.Parse(txttongtien.Text.Replace(",", string.Empty)) * (int.Parse(dt11.Rows[0][0].ToString())+ int.Parse(dt12.Rows[0][0].ToString())) / 100)).ToString();
                        lblgiamkm.Text = "KM " + dt12.Rows[0][0].ToString() + "%";
                        if (cbmadotkm.SelectedValue.ToString().Substring(0, 3) == "KHS")
                        {
                            lblgiamkh.Text = cbmakh.Text + " : " + dt11.Rows[0][0].ToString() + "%";
                        }
                        else
                        {
                            lblgiamkh.Text = cbmakh.Text + " : " + dt11.Rows[0][0].ToString() + "%";
                        }
                    }    
                }
            }
        }

        private void cbmadotkm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txttongtien.Text != "")
            {
                DataTable dt11 = KetnoiCSDL.ExcuteQuery("select TILECHIETKHAU,TENKH from LOAIKHACHHANG, KHACHHANG where MAKH = '" + cbmakh.SelectedValue.ToString() + "' and LOAIKHACHHANG.MALOAIKH = KHACHHANG.MALOAIKH");
                DataTable dt12 = KetnoiCSDL.ExcuteQuery("select SOPHANTRAMKM from DOTKHUYENMAI where MADOTKM = '" + cbmadotkm.SelectedValue.ToString() + "'");
                if (dt12.Rows.Count > 0)
                {
                    if (cbmakh.Text == "KHL0")
                    {
                        txtongtiencuoi.Text = (int.Parse(txttongtien.Text.Replace(",", string.Empty)) - (int.Parse(txttongtien.Text.Replace(",", string.Empty)) * int.Parse(dt11.Rows[0][0].ToString()) / 100)).ToString();
                        lblgiamkm.Text = "KM " + dt12.Rows[0][0].ToString() + "%";
                        lblgiamkh.Text = "KH 0 %";
                    }
                    else if (cbmakh.Text != "KHL0" && dt11.Rows.Count > 0)
                    {
                        txtongtiencuoi.Text = (int.Parse(txttongtien.Text.Replace(",", string.Empty)) - (int.Parse(txttongtien.Text.Replace(",", string.Empty)) * (int.Parse(dt11.Rows[0][0].ToString()) + int.Parse(dt12.Rows[0][0].ToString())) / 100)).ToString();
                        lblgiamkm.Text = "KM " + dt12.Rows[0][0].ToString() + "%";
                        if(cbmadotkm.SelectedValue.ToString().Substring(0,3) == "KHS")
                        {
                            lblgiamkh.Text = cbmakh.Text + " : " + dt11.Rows[0][0].ToString() + "%";
                        }
                        else
                        {
                            lblgiamkh.Text = cbmakh.Text + " : " + dt11.Rows[0][0].ToString() + "%";
                        }    
                    }
                }
            }
        }

        private void txtongtiencuoi_TextChanged(object sender, EventArgs e)
        {
            if (txtongtiencuoi.TextLength > 0)
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(txtongtiencuoi.Text, System.Globalization.NumberStyles.AllowThousands);
                txtongtiencuoi.Text = String.Format(culture, "{0:N0}", value);
                txtongtiencuoi.Select(txtongtiencuoi.Text.Length, 0);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XemDoanhThu xdt = new XemDoanhThu();
            xdt.tk = tentk;
            xdt.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select * from MATHANG where MASP like '" + textBox1.Text + "' and BANHUTANG <> N'Hư'");
            if (dt3.Rows.Count > 0)
            {
                txttensp.Text = dt3.Rows[0][1].ToString();
                cbmasp.Text = dt3.Rows[0][0].ToString();
                txtgia.Text = dt3.Rows[0][5].ToString();
                sl.Enabled = true;
            } 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            cbmasp.Enabled = true;
            textBox1.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            cbmasp.Enabled = false;
            textBox1.Enabled = true;
        }

        private void textBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select * from MATHANG where MABC like '" + textBox1.Text + "' and BANHUTANG <> N'Hư'");
            if (dt3.Rows.Count > 0)
            {
                txttensp.Text = dt3.Rows[0][1].ToString();
                cbmasp.Text = dt3.Rows[0][0].ToString();
                txtgia.Text = dt3.Rows[0][5].ToString();
                sl.Enabled = true;
            }
        }

        private void cbmasp_TextChanged(object sender, EventArgs e)
        {
            /*DataTable dt3 = KetnoiCSDL.ExcuteQuery("select * from MATHANG where MASP = '" + cbmasp.SelectedValue.ToString() + "'");
            if (dt3.Rows.Count > 0)
            {
                txttensp.Text = dt3.Rows[0][1].ToString();
                textBox1.Text = dt3.Rows[0][4].ToString();
                txtgia.Text = dt3.Rows[0][5].ToString();
            }
            sl.Enabled = true;*/
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int tongtien = 0;
            int tong = 0;
            thieudulieu1();
            bool a = false;
            a = KetnoiCSDL.ExcuteQueryReader("select MAHD from HOADON where  MAHD = '" + string.Concat(label9.Text, txtmhd.Text) + "'");
            if (daydu1 == true && dtgsanphamsapxuat.Rows.Count > 0 && a == false)
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thêm " + string.Concat(label9.Text, txtmhd.Text) + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    tongtien = int.Parse(txttongtien.Text.Replace(",", string.Empty)) + int.Parse(txttongtien.Text.Replace(",", string.Empty));
                    KetnoiCSDL.ExcuteNonQuery("insert into HOADON values('" + string.Concat(label9.Text, txtmhd.Text) + "', '" + barHeaderItem1.Caption + "','" + cbmakh.SelectedValue.ToString() + "','" + cbmadotkm.SelectedValue.ToString() + "', '" + String.Format("{0:MM/dd/yyyy}", dtpngayxuat.Value) + "'," + tongtien + ")");
                    Same.Success();
                    for (int f = 0; f < dtgsanphamsapxuat.Rows.Count; f++)
                    {
                        KetnoiCSDL.ExcuteNonQuery("insert into CTHD values('" + string.Concat(label9.Text, txtmhd.Text) + "', '" + dtgsanphamsapxuat.Rows[f].Cells[0].Value.ToString() + "'," + int.Parse(dtgsanphamsapxuat.Rows[f].Cells[1].Value.ToString()) + "," + int.Parse(dtgsanphamsapxuat.Rows[f].Cells[4].Value.ToString().Replace(",", string.Empty)) + ")");
                        KetnoiCSDL.ExcuteNonQuery("update MATHANG set SLTON = SLTON - " + int.Parse(dtgsanphamsapxuat.Rows[f].Cells[1].Value.ToString()) + " where MASP = '" + dtgsanphamsapxuat.Rows[f].Cells[0].Value.ToString() + "'");
                        tong += int.Parse(dtgsanphamsapxuat.Rows[f].Cells[4].Value.ToString().Replace(",", string.Empty)) * int.Parse(dtgsanphamsapxuat.Rows[f].Cells[1].Value.ToString());
                    }                 
                    DataTable table24 = new DataTable();
                    table24.Clear();
                    DataTable table25 = new DataTable();
                    table25.Clear();
                    DataTable table26 = new DataTable();
                    table26.Clear();
                    table25.Columns.Add("MAHD", typeof(string));
                    table25.Columns.Add("NGAYXUAT", typeof(DateTime));
                    table25.Columns.Add("TONGTIEN", typeof(string));
                    table24.Columns.Add("MASP", typeof(string));
                    table24.Columns.Add("SL", typeof(string));
                    table24.Columns.Add("GIA", typeof(string));
                    table26.Columns.Add("KH", typeof(string));
                    table26.Columns.Add("KM", typeof(string));
                    table26.Columns.Add("TONGTIENCUOI", typeof(string));
                    table25.Rows.Add(string.Concat(label9.Text, txtmhd.Text), dtpngayxuat.Text, string.Concat(string.Format(new CultureInfo("vi-VN"), "{0:N0}", tong)," đồng"));
                    table26.Rows.Add(lblgiamkh.Text, lblgiamkm.Text, string.Concat(string.Format(new CultureInfo("vi-VN"), "{0:N0}", int.Parse(txtongtiencuoi.Text.Replace(",", string.Empty))), " đồng"));
                    for (int tai = 0;tai < dtgsanphamsapxuat.Rows.Count;tai++)
                    {
                        DataTable tensp = KetnoiCSDL.ExcuteQuery("select TENSP from MATHANG where MASP = '" + dtgsanphamsapxuat.Rows[tai].Cells[0].Value.ToString() + "'");
                        table24.Rows.Add(tensp.Rows[0][0].ToString(), dtgsanphamsapxuat.Rows[tai].Cells[1].Value.ToString(), string.Concat(string.Format(new CultureInfo("vi-VN"), "{0:N0}",int.Parse(dtgsanphamsapxuat.Rows[tai].Cells[1].Value.ToString()) * int.Parse(dtgsanphamsapxuat.Rows[tai].Cells[4].Value.ToString().Replace(",", string.Empty)))," đồng"));
                    }
                    InHoaDon ihd = new InHoaDon();
                    ihd.table25 = table25;
                    ihd.table24 = table24;
                    ihd.table26 = table26;
                    ihd.Show();
                    txtmhd.Text = "";

                    DataTable dt10 = KetnoiCSDL.ExcuteQuery("select top 1 * from HOADON order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
                    dataGridView1.DataSource = dt10;
                    DataTable dt11 = KetnoiCSDL.ExcuteQuery("select * from CTHD where MAHD = '"+dt10.Rows[0][0].ToString()+"'");
                    dtgctnh.DataSource = dt11;
                    dtgsanphamsapxuat.Rows.Clear();
                    dtgsanpham.Rows.Clear();
                    txttongtien.Text = "";
                    txtongtiencuoi.Text = "";
                    cbmadotkm.SelectedIndex = 0;
                    cbmakh.SelectedIndex = 0;
                    lblgiamkh.Text = "Giảm";
                    lblgiamkm.Text = "Giảm";
                }
            }
            else
            {
                MessageBox.Show("Bạn nhập sai hoặc thiếu ký tự hoặc chưa có sản phẩm hoặc trùng mã nhập hàng");
            }
        }
    }
}