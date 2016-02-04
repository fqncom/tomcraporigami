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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Action<string> _ac;
        public Form2(string text, Action<string> ac)
            : this()
        {
            textBox1.Text = text;
            this._ac = ac;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Click += (sender1, e1) =>
            {
                this._ac(textBox1.Text);
                this.Close();
            };
        }


    }
}
