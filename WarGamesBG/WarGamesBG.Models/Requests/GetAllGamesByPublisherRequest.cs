namespace WarGamesBG.Models.Requests
{
    public class AddGameRequest
    {
        public string Title { get; set; }

        public decimal Year { get; set; }

        public List<string> PublisherIds { get; set; }

        
    }
}
