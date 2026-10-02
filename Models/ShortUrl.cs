namespace URL_Shortener_API.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public int ClickCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }

    }
}
