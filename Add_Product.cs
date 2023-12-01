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
    public partial class Add_Product : Form
    {
        public Add_Product()
        {
            InitializeComponent();
        }

        private void Add_Product_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e) // back
        {
            this.Hide();
            Form f = new Options_A();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e) //add button
        {
            bool check = true;

            if (check == true)
            {
                if (string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(description.Text) || string.IsNullOrEmpty(quantity.Text) || string.IsNullOrEmpty(price.Text))
                {
                    check = false;
                    MessageBox.Show("Please fill all fields");
                }
            }

            try
            {
                if (check == true)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Products(Name,Description,Price,StockQuantity,Status_Product,Created_At, Category) VALUES (@Name,@Description, @Price,@Stock_Quantity, @Status, @created, @category)", con);

                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Description", description.Text);
                    cmd.Parameters.AddWithValue("@Price", int.Parse(price.Text));
                    cmd.Parameters.AddWithValue("@Stock_Quantity", int.Parse(quantity.Text));
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@created", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@updated", null);

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


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");

                    this.Hide();
                    Form f = new Options_A();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
