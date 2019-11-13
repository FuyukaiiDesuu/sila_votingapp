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

namespace sila_votingapp
{
    public partial class Form1 : Form
    {
        public Form2 reference { get; set; }
        int x,y;
        String file_loc;
        String[] file_names;
        public Form1()
        {
            InitializeComponent();
            x = 0;
            file_loc = System.Configuration.ConfigurationManager.AppSettings["file_location"];
            file_names = Directory.GetFiles(file_loc, "*.jpg");
            y = file_names.Length;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(x == y)
            {
                MessageBox.Show("YOU HAVE FINISHED VOTING!", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reference.Show();
                this.Dispose();
            }
            else
            {
                Image image = Image.FromFile(file_loc + Path.GetFileName(file_names[x]));
                this.pictureBox1.Image = image;
                x++;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Path.GetFileName(file_names[0] + " " + y), "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
