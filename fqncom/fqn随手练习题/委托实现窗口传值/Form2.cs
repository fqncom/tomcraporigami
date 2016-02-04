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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Action<string> _ac;
        public Form2(string msg,Action<string> ac)
            : this()
        {
            textBox1.Text = msg;
            this._ac = ac;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._ac(textBox1.Text);
            this.Close();
        }




    }
}
