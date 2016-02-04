using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllToOne_Image
{
    public partial class Form1 : Form
    {

        public Bitmap Bitmap { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.FileOk += (s, e1) => { this.txtFilePath.Text = fileOpen.FileName.Substring(0, fileOpen.FileName.LastIndexOf('\\')); };
            fileOpen.ShowDialog();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var rowCount = Convert.ToInt32(txtRow.Text);
            var columnCount = Convert.ToInt32(txtColumn.Text);

            var oneWidth = Convert.ToInt32(txtOneWidth.Text);
            var oneHeitht = Convert.ToInt32(txtOneHeight.Text);

            int bitmapWidth = columnCount * oneWidth;
            int bitmapHeight = rowCount * oneHeitht;
            Bitmap = new Bitmap(bitmapWidth, bitmapHeight);

            Graphics gri = Graphics.FromImage(Bitmap);

            var fileNames = Directory.GetFiles(txtFilePath.Text);
            int rowIndex = 0;
            int columnIndex = 0;
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (i != 0 && i % columnCount == 0)
                {
                    rowIndex++;
                    columnIndex = 0;
                }
                Image image = Image.FromFile(fileNames[i]);
                gri.DrawImage(image, new Rectangle(oneWidth * columnIndex, oneHeitht * rowIndex, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                columnIndex++;
            }
            picPreview.Image = Bitmap;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileSave = new SaveFileDialog();
            fileSave.Filter = "|*.png";
            fileSave.ShowDialog();
            Bitmap.Save(fileSave.FileName);
        }
    }
}
