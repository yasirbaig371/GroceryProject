﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_mart_Project.Admin_Auth
{
    public partial class Options_A : Form
    {
        public Options_A()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // back
        {
            this.Hide();
            Form f = new LoginA();
            f.Show();
        }

        private void Options_A_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "View Products")
            {
                this.Hide();
                Form f = new View_ProductsA();
                f.Show();
            }
            if (comboBox1.SelectedItem == "Add Product")
            {
                this.Hide();
                Form f = new Add_Product();
                f.Show();
            }
        }
    }
}
