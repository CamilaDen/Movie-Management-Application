using Models.Enums;

namespace Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public int? ExternalId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int EpisodeId { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Producer { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string OpeningCrawl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MovieStatus Status { get; set; } = MovieStatus.Active;
    }
}
