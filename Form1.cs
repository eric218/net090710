using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("sss11");
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Button2_Click(object sender, EventArgs e)
        {
  
            //password = Password.Text;
            Console.WriteLine(@"[{0}]username:{1},password:{2}", DateTimeOffset.Now, "s", "f");

            //createTab();

            //Jdbc.updateData("INSERT INTO test.person(personcode,personname1) values('123456','测试老sss')");
            getResult();
        }

        private int insert()
        {
            MySqlConnection m_conn = new MySqlConnection("server=localhost;user id=root;password=root;persist security info=True;database=test;Charset=utf8");
            m_conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO test.person(personcode,personname1) values('123456','测试老王')", m_conn);
            cmd.CommandTimeout = 12000;
            int iRecordAffected = cmd.ExecuteNonQuery();//返回插入了几条数据
            return iRecordAffected;
        }

        private void getResult()
        {
            String code, name;
            code = f_code.Text;
            name = f_name.Text;

            String url = System.Configuration.ConfigurationManager.ConnectionStrings["database"].ConnectionString.ToString();
            //using 不需要关闭流
            using (MySqlConnection myCon = new MySqlConnection(url))
            {

                //2.打开
                myCon.Open();
                string sql = "select * from person where 1=1";
                if (code.Length>0)
                {
                    sql = sql + " and PERSONCODE like '%"+ code + "%'";

                }
                if (name.Length > 0)
                {
                    sql = sql + " and PERSONNAME1 like '%" + name + "%'";

                }
                Console.WriteLine(@"[{0}]sql:{1}", DateTimeOffset.Now, sql);

                //3.执行命令
                MySqlCommand cmd = new MySqlCommand(sql, myCon);
                //4.读取结果
                MySqlDataReader read = cmd.ExecuteReader();
                //5.使用
                List<Person> stuList = new List<Person>();
                while (read.Read())
                {
                    //构造初始化器
                    stuList.Add(new Person()
                    {
                        id = Convert.ToInt32(read.GetValue(0).ToString()),
                        personCode = read.GetValue(1).ToString(),
                        personName1 = read.GetValue(2).ToString()
                    });
                }
                dataGridView1.DataSource = stuList;
                Console.WriteLine(@"[{0}]stuList:{1}", DateTimeOffset.Now, stuList);
            }
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewColumn colunm = this.dataGridView1.Columns[e.ColumnIndex];
                string id = this.dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString().Trim();
                string code = this.dataGridView1.Rows[e.RowIndex].Cells["code"].Value.ToString().Trim();
                string name = this.dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString().Trim();
                if (colunm is DataGridViewButtonColumn)
                {
                    if ("修改操作".Equals(colunm.HeaderText.ToString()))
                    {
                        Form2 f = new Form2(id, code, name);
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.OK)
                        {
                            this.Button2_Click(sender,e);//重新绑定
                        }
                    }
                    else if ("删除操作".Equals(colunm.HeaderText.ToString()))
                    {
                        Jdbc.updateData("delete from person where id ='"+ id + "'");
                        MessageBox.Show("删除成功");
                        this.Button2_Click(sender, e);//重新绑定
                    }
                }
            }
        }


        private void Button2_Click_1(object sender, EventArgs e)
        {
            //弹出新窗口

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                this.Button2_Click(sender, e);//重新绑定
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
