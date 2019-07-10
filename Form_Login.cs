using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String uid = textuid.Text;
            String pwd = textpwd.Text;
            if (uid!="" && pwd!="")
            {
                String sql = "select * from person where id=@id";
                MySqlParameter[] parameters = { new MySqlParameter("@id", MySqlDbType.Int32) };
                parameters[0].Value = uid;
                DataTable tab = Jdbc.GetDataTable(sql, parameters);
                if (tab.Rows.Count==1)
                {
                    String code = tab.Rows[0]["PERSONCODE"].ToString();
                    Console.WriteLine(@"[{0}],code={1}", DateTimeOffset.Now, code);
                    if (code.Equals(pwd))
                    {
                        this.Hide();
                        Form1 f = new Form1();
                        f.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("密码错误");
                    }
                }
                else
                {
                    MessageBox.Show("查询不到用户id");
                }
                
            }
            else
            {
                MessageBox.Show("用户名密码不能为空");
            }
        }
    }
}
