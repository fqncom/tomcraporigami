using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 清空所有textbox控件中的内容
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    control.Text = string.Empty;
                }
            }
        }
    }
}
