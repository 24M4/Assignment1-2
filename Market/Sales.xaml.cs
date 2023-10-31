using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
            string connectionString = "Data Source=MSI;Initial Catalog=master;Integrated Security=True";
            con = new SqlConnection(connectionString);
            con.Open();
            con.Close();
            
        }

        static SqlConnection con; // It is the connection adapter
        static SqlCommand cmd; // It is the query proceeding adapter


        private void ProductPrices()
        {
            productPrices = new Dictionary<string, double>
            {
                { "Apple", 2.10 },
                { "Orange", 2.49 },
                { "Raspberry", 2.35 },
                { "Blueberry", 1.45 },
                { "Cauliflower", 2.22 }
            };
        }

        private void Calculate(string prodName, double prodAmt)
        {
            ProductPrices();

            // Calculate total sales amount
            double prodPrice = productPrices[prodName];
            double totalSalesAmount = prodPrice * prodAmt;

            // Update inventory in the database
            UpdateInventory(prodName, prodAmt);

            // Display total sales amount
            TotalSalesAmount.Text = totalSalesAmount.ToString("C");
        }

        private void UpdateInventory(string prodName, double prodAmt)
        {
            //  Update the information from the WPF 
            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Generate the Query
                string Query = "UPDATE MarketProducts SET prodAmt = prodAmt - @amount WHERE prodName = @name";

                // Step 3: Generate the SQL command
                cmd = new SqlCommand(Query, con);

                // Step 4: Passing the updated values
                cmd.Parameters.AddWithValue("@name", prodName);
                cmd.Parameters.AddWithValue("@amount", prodAmt);

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

        private void calculateTotal_Click(object sender, RoutedEventArgs e)
        {
            prodPrice = prodAmt * productPrices;
        }

        private void purchase_btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateInventory();
        }
    }
}
