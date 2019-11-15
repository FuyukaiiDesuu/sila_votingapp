using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sila_votingapp
{
    public partial class Form1 : Form
    {
        
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public Form2 reference { get; set; }
        int x,y = 0;
        String file_loc;
        String[] file_names;
        public Form1()
        {
            InitializeComponent();
            x = 0;
            file_loc = System.Configuration.ConfigurationManager.AppSettings["file_location"];
            file_names = Directory.GetFiles(file_loc, "*.jpg");
            y = file_names.Length;
            inserter();
        }
        SqlConnection con = new SqlConnection(GetConnectionString());
        SqlCommand cmd;
        SqlDataReader reader;
        public static string GetConnectionString()
        {
            var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["entriesdbConnectionString"].ConnectionString;
            return conStr.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
        public bool TableisEmpty()
        {
            cmd = new SqlCommand("SELECT COUNT(*) from Entries", con);
            int result = int.Parse(cmd.ExecuteScalar().ToString());
            if(result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public void inserter()
        {
            con.Open();
            if(TableisEmpty())
            {
                for (int z = 0; z < y; z++)
                {
                    try
                    {
                        cmd = new SqlCommand("INSERT into Entries (Id, EntryName) values ('" + z + "','" + Path.GetFileName(file_names[z].Replace(".jpg", "")) + "');", con);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(Path.GetFileName(file_names[z].Replace(".jpg", "")));
                    }
                }
                MessageBox.Show("No Entries added, will proceed to create them", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Entries Already Exist", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(y == 0)
            {
                MessageBox.Show("There are no images to be loaded!", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                button2.Enabled = true;
                if (x == y)
                {
                    MessageBox.Show("YOU HAVE FINISHED VOTING!", "ATTENTION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    reference.Show();
                    this.Dispose();
                }
                else
                {
                    Image image = Image.FromFile(file_loc + Path.GetFileName(file_names[x]));
                    label1.Text = Path.GetFileName(file_names[x].Replace(".jpg", ""));
                    this.pictureBox1.Image = image;
                    x++;
                }
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'entriesdbDataSet.Entries' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'database1DataSet.Entries' table. You can move, or remove it, as needed.

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void addvoter()
        {
            try
            {
                cmd = new SqlCommand("UPDATE Entries SET score = (score + 1) WHERE EntryName = '"+ label1.Text + "'", con);
                cmd.ExecuteNonQuery();
                button1.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.PerformClick();
            button3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            addvoter();
        }
    }
}
