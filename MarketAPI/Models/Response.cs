namespace MarketAPI.Models
{
    public class Response
    {
        // This is the response message we want our database server to provide us as reponse

        // Response starts with a code
        public int statusCode {  get; set; }

        // Response has a message
        public string messageCode { get; set; }

        // Response can have only one entity (Student, Fruit, etc) from the database
        public Market market { get; set; }

        // Response can have list of fruits from the database

        public List<Market> markets {  get; set; }
    }
}
