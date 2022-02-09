using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SacMauShop.Class.Code;
using SacMauShop.Class.Connection;

namespace SacMauShop
{
    public partial class XemDoanhThu : Form
    {
        public string tk;
        public XemDoanhThu()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            radioButton1.Checked = false;
            dateTimePicker1.Enabled = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt10 = KetnoiCSDL.ExcuteQuery("select * from HOADON  where MANV = '" + tk + "' order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
            dtgxemthongke.DataSource = dt10;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt10 = KetnoiCSDL.ExcuteQuery("select * from HOADON  where MANV = '" + tk + "' and month(NGAYXUATHD) = '"+comboBox1.Text+"' order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
            dtgxemthongke.DataSource = dt10;
        }

        private void XemDoanhThu_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            label3.Text = tk;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt10 = KetnoiCSDL.ExcuteQuery("select * from HOADON  where MANV = '" + tk + "' and NGAYXUATHD = '" + String.Format("{0:MM/dd/yyyy}", dateTimePicker1.Value) + "' order by Convert(int,SUBSTRING(MAHD,3,LEN(MAHD))) desc");
            dtgxemthongke.DataSource = dt10;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dtgxemthongke.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể ghi dữ liệu tới ổ đĩa. Mô tả lỗi:" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dtgxemthongke.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dtgxemthongke.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dtgxemthongke.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Dữ liệu Export thành công!!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Mô tả lỗi :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có bản ghi nào được Export!!!", "Info");
            }
        }
    }
}
