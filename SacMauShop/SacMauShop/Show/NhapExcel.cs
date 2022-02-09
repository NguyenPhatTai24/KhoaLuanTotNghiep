using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SacMauShop.Class.Code;
using SacMauShop.Class.Connection;

namespace SacMauShop
{
    public partial class NhapExcel : Form
    {
        public NhapExcel()
        {
            InitializeComponent();
        }
        private DataTable oTbl;
        private string filename;
        private void readExcel()
        {
            if(filename == null)
            {
                MessageBox.Show("Chưa chọn file Excel");
            }
            else
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                try
                {
                    ExcelApp.Workbooks.Open(filename);
                }
                catch
                {
                    MessageBox.Show("Không thể mở file");
                }
                oTbl = new DataTable();
                oTbl.Columns.Add("Mã sản phẩm", typeof(string));
                oTbl.Columns.Add("Tên sản phẩm", typeof(string));
                oTbl.Columns.Add("Số lượng tồn", typeof(string));
                oTbl.Columns.Add("Nhóm sản phẩm", typeof(string));
                oTbl.Columns.Add("Mã nhãn dán", typeof(string));
                oTbl.Columns.Add("Giá bán", typeof(string));
                oTbl.Columns.Add("Trạng thái", typeof(string));
                long f = 0;
                foreach(Microsoft.Office.Interop.Excel.Worksheet WSheet in ExcelApp.Worksheets)
                {
                    DataRow dr;
                    long i = 4;
                    try
                    {
                        do
                        {
                            if (WSheet.Range["A"+i].Value == null && WSheet.Range["B"+i].Value == null && WSheet.Range["C"+i].Value == null && WSheet.Range["D"+i].Value == null && WSheet.Range["E"+i].Value == null && WSheet.Range["F"+i].Value == null && WSheet.Range["G"+i].Value == null)
                            {
                                break;
                            }
                            dr = oTbl.NewRow();
                            dr["Mã sản phẩm"] = WSheet.Range["A"+i].Value;
                            dr["Tên sản phẩm"] = WSheet.Range["B"+i].Value;
                            dr["Số lượng tồn"] = WSheet.Range["C"+i].Value;
                            dr["Nhóm sản phẩm"] = WSheet.Range["D"+i].Value;
                            dr["Mã nhãn dán"] = WSheet.Range["E"+i].Value;
                            dr["Giá bán"] = WSheet.Range["F"+i].Value;
                            dr["Trạng thái"] = WSheet.Range["G"+i].Value;
                            oTbl.Rows.Add(dr);
                            i++;
                        }
                        while (true);
                    }
                    catch {
                        MessageBox.Show("Có lỗi khi thực hiện");
                    }
                }    
            }    
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            DialogResult dr = op.ShowDialog();
            if (dr == DialogResult.OK)
            {
                filename = op.FileName;
                readExcel();
                dtgbarcode.DataSource = oTbl;
            }
            if(dtgbarcode.Rows.Count>0)
            {
                label1.Text = "Tổng số mã sản phẩm là : " + dtgbarcode.Rows.Count.ToString();
            }    
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int tai1 = 0;
            int tai2 = 0;
            if (dtgbarcode.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < dtgbarcode.Rows.Count; i++)
                    {
                        bool a = true;
                        a = KetnoiCSDL.ExcuteQueryReader("select MASP from MATHANG where MASP ='" + dtgbarcode.Rows[i].Cells[0].Value.ToString() + "'");
                        if (a == false)
                        {
                            KetnoiCSDL.ExcuteNonQuery("insert into MATHANG values('" + dtgbarcode.Rows[i].Cells[0].Value.ToString() + "', N'" + dtgbarcode.Rows[i].Cells[1].Value.ToString() + "', " + dtgbarcode.Rows[i].Cells[2].Value.ToString() + ",N'" + dtgbarcode.Rows[i].Cells[3].Value.ToString() + "', '" + dtgbarcode.Rows[i].Cells[4].Value.ToString() + "'," + int.Parse(dtgbarcode.Rows[i].Cells[5].Value.ToString().Replace(",", string.Empty).Replace(" đồng", string.Empty)) + ",N'" + dtgbarcode.Rows[i].Cells[6].Value.ToString() + "')");
                            tai1++;
                        }
                        else if (a == true)
                        {
                            KetnoiCSDL.ExcuteNonQuery("update MATHANG set TENSP = N'" + dtgbarcode.Rows[i].Cells[1].Value.ToString() + "', SLTON = " + int.Parse(dtgbarcode.Rows[i].Cells[2].Value.ToString()) + ", NHOMSP = N'" + dtgbarcode.Rows[i].Cells[3].Value.ToString() + "', MABC = '" + dtgbarcode.Rows[i].Cells[4].Value.ToString() + "', GIABAN = " + int.Parse(dtgbarcode.Rows[i].Cells[5].Value.ToString().Replace(",", string.Empty).Replace(" đồng", string.Empty)) + ", BANHUTANG = N'" + dtgbarcode.Rows[i].Cells[6].Value.ToString() + "' where MASP = '" + dtgbarcode.Rows[i].Cells[0].Value.ToString() + "'");
                            tai2++;
                        }
                    }
                    MessageBox.Show("Nhập thành công : "+tai1+"\n"+" Cập nhật thành công : "+tai2);
                }
                catch
                {
                    MessageBox.Show("Vui lòng kiếm tra lại dữ liệu");
                }
            }
        }
    }
}
