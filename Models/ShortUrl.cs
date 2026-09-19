namespace URL_Shortener_API.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalURL { get; set; }
        public string ShortURL { get; set; }
        public int Status { get; set; }
        public int NumberOfClicks { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
