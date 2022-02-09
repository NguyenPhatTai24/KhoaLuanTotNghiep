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
    public partial class LoaiKhachHang : Form
    {
        public bool DayDu, kytu;
        public LoaiKhachHang()
        {
            InitializeComponent();
        }
        void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        void batkytu()
        {
            kytu = false;
            if (textBox2.Text.Length > 2)
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
            if (textBox1.Text == "" || textBox2.Text == "")
                DayDu = false;
            else
                DayDu = true;
        }
        void load()
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select MALOAIKH as N'Mã loại khách hàng', TILECHIETKHAU as N'Tỉ lệ chiết khấu' from LOAIKHACHHANG");
            dataGridView1.DataSource = dt;
        }

        private void LoaiKhachHang_Load(object sender, EventArgs e)
        {
            load();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool a = true;
            a = KetnoiCSDL.ExcuteQueryReader("select MALOAIKH from LOAIKHACHHANG where MALOAIKH ='" + textBox2.Text + "'");
            thieudulieu();
            batkytu();
            if (DayDu == true && kytu == true && a == false)
            {
                DialogResult tb = MessageBox.Show("Bạn muốn thêm " + textBox2.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("insert into LOAIKHACHHANG values('" +textBox2.Text+ "', " + int.Parse(textBox1.Text) + ")");
                    load();
                    Same.Success();
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
            }    
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool a = false;
            a = KetnoiCSDL.ExcuteQueryReader("select MALOAIKH from LOAIKHACHHANG where MALOAIKH ='" + textBox2.Text + "'");
            thieudulieu();
            batkytu();
            if (DayDu == true && kytu == true && a == true)
            {
                DialogResult tb = MessageBox.Show("Bạn muốn sửa " + textBox2.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("update LOAIKHACHHANG set TILECHIETKHAU = " + int.Parse(textBox1.Text) + " where MALOAIKH = '" +textBox2.Text+ "'");
                    load();
                    Same.Success();
                    clear();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void LoaiKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            if (tb == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }    
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            bool a = false;
            a = KetnoiCSDL.ExcuteQueryReader("select MALOAIKH from LOAIKHACHHANG where MALOAIKH ='" + textBox2.Text + "'");
            bool b = false;
            b = KetnoiCSDL.ExcuteQueryReader("select MALOAIKH from KHACHHANG where MALOAIKH ='" + textBox2.Text + "'");
            thieudulieu();
            if (DayDu == false && a == true && b == false)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
            }
            else if(DayDu == false && a == true && b == true)
            {
                MessageBox.Show("Còn tồn tại khách hàng trong loại này");
            }    
            else
            {
                DialogResult tb = MessageBox.Show("Bạn muốn xóa " + textBox2.Text + " ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tb == DialogResult.OK)
                {
                    KetnoiCSDL.ExcuteNonQuery("Delete from LOAIKHACHHANG where MALOAIKH = '" + textBox2.Text + "'");
                    load();
                    clear();
                    Same.Success();
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin, ký tự hoặc sai mã nhân viên");
                }
            }
        }
    }
}
