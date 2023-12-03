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
    public partial class ViewProducts : Form
    {
        public int User_ID, Cart_Id;
        public int P_ID;
        public ViewProducts(int Id_login, int cart)
        {
            InitializeComponent();
            User_ID = Id_login; // gets user id from login form
            Cart_Id = cart;
        }

        private void ViewProducts_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button7_Click(object sender, EventArgs e) // back button
        {
            this.Hide();
            Form f = new Options(User_ID, Cart_Id);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e) // bakery button
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 3 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e) // beauty
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 4 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e) // beverage
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 13 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e) // fruits
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 6 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button6_Click(object sender, EventArgs e) //snacks
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 5 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button5_Click(object sender, EventArgs e) // vegetable
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select ProductId, Name, Description , Price , Category from Products where Category = 7 AND Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button8_Click(object sender, EventArgs e) // view cart button 
        {
            this.Hide();
            Form f = new ViewCart(User_ID, Cart_Id);
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.Columns.Count && e.RowIndex >= 0)
            {
                var clickedColumn = dataGridView1.Columns[e.ColumnIndex];

                // Check if the clicked column is the button column
                if (clickedColumn != null && clickedColumn.Name == "Add")
                {
                    P_ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()); // gets product id from datagrid
                    InsertIntoCart();

                }
            }
        }

        public void InsertIntoCart()
        {
            var con1 = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("INSERT INTO CartItems(Cart_Id, Product_Id) VALUES (@cart, @product)", con1);
            cmd1.Parameters.AddWithValue("@cart", Cart_Id);
            cmd1.Parameters.AddWithValue("@product", P_ID);

            cmd1.ExecuteNonQuery();
            MessageBox.Show("Product added in cart");

        }




    }

}
