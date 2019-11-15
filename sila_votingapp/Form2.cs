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
    public partial class Form2 : Form
    {
        int x, y = 0;
        String file_loc;
        String file_write_loc;
        String[] file_names;
        public Form2()
        {
            InitializeComponent();
            file_loc = System.Configuration.ConfigurationManager.AppSettings["file_location"];
            file_write_loc = System.Configuration.ConfigurationManager.AppSettings["file_write_location"];
        }
        public Form1 Form1;
        public void imageloaddetails()
        {
            try
            {
                file_names = Directory.GetFiles(file_loc, "*.jpg");
                y = file_names.Length;
            }
            catch (Exception ee)
            {
                
            }
            if (y > 0)
            {
                MessageBox.Show("Found (" + y + ") image/s", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Found (0) image/s please check file location!", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            imageloaddetails();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 = new Form1();
            Form1.Show();
            Form1.reference = this;
            this.Hide();
        }
        public Form3 Form3;

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will close the program, proceed?", "Attention!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 = new Form3();
            Form3.Show();
            Form3.reference = this;
            this.Hide();

        }
    }
}
