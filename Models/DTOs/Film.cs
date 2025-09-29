namespace Models.DTOs
{
    public class Film
    {
        public string Uid { get; set; } = string.Empty;
        public FilmProperties Properties { get; set; } = new();
    }
}
