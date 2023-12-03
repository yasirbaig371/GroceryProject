using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_mart_Project.User_Options
{
    public partial class Payment : Form
    {
        public int User_id, Cart_id;
        public Payment(int user, int cart)
        {
            InitializeComponent();
            User_id = user;
            Cart_id = cart;
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
