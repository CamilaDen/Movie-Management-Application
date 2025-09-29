namespace Models.DTOs
{
    public class SwapiSingleResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public T Result { get; set; } = default!;
    }
}
