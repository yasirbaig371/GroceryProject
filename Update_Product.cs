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
    public partial class Update_Product : Form
    {
        public int ID_text, status_text;
        public string created;
        public Update_Product(int ID, string name_table, string des_table, int price_table, int quantity_table, int status_table, int category_table, string created_table)
        {
            InitializeComponent();

            // make ui and receive these texts in textboxes
            status_text = status_table;
            ID_text = ID;
            created = created_table;
            name.Text = name_table;
            description.Text = des_table;
            price.Text = price_table.ToString();
            quantity.Text = quantity_table.ToString();

            if (category_table == 3)
            {
                comboBox1.Text = "Bakery";
            }
            else if (category_table == 13)
            {
                comboBox1.Text = "Beverage";
            }
            else if (category_table == 4)
            {
                comboBox1.Text = "Beauty";
            }
            else if (category_table == 5)
            {
                comboBox1.Text = "Snacks";
            }
            else if (category_table == 6)
            {
                comboBox1.Text = "Fruits";
            }
            else if (category_table == 7)
            {
                comboBox1.Text = "Vegetable";
            }
        }

        private void button2_Click(object sender, EventArgs e) //update button
        {
            if (ID_text != 0)
            {
                try
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("update Products " +
                        " set Name = @Name, Description = @Description, Price = @Price, StockQuantity = @quantity, Status_Product = @status, Created_At = @created, Updated_At = @update ,Category = @category where ProductId = @ID ", con);

                    cmd.Parameters.AddWithValue("@Id", ID_text);
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Description", description.Text);
                    int price_x = int.Parse(price.Text);
                    cmd.Parameters.AddWithValue("@Price", price_x);
                    int quantity_x = int.Parse(quantity.Text);
                    cmd.Parameters.AddWithValue("@quantity", quantity_x);
                    cmd.Parameters.AddWithValue("@status", status_text);
                    cmd.Parameters.AddWithValue("@created", created);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);


                    int cat = 0;
                    if (comboBox1.Text == "Bakery")
                    {
                        cat = 3;
                    }
                    else if (comboBox1.Text == "Beverage")
                    {
                        cat = 13;
                    }
                    else if (comboBox1.Text == "Beauty")
                    {
                        cat = 4;
                    }
                    else if (comboBox1.Text == "Snacks")
                    {
                        cat = 5;
                    }
                    else if (comboBox1.Text == "Fruits")
                    {
                        cat = 6;
                    }
                    else if (comboBox1.Text == "Vegetable")
                    {
                        cat = 7;
                    }
                    cmd.Parameters.AddWithValue("@category", cat);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Successfully updated");
                    this.Hide();
                    Form f = new View_ProductsA();
                    f.Show();

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

        private void Update_Product_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e) // back
        {
            this.Hide();
            Form f = new View_ProductsA();
            f.Show();
        }
    }
}
