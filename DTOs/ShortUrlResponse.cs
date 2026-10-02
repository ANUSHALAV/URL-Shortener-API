namespace URL_Shortener_API.DTOs
{
    public class ShortUrlResponse
    {
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public int Status { get; set; }
    }
}
