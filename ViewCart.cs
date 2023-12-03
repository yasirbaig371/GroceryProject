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
    public partial class ViewCart : Form
    {
        public int User_id, Cart_id, P_ID;
        public ViewCart(int user_id, int cart)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            User_id = user_id;
            Cart_id = cart;
        }

        private void button1_Click(object sender, EventArgs e) // back button
        {
            this.Hide();
            Form f = new ViewProducts(User_id, Cart_id);
            f.Show();
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.Columns.Count && e.RowIndex >= 0)
            {
                var clickedColumn = dataGridView1.Columns[e.ColumnIndex];

                // Check if the clicked column is the button column
                if (clickedColumn != null && clickedColumn.Name == "Remove")
                {
                    P_ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    RemoveItemFromCart();
                }
            }
        }

        public void RemoveItemFromCart()
        {
            var con2 = Configuration.getInstance().getConnection();
            SqlCommand cmd2 = new SqlCommand("delete CartItems where Cart_Id = @cart AND Product_Id = @product", con2);
            cmd2.Parameters.AddWithValue("@cart", Cart_id);
            cmd2.Parameters.AddWithValue("@product", P_ID);
            cmd2.ExecuteNonQuery();

            MessageBox.Show("The selected item has been deleted");
            Reload();
        }

        private void button2_Click(object sender, EventArgs e) // proceed to payment 
        {
            var con3 = Configuration.getInstance().getConnection();
            SqlCommand cmd3 = new SqlCommand("INSERT INTO OrdersPlaced(Person_Id, Cart_Id, DateofOrder) VALUES (@user, @cart, @date)", con3);
            cmd3.Parameters.AddWithValue("@user", User_id);
            cmd3.Parameters.AddWithValue("@cart", Cart_id);
            cmd3.Parameters.AddWithValue("@date", DateTime.Now);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Your order has been placed");

            this.Hide();
            Form f = new Payment(User_id, Cart_id);
            f.Show();

        }

        public void Reload()
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from CartItems as CI where CI.Cart_Id = @cart", con);
            cmd.Parameters.AddWithValue("@cart", Cart_id);
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void ViewCart_Load(object sender, EventArgs e) // loads data in table
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from CartItems as CI where CI.Cart_Id = @cart", con);
            cmd.Parameters.AddWithValue("@cart", Cart_id);
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }
    }
}
