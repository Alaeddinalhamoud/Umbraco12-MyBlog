namespace MyBlog.Domain.Models.ViewComponentModels
{
    public class HeaderView
    {
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Imageurl { get; set;}
        public bool? IsBlog { get; set; }
        public string? CreatedBy { get; set; }
        public string? AuthorUrl { get; set; }
        public string? CreatedDate { get; set; }
    }
}
