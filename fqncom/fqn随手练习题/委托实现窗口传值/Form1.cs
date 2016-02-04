using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 委托实现窗口传值
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //第一种方法：委托实现
            Form2 f2 = new Form2(textBox1.Text, getTextFromForm2);
            f2.Show();

            //第二种方法：事件实现,在第一个窗体加载的时候就给第一个按钮注册事件

        }

        public void getTextFromForm2(string msg)
        {
            textBox1.Text = msg;
        }

        //第二种方法：事件实现,在第一个窗体加载的时候就给第一个按钮注册事件
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    button1.Click += (sender1, e1) =>
        //    {
        //        Form2 f2 = new Form2(textBox1.Text, getTextFromForm2);
        //        f2.Show();
        //    };
        //}
    }
}
