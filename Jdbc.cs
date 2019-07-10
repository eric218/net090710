using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    class Jdbc
    {
        static String url = System.Configuration.ConfigurationManager.ConnectionStrings["database"].ConnectionString.ToString();
        public static int updateData(string sql)
        {
            Console.WriteLine(@"[{0}]sql:{1}", DateTimeOffset.Now, sql);
            MySqlConnection m_conn = new MySqlConnection(url);
            m_conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, m_conn);
            cmd.CommandTimeout = 12000;
            int iRecordAffected = cmd.ExecuteNonQuery();//返回插入了几条数据
            return iRecordAffected;
        }

        public static DataTable GetDataTable(string sql, params MySqlParameter[] parameters)
        {
            DataSet dataset = new DataSet();
            using (MySqlConnection con = new MySqlConnection(url))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataset);
                }
            }
            return dataset.Tables[0];
        }
    }
}
