using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SacMauShop.Class.Code
{
    class Same
    {
        public static void Success()
        {
            MessageBox.Show("Thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Error()
        {
            MessageBox.Show("Thất bại, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static int step = 100, time = 100000, interval = 100;
    }
}
