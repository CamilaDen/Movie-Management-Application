namespace Models.DTOs
{
    public class FilmProperties
    {
        public string Title { get; set; } = string.Empty;
        public int Episode_Id { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Producer { get; set; } = string.Empty;
        public string Release_Date { get; set; } = string.Empty;
        public string Opening_Crawl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
