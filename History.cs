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
    public partial class History : Form
    {
        public int user_id, cart_id;
        public History(int user, int cart)
        {
            InitializeComponent();
            user_id = user;
            cart_id = cart;
        }

        private void button1_Click(object sender, EventArgs e) //back
        {
            this.Hide();
            Form f = new Options(user_id, cart_id);
            f.Show();
        }

        private void History_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select OP.OrderId, P.FirstName, P.Contact, P.Home, Pro.Name from OrdersPlaced as OP join Person as P on P.Id = OP.Person_Id join Cart as C on P.Id = C.Person_Id join CartItems as CI on CI.Cart_ID = C.CartId join Products as Pro on Pro.ProductId = CI.Product_ID where P.Id = @pid", con);

            cmd.Parameters.AddWithValue("@pid", user_id);
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }
    }
}
