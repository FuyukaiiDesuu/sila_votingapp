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
        public String file_loc = "";
        String appdatastring = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\SILA Voting App";
        String[] file_names;
        public Form2()
        {
            InitializeComponent();
            FileFolderCreator();
        }
        public Form1 Form1;
        public void FileFolderCreator()
        {
            //test
            System.IO.Directory.CreateDirectory(appdatastring);
            if (!File.Exists(appdatastring + "\\imgfileloc.txt"))
            {
                FileInfo f = new FileInfo(appdatastring + "\\imgfileloc.txt"):
                File.Create(appdatastring + "\\imgfileloc.txt").Dispose();
                File.WriteAllText(appdatastring + "\\imgfileloc.txt", "null");
            }
        }
        public void imagepathchecker()
        {
            if (File.ReadLines(appdatastring + "\\imgfileloc.txt").First() == "null")
            {
                MessageBox.Show("There is no filepath specified, could not retrieve image/s, " +
                    "please set image folder path in settings", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                file_loc = File.ReadLines(appdatastring + "\\imgfileloc.txt").First();
                button3.Enabled = true;
                button2.Enabled = false;
            }
        }
        public void imageloaddetails()
        {
            try
            {
                file_names = Directory.GetFiles(file_loc, "*.jpg");
                y = file_names.Length;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            if (y > 0)
            {
                Form1 = new Form1(file_loc);
                Form1.Show();
                Form1.reference = this;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Found (0) image/s please check file location!", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            imagepathchecker();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageloaddetails();
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (File.ReadLines(appdatastring + "\\imgfileloc.txt").First() == "null")
            {
                MessageBox.Show("There is no current path to the images folder ", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {  
                MessageBox.Show("The current path to the images folder is: " + File.ReadLines(appdatastring + "\\imgfileloc.txt").First(), "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "Select Folder Where Images Are Stored";
            fbd.ShowNewFolderButton = false;
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                file_loc = fbd.SelectedPath;
                File.WriteAllText(appdatastring + "\\imgfileloc.txt", file_loc);
                MessageBox.Show("The Selected path is: " + file_loc, "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                file_names = Directory.GetFiles(file_loc, "*.jpg");
                y = file_names.Length;
                MessageBox.Show("Found (" + y + ") image/s", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
