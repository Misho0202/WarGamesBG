using MessagePack;


namespace WarGamesBG.Models.DTO
{
    [MessagePackObject]

 
    public record Game : ICacheItem<string>
    {
        [Key(0)]
        public string Id { get; set; }

        [Key(1)]
        public string Title { get; set; }


        [Key(2)]
        public int Year { get; set; }
        [Key(3)]
        public List<string> PublisherIds { get; set; }

        [Key(4)]
        public List<string> Sellers { get; set; }

        [Key(5)]
        public List<string> Publishers { get; set; }
        [Key(6)]
        public DateTime DateInserted { get; set; }

        [Key(7)]
        public DateTime? ReleaseDate { get; set; }

        
        public string GetKey()
        {
            return Id;
        }
    }
}
