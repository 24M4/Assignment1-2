using MarketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace RestAPIMarket.Controllers


{
    // >> Need to add the base controller name over here, before the controller executes
    [Route("[controller]")]
    [ApiController] // Characteristics: what type of controller is it
    public class MarketController : ControllerBase



    {
        // this class object helps to create a state receiver in the program
        // this will hold the connection information from hte remote database server to the local / client
        private readonly IConfiguration _configuration;

        // MarketController Constructor
        MarketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Now our target is to create out first API wrapper for the database itself.
        // We are going to generate the GetAllMembers API

        [HttpGet] // the GetAllInformation API is going ot geterate a get request
        [Route("GetAllInformation")] // Give API a name / path

        // Now we need ot create GetAllInformation API
        // When we create API, we always get hte query response from the remote server to catch that properly,
        // we need a response class which can get all the information as well as the response methods
        // that means, we need to create a response model and a information model based on the data they are retrieving.

        // Create the API method
        public Response GetAllInformation() {

            // Step 1: Creating Response Object
            Response response = new Response();

            // Step 2: Need to create the SQL Connection, with the Rest API Configuration variable,
            // We need to pass the connectionString to the appSettings
            // We need to pass the Configuration to the SQL connection Adapter, for this case, NpgSQLConnection

            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("MarketConnection"));

            // Step 3: Need to query to database with the command and connection, we can do this in a seperate class, name DBApplication

            DBApplication dBApplication = new DBApplication();
            response = dBApplication.GetAllInformation(con);

            // Step L: Returning the Response to the client
            return response;

        }

    }
}
