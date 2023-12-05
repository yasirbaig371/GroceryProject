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
    public partial class OrdersDetails : Form
    {
        int OrderID, contact;
        string Fname, home, Pname;
        public OrdersDetails()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // back
        {
            this.Hide();
            Form f = new Options_A();
            f.Show();
        }

        private void OrdersDetails_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd = new SqlCommand("select OP.OrderId, P.FirstName, P.Contact, P.Home, Pro.Name from OrdersPlaced as OP join Person as P on P.Id = OP.Person_Id join Cart as C on P.Id = C.Person_Id join CartItems as CI on CI.Cart_ID = C.CartId join Products as Pro on Pro.ProductId = CI.Product_ID where OP.Order_Status = 8", con);
            SqlCommand cmd = new SqlCommand("select * from OrdersPlaced where Order_Status != 8", con);
            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.Columns.Count && e.RowIndex >= 0)
            {
                var clickedColumn = dataGridView1.Columns[e.ColumnIndex];

                // Check if the clicked column is the button column
                if (clickedColumn != null && clickedColumn.Name == "Confirm")
                {
                    //get values in variables 
                    OrderID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Fname = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    contact = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    home = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Pname = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                    UpdateStatus();

                }
            }
        }

        public void UpdateStatus()
        {
            int oid = 0, pid = 0, ostatus = 8,cid = 0;
            DateTime date = DateTime.Now;
            var con1 = Configuration.getInstance().getConnection();
            
            SqlCommand cmd = new SqlCommand("SELECT OrderId, Person_Id, Cart_Id, DateofOrder, Order_Status from OrdersPlaced", con1);
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
              
                    oid = reader.GetInt32(0); 
                    pid = reader.GetInt32(1);
                    cid = reader.GetInt32(2);
                    date = reader.GetDateTime(3);
                    ostatus = reader.GetInt32(4);
                            
                }
            }

            var con2 = Configuration.getInstance().getConnection();

            SqlCommand cmd2 = new SqlCommand($"update OrdersPlaced set Person_Id = @pid, Cart_Id = @cid, DateofOrder = @date, Order_Status = @status where OrderId = @oid", con2);
            cmd2.Parameters.AddWithValue("@oid", oid); 
            cmd2.Parameters.AddWithValue("@pid", pid);
            cmd2.Parameters.AddWithValue("@cid", cid);
            cmd2.Parameters.AddWithValue("@date", date);
            cmd2.Parameters.AddWithValue("@status", 9);

            cmd2.ExecuteNonQuery(); // doesnt update

            MessageBox.Show("Order confirmed");
            reload();

        }

        public void reload()
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.ReadOnly = true;

            int totalRowHeight;

            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            totalRowHeight = dataGridView1.ColumnHeadersHeight;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                totalRowHeight += row.Height;

            var con = Configuration.getInstance().getConnection();
            //SqlCommand cmd = new SqlCommand("select OP.OrderId, P.FirstName, P.Contact, P.Home, Pro.Name from OrdersPlaced as OP join Person as P on P.Id = OP.Person_Id join Cart as C on P.Id = C.Person_Id join CartItems as CI on CI.Cart_ID = C.CartId join Products as Pro on Pro.ProductId = CI.Product_ID where OP.Order_Status = 8", con);
            SqlCommand cmd = new SqlCommand("select * from OrdersPlaced", con);

            cmd.Connection = con;
            SqlDataReader sqlData = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlData);
            dataGridView1.DataSource = dataTable;
        }

    }
}
