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
    public partial class LoginA : Form
    {
        public LoginA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Show();
        }

        private void LoginA_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            string name = fname.Text;

            // SQL query to check authentication and retrieve Person.Id and count
            if (int.TryParse(contactText.Text, out int contact))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Person Join Admin_Info as AI on Person.id = AI.Person_Id WHERE FirstName = @Name AND Contact = @Contact AND AI.Designation = 10 ", con);

                // Use parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Contact", contact);

                try
                {
                    // Execute the query and get the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve the count and Person.Id from the reader
                            int count = reader.GetInt32(0); // Index 0 is the count
                            

                            // Now you have both values in variables
                            if (count > 0)
                            {
                                // Authentication successful, proceed with other actions or form navigation
                                //MessageBox.Show($"Authentication successful. Count: {count}, Person ID: {personId}");
                                this.Hide();
                                Form f = new Options_A();
                                f.Show();
                            }
                            else
                            {
                                // Authentication failed, handle accordingly (e.g., show a registration form)
                                MessageBox.Show("Authentication failed.");
                                this.Hide();
                                Form f = new Form1();
                                f.Show();
                            }
                        }
                        else
                        {
                            // Person does not exist in the database
                            MessageBox.Show("Authentication failed.");
                            // Handle accordingly (e.g., show a registration form)
                            this.Hide();
                            Form f = new Form1();
                            f.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
