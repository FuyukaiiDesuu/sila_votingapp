using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sila_votingapp
{
    public partial class Form3 : Form
    {
        public Form2 reference { get; set; }
        public Form3()
        {
            InitializeComponent();
        }
        public void tableloader()
        {
            SqlConnection con = new SqlConnection(GetConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Entries;", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public static string GetConnectionString()
        {
            var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["entriesdbConnectionString"].ConnectionString;
            return conStr.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            tableloader();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            reference.Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will purge all data from the table, proceed?", "WARNING!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(GetConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand("TRUNCATE TABLE Entries;", con);
                cmd.ExecuteNonQuery();
                con.Close();
                tableloader();
            }
            else if (dialogResult == DialogResult.No)
            {
                
            }
            
        }
    }
}
