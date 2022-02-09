using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SacMauShop.Class.Connection
{
    class KetnoiCSDL
    {
        private static SqlConnection cnn = new SqlConnection();
        public static void MoKetNoi()
        {
            string sqlcon = @"Data Source=DESKTOP-JVM8D5H\SQLEXPRESS;Initial Catalog=SacMau;Integrated Security=True";
            cnn.ConnectionString = sqlcon;
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
        }
        public static void DongKetNoi()
        {
            if (cnn.State == ConnectionState.Open)
                cnn.Close();
        }
        public static DataTable ExcuteQuery(string sql)
        {
            MoKetNoi();
            SqlCommand cd = new SqlCommand(sql, cnn);
            SqlDataReader dr = cd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            DongKetNoi();
            return dt;
        }
        public static bool ExcuteQueryReader(string sql)
        {
            MoKetNoi();
            SqlCommand cd = new SqlCommand(sql, cnn);
            SqlDataReader dr = cd.ExecuteReader();
            bool giatri = dr.Read();
            DongKetNoi();
            return giatri;
        }
            public static void ExcuteNonQuery(string sql)
        {
            MoKetNoi();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }
    }
}
