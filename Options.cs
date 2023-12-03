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

namespace E_mart_Project.User_Options
{
    public partial class Options : Form
    {
        public int user_id;
        public int cart_Id;

        public Options(int X, int id)
        {
            InitializeComponent();
            user_id = X;
            cart_Id = id;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "View all Products")
            {
                this.Hide();
                Form f = new ViewProducts(user_id, cart_Id);
                f.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Show();
        }
    }
}
