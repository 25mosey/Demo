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

namespace De
{
    public partial class Form2 : Form
    {
        private string foto,path;
        public bool ForEdit { get; set; }
        public Form2 ()
        {
            InitializeComponent();
        }
        public string Foto { get; set; }

        private void button1_Click ( object sender, EventArgs e )
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                pictureBox1.Image = Image.FromFile(path);
            }
        }

        private void button2_Click ( object sender, EventArgs e )
        {
            if (ForEdit == false)
            {
                string newPath = Environment.CurrentDirectory + @"\agents\" + foto;
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                }
                Foto = @"\agents\" + foto;
            }
        }
        public void LoadFoto(string path)
        {
            pictureBox1.Image = Image.FromFile(Environment.CurrentDirectory+path);
        }
    }
}
