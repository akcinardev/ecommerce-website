namespace EcommerceMVC.Models
{
    public class Comment
    {
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
