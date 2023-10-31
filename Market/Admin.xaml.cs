using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Market
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            string connectionString = "Data Source=MSI;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            con.Close();


        }

        static SqlConnection con; // It is the connection adapter
        static SqlCommand cmd; // It is the query proceeding adapter


        private void Select_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Generate the query
                String Query = "Select * from MarketProducts where prodID=@ID";

                // Step 3: Generate the command for SQL
                cmd = new SqlCommand(Query, con);

                // Pass the ID parameter value
                cmd.Parameters.AddWithValue("@ID", int.Parse(prodID.Text));

                // Step 4: Creating the Data Adapter to get the values properly
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Step 5: We are going to fill the textboxes with the retrieved information

                if (dt.Rows != null)
                {
                    prodName.Text = dt.Rows[0]["prodName"].ToString();
                    prodID.Text = dt.Rows[0]["prodID"].ToString();
                    prodAmt.Text = dt.Rows[0]["prodAmt"].ToString();
                    prodPrice.Text = dt.Rows[0]["prodPrice"].ToString();
                }
                else
                {
                    MessageBox.Show("Market Products haven't been registered yet");
                }

                // Step 6: Close connection
                con.Close();


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Insert_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // step 1; Is to open the connection
                con.Open();

                // step 2; generaet the database query
                string Query = "Insert into MarketProducts values(@name, @ID, @amount, @price)";

                // step 3; Create the command for Databse
                cmd = new SqlCommand(Query, con);

                // step 4; Assign valeus to the query valuables
                cmd.Parameters.AddWithValue("@name", prodName.Text);
                cmd.Parameters.AddWithValue("@ID", prodID.Text);
                cmd.Parameters.AddWithValue("@amount", prodAmt.Text);
                cmd.Parameters.AddWithValue("@price", double.Parse(prodPrice.Text));

                // step 5; Execute the Command/Query
                cmd.ExecuteNonQuery();

                // Step 6; Successful Message
                MessageBox.Show("Insertion is succesful");

                //Step 7; Close the connection
                con.Close();


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            //  Update the information from the WPF 
            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Generate the Query
                string Query = "Update MarketProducts set prodName=@name, " +
                "prodAmt=@amount,  prodPrice=@price where prodID=@ID";

                // Step 3: Generate the SQL command
                cmd = new SqlCommand(Query, con);

                // Step 4: Passing the updated values
                cmd.Parameters.AddWithValue("@name", prodName.Text);
                cmd.Parameters.AddWithValue("@ID", prodID.Text);
                cmd.Parameters.AddWithValue("@amount", prodAmt.Text);
                cmd.Parameters.AddWithValue("@price", double.Parse(prodPrice.Text));

                // Step 5: Executing the Query/Command
                int i = cmd.ExecuteNonQuery();

                // If cmd executed succesful, it returns 1, else 0
                if (i == 1)

                {
                    // Step 6: Message to user
                    MessageBox.Show("Information updated");
                }

                // Step 7: Close the connection
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Step 1: Open the connection
                con.Open();

                // Step 2: Generate the Query
                string Query = "Delete from MarketProducts where prodID=@ID";

                // Step 3: Generate the command
                cmd = new SqlCommand(Query, con);

                // Step 4: Pass the parameter's value
                cmd.Parameters.AddWithValue("@ID", int.Parse(prodID.Text));

                // Step 5: Execute the query
                int i = cmd.ExecuteNonQuery();

                if (i == 1)
                {
                    MessageBox.Show("Entry Deleted");
                }
                else
                {
                    MessageBox.Show("Check the ID Properly");
                }

                // Step 6: Close the connection
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void show_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Step 1: Open the connection
                con.Open();

                // Step 2; Create the select Query
                string Query = "Select * from MarketProducts";

                // Step 3; Create the command to excecute
                cmd = new SqlCommand(Query, con);

                // Step 4; Prepare the data for datagrid
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Step 5; Update the dataGrid itemSource
                dbGrid.ItemsSource = dt.AsDataView();

                // Step 6; Bind the data inthe wpf frontend
                DataContext = da;

                // Step 7; Close the connection
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sales_btn_Click(object sender, RoutedEventArgs e)
        {
            Sales salesWindow = new Sales();
            salesWindow.Show();
        }
    }
}
