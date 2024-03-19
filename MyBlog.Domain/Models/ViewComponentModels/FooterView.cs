namespace MyBlog.Domain.Models.ViewComponentModels
{
    public class FooterView
    {
        public List<FooterLink>? FooterLinks { get; set; } = new ();
        public List<SocialMedia>? SocialMedias { get; set; } = new ();
    }
    public class FooterLink
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public string? Target { get; set; }
    }

    public class SocialMedia
    {
        public string?  Icon { get; set; }
        public string? SocialMediaUrl { get; set; }
        public string? Target { get; set; }
    }
}
