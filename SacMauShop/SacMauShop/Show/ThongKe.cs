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
    public partial class ThongKe : UserControl
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {

             DataTable dt = KetnoiCSDL.ExcuteQuery("select top 5 sum(TONGTIEN) as 'TT',MAKH from HOADON where year(NGAYXUATHD) = '"+System.DateTime.Now.Year.ToString()+"' group by  MAKH");
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                chart1.Series["SL"].Points.AddXY(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                chart1.Series["SL"].Points[i].Label = dt.Rows[i][0].ToString();
            }

            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select Concat(N'Tháng ',Month(NGAYXUATHD)),Sum(TONGTIEN) from HOADON where year(NGAYXUATHD) = '" + System.DateTime.Now.Year.ToString() + "' group by Month(NGAYXUATHD)");
             for (int j = 0; j < dt1.Rows.Count; j++)
             {
                    chart2.Series["DoanhThu"].Points.AddXY(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString());
                    chart2.Series["DoanhThu"].Points[j].Label = dt1.Rows[j][1].ToString();
             }
            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select NHANVIEN.MANV,sum(TONGTIEN) from HOADON,NHANVIEN where HOADON.MANV = NHANVIEN.MANV and year(NGAYXUATHD) = '" + System.DateTime.Now.Year.ToString() + "' and month(NGAYXUATHD) = '" + System.DateTime.Now.Month.ToString() + "' group by NHANVIEN.MANV");
            for (int a = 0; a < dt2.Rows.Count; a++)
            {
                chart4.Series["NhanVien"].Points.AddXY(dt2.Rows[a][0].ToString(), dt2.Rows[a][1].ToString());
                chart4.Series["NhanVien"].Points[a].Label = dt2.Rows[a][1].ToString();
            }
            DataTable dt3 = KetnoiCSDL.ExcuteQuery("select top 5 sum(SL) as 'SL',MASP from CTHD,HOADON where CTHD.MAHD = HOADON.MAHD and year(NGAYXUATHD) = '" + System.DateTime.Now.Year.ToString() + "' and month(NGAYXUATHD) = '" + System.DateTime.Now.Month.ToString() + "' group by MASP");
            for (int b = 0; b < dt3.Rows.Count; b++)
            {
                chart3.Series["SLBan"].Points.AddXY(dt3.Rows[b][1].ToString(), dt3.Rows[b][0].ToString());
                chart3.Series["SLBan"].Points[b].Label = dt3.Rows[b][0].ToString();
            }
        }
    }
}
