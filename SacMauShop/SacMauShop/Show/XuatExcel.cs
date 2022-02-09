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
    public partial class XuatExcel : Form
    {
        public XuatExcel()
        {
            InitializeComponent();
        }
        public int a = 0;
        void clear()
        {
            cbbanhutang.Enabled = false;
            cbmanhap.Enabled = false;
            cbnsp.Enabled = false;
            dtpnh.Enabled = false;
            slton.Enabled = false;
            slton1.Enabled = false;
            giaban.Enabled = false;
            giaban1.Enabled = false;
        }
        private void XuatExcel_Load(object sender, EventArgs e)
        {
            DataTable dt = KetnoiCSDL.ExcuteQuery("select Distinct(BANHUTANG) from MATHANG");
            cbbanhutang.DataSource = dt;
            cbbanhutang.DisplayMember = "BANHUTANG";
            cbbanhutang.ValueMember = "BANHUTANG";

            DataTable dt1 = KetnoiCSDL.ExcuteQuery("select Distinct(NHOMSP) from MATHANG");
            cbnsp.DataSource = dt1;
            cbnsp.DisplayMember = "NHOMSP";
            cbnsp.ValueMember = "NHOMSP";

            DataTable dt2 = KetnoiCSDL.ExcuteQuery("select Distinct(MANHAPHANG) from DOTNHAPHANG");
            cbmanhap.DataSource = dt2;
            cbmanhap.DisplayMember = "MANHAPHANG";
            cbmanhap.ValueMember = "MANHAPHANG";

            clear();
        }
        public void Export(DataTable dt, string sheetName, string title,string filename)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;



            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;



            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Tahoma";

            head.Font.Size = "18";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã sản phẩm";

            cl1.ColumnWidth = 13.5;


            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên sản phẩm";

            cl2.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Số lượng tồn";

            cl3.ColumnWidth = 12;


            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Nhóm sản phẩm";

            cl4.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Mã nhãn dán";

            cl5.ColumnWidth = 25.0;


            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Giá bán";

            cl6.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Trạng thái";

            cl7.ColumnWidth = 10.0;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");
            rowHead.Font.Bold = true;
            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            rowHead.Interior.ColorIndex = 15;
            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];
            for (int r = 0; r < dt.Rows.Count; r++)

            {

                DataRow dr = dt.Rows[r];

                for (int c = 0; c < dt.Columns.Count; c++)

                {
                    arr[r, c] = dr[c];
                }
            }
            int rowStart = 4;
            int columnStart = 1;
            int rowEnd = rowStart + dt.Rows.Count - 1;
            int columnEnd = dt.Columns.Count;
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);
            range.Value2 = arr;
            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
            Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
            oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            oBook.SaveAs(filename);
            MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
        }
        private void bht_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            cbbanhutang.Enabled = true;
            a = 1;
        }

        private void nsp_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            cbnsp.Enabled = true;
            a = 2;
        }

        private void mnh_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            cbmanhap.Enabled = true;
            a = 3;
        }

        private void nnh_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            dtpnh.Enabled = true;
            a = 4;
        }

        private void slt_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            slton.Enabled = true;
            slton1.Enabled = true;
            a = 5;
        }

        private void gb_CheckedChanged(object sender, EventArgs e)
        {
            clear();
            giaban.Enabled = true;
            giaban1.Enabled = true;
            a = 6;
        }

        private void bttim_Click(object sender, EventArgs e)
        {
            switch(a)
            {
                case 0: MessageBox.Show("Vui lòng chọn dữ liệu cần tìm"); break;
                case 1: 
                if(cbbanhutang.SelectedValue.ToString() != "")
                    {
                        DataTable dt1 = KetnoiCSDL.ExcuteQuery("select Distinct(MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where BANHUTANG = N'" + cbbanhutang.SelectedValue.ToString()+"'");
                        dtgbarcode.DataSource = dt1;
                    }   
                else
                    {
                        MessageBox.Show("Vui lòng chọn dữ liệu cần tìm"); 
                    }
                    break;
                case 2:
                    if (cbnsp.SelectedValue.ToString() != "")
                    {
                        DataTable dt2 = KetnoiCSDL.ExcuteQuery("select Distinct(MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where NHOMSP = N'" + cbnsp.SelectedValue.ToString() + "'");
                        dtgbarcode.DataSource = dt2;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn dữ liệu cần tìm");
                    }
                    break;
                case 3:
                    if (cbmanhap.SelectedValue.ToString() != "")
                    {
                        DataTable dt3 = KetnoiCSDL.ExcuteQuery("select Distinct(MATHANG.MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG,CHITIETNHAPHANG where MANHAPHANG = '" + cbmanhap.SelectedValue.ToString() + "' and MATHANG.MASP = CHITIETNHAPHANG.MASP");
                        dtgbarcode.DataSource = dt3;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn dữ liệu cần tìm");
                    }
                    break;
                case 4:
                    DataTable dt4 = KetnoiCSDL.ExcuteQuery("select Distinct(MATHANG.MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG,CHITIETNHAPHANG,DOTNHAPHANG where CHITIETNHAPHANG.MANHAPHANG = DOTNHAPHANG.MANHAPHANG and MATHANG.MASP = CHITIETNHAPHANG.MASP and THOIGIANNHAP = '" + String.Format("{0:MM/dd/yyyy}",dtpnh.Value)+"'");
                    dtgbarcode.DataSource = dt4;
                    break;
                case 5:
                    if (slton.Text != "" && slton1.Text != "")
                    {
                        DataTable dt5 = KetnoiCSDL.ExcuteQuery("select Distinct(MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where SLTON >= " + int.Parse(slton.Text)+ " and SLTON <= " + int.Parse(slton1.Text) + "");
                        dtgbarcode.DataSource = dt5;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền dữ liệu cần tìm");
                    }    
                    break;
                case 6:
                    if (giaban.Text != "" && giaban1.Text != "")
                    {
                        DataTable dt6 = KetnoiCSDL.ExcuteQuery("select Distinct(MASP) as N'Mã sản phẩm',TENSP as N'Tên sản phẩm',SLTON as N'Số lượng tồn',NHOMSP as N'Nhóm sản phẩm',MABC as N'Mã nhãn dán',concat(FORMAT(GIABAN, 'N0','en-US'),N' đồng') as N'Giá bán',BANHUTANG as N'Bán Hư Tặng' from MATHANG where GIABAN >= " + int.Parse(giaban.Text) + " and GIABAN <= " + int.Parse(giaban1.Text) + "");
                        dtgbarcode.DataSource = dt6;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền dữ liệu cần tìm");
                    }
                    break;
            } 
        }

        private void btxuat_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XuatExcel excel = new XuatExcel();
                DataTable dt = (DataTable)dtgbarcode.DataSource;
                excel.Export(dt, "Danh sach", "DANH SÁCH CÁC MẶT HÀNG", saveFileDialog1.FileName);
            }
        }
    }
}
