namespace Models.DTOs
{
    public class SwapiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public List<T> Result { get; set; } = new();
    }
}
