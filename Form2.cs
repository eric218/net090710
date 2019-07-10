using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        bool updateFalg = false;
        public Form1 form1;
        private String id;
        private string personCode; private string personName1;
        public Form2(String id, string personCode, string personName1)
        {
            
            updateFalg = true;
            this.id = id;
            this.personCode = personCode;
            this.personName1 = personName1;
            InitializeComponent();
        }
        public Form2()
        {
            
            updateFalg = false;
            InitializeComponent();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if (updateFalg)
            {
                this.button1.Text = "修改";
                this.textBox1.Text = id.ToString();
                this.textBox2.Text = personCode;
                this.textBox3.Text = personName1;
            }
            else{
                this.button1.Text = "添加";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (updateFalg)
            {
                this.update();
            }
            else
            {
                this.add();
            }
               
            this.DialogResult = DialogResult.OK;
        }

        private  void update()
        {
            Jdbc.updateData("update person set PERSONCODE='" + this.textBox2.Text + "',personName1='" + this.textBox3.Text + "' where id='" + this.textBox1.Text + "'");
            MessageBox.Show("修改成功");
        }

        private void add()
        {
            Jdbc.updateData("INSERT INTO test.person(personcode,personname1) values('"+ this.textBox2.Text + "','"+ this.textBox3.Text + "')");
            MessageBox.Show("添加成功");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
