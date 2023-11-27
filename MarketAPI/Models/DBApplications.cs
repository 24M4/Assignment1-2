using Npgsql;
using System.Data;

namespace MarketAPI.Models
{
    public class DBApplication
    {
        public Response GetAllInformation(NpgsqlConnection con)
        {
            // Step 1: Create instance of the Response class
            Response response = new Response();

            // Step 2: Create the query
            string Query = "Select * from MarketProducts";

            // Step 3: Need data adapter to read the data from database and a table structure to add it into the table.
            NpgsqlConnection da = new NpgsqlDataAdapter(Query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Response reponse = new Response();

            // Step 4: Initialize the list of students variable
            List<Market> MarketInformation = new List<Market>();

            // Step 5: Verify the database query retrived in DT
            if (dt.Rows.Count > 0) // If rows is more then zero              
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Market markets = new Market(); // To capture each entry in the table

                    markets.prodName = (String)dt.Rows[i]["prodName"];
                    markets.ProdID = (int)dt.Rows[i]["ProdID"];
                    markets.prodAmt = (int)dt.Rows[i]["prodAmt"];
                    markets.prodPrice = (int)dt.Rows[i]["prodPrice"];

                    MarketInformation.Add(markets);
                }
            }


            // Step 6: verify the list of the students and configure response
            if (MarketInformation.Count > 0)
            {
                response.statusCode = 200;
                response.messageCode = "Succesful";
                response.markets = null;
                response.MarketInformation = MarketInformation;
            }
            else
            {
                response.statusCode = 100;
                response.messageCode = "Data failed to retrieve";
                response.market = null;
                response.markets = null;
            }
            return response;
        }


    }
}
