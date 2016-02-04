using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 事件实现窗口传值
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //方法二：事件实现窗体传值，在窗体加载的时候就给按钮注册一个事件
            button1.Click += (sender1, e1) =>
            {
                Form2 f2 = new Form2(textBox1.Text, getText);
                f2.Show();
            };
        }

        public void getText(string text)
        {
            textBox1.Text = text;
        }
    }
}
