namespace URL_Shortener_API.DTOs
{
    public class CreateShortUrlRequest
    {
        public string OriginalUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
