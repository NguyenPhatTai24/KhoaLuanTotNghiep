using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SacMauShop.Class.Code;
using SacMauShop.Class.Connection;

namespace SacMauShop
{
    public partial class XacThuc : DevExpress.XtraEditors.XtraForm
    {
        int solan = 0;
        string[] chuoi = new string[] { "A", "B", "C", "D", "E", "Q", "W", "E", "R", "1", "2", "3", "4", "5", "6", "7", "8", "9","a","b","c","d"};
        public string TenTK , maxacthuc , TenTK1 = "";
        public XacThuc()
        {
            InitializeComponent();
        }

        private void XacThuc_Load(object sender, EventArgs e)
        {
            try
            {
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    maxacthuc += chuoi[r.Next(0, 22)];
                }
                GuiMail("shopsacmau01@gmail.com", TenTK, "Mã xác nhận", maxacthuc);
                PerformTime.Step = Same.step;
                PerformTime.Maximum = Same.time;
                PerformTime.Value = 0;
                timer1.Interval = Same.interval;
                timer1.Start();
            }
            catch
            {
                MessageBox.Show("Email sai");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformTime.PerformStep();
            if (PerformTime.Value >= PerformTime.Maximum)
            {
                timer1.Stop();
                Same.Error();
                DangNhap dn = new DangNhap();
                dn.Show();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn muốn thoát giao diện ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tb == DialogResult.OK)
            {
                DangNhap dn = new DangNhap();
                dn.Show();
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            maxacthuc = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                maxacthuc += chuoi[r.Next(0, 22)];
            }
            GuiMail("shopsacmau01@gmail.com", TenTK, "Mã xác nhận", maxacthuc);
            PerformTime.Step = Same.step;
            PerformTime.Maximum = Same.time;
            PerformTime.Value = 0;
            timer1.Interval = Same.interval;
            timer1.Start();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (solan < 3)
            {
                if (maxacthuc == txtmaxacthuc.Text)
                {
                    MessageBox.Show("Xác thực thành công");
                    QuenMatKhau qmk = new QuenMatKhau();
                    qmk.bienxacthuc = 1;
                    qmk.TenTK1 = TenTK1;
                    qmk.Show();
                    this.Close();
                }
                else
                {
                    solan = solan + 1;
                    MessageBox.Show("Xác thực thất bại lần " + solan);
                }
            }
            else
            {
                MessageBox.Show("Quá số lần quy định(3) vui lòng xác thực lại sau");
                DangNhap dn = new DangNhap();
                dn.Show();
                this.Close();
            }
        }
        void GuiMail(string from, string to, string subject, string message)
        {
            MailMessage mess = new MailMessage(from, to, subject, message);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("shopsacmau01@gmail.com", "shopsacmau123");
            client.Send(mess);
        }
    }
}