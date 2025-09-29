namespace Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public int EpisodeId { get; set; }
        public string Director { get; set; } = default!;
        public string Producer { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        public string OpeningCrawl { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
