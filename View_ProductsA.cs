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

namespace E_mart_Project.Admin_Auth
{
    public partial class View_ProductsA : Form
    {
        public int ID, category_table, price_table, quantity_table, status_table;
        public string name_table, des_table;
        public string created_table;

        private void button3_Click(object sender, EventArgs e) //update
        {
            if (name_table == null || des_table == null || quantity_table == 0)
            {
                MessageBox.Show("Select a record");
            }
            if (name_table != null || des_table != null || quantity_table != 0)
            {
                try
                {
                    Form f = new Update_Product(ID, name_table, des_table, price_table, quantity_table, status_table, category_table, created_table); // pass variables here
                    this.Hide();
                    f.Show();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        public View_ProductsA()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button7_Click(object sender, EventArgs e) // back
        {
            this.Hide();
            Form f = new Options_A();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e) // reload
        {
            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select P.ProductId, P.Name, P.Description , P.Price , P.StockQuantity, P.Status_Product, LU.Value, P.Created_At \r\n from Products as P \r\n join Lookup as LU \r\n on LU.Id = P.Category where P.Status_Product = 1");
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e) // delete
        {
            if (price_table != 0)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("update Products " +
                        " set Name = @Name, Description = @Description, Price = @Price, StockQuantity = @quantity, Status_Product = @status, Created_At = @created, Updated_At = @updated, Category = @category where ProductId = @ID ", con);

                    cmd.Parameters.AddWithValue("@Id", ID);
                    cmd.Parameters.AddWithValue("@Name", name_table);
                    cmd.Parameters.AddWithValue("@Description", des_table);
                    cmd.Parameters.AddWithValue("@Price", price_table);
                    cmd.Parameters.AddWithValue("@quantity", quantity_table);
                    cmd.Parameters.AddWithValue("@status", 2);
                    cmd.Parameters.AddWithValue("@created", DateTime.Parse(created_table));
                    cmd.Parameters.AddWithValue("@category", category_table);
                    cmd.Parameters.AddWithValue("@updated", DateTime.Now);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully deleted");

                }

                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

            else
            {
                MessageBox.Show("Select a record to delete");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            name_table = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            des_table = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            price_table = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            quantity_table = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            status_table = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            string x = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

            if (x == "Bakery")
            {
                category_table = 3;
            }
            else if (x == "Beverage")
            {
                category_table = 13;
            }
            else if (x == "Beauty")
            {
                category_table = 4;
            }
            else if (x == "Snacks")
            {
                category_table = 5;
            }
            else if (x == "Fruits")
            {
                category_table = 6;
            }
            else if (x == "Vegetable")
            {
                category_table = 7;
            }

            created_table = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();


            MessageBox.Show("row data received");
        }
    }
}
