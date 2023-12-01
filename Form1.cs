using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_mart_Project.User_Auth;

namespace E_mart_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e) // register button
        {
            this.Hide();
            Form f = new Register();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e) //login button
        {
            this.Hide();
            Form f = new Login();
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // admin 
        {

        }
    }
}
