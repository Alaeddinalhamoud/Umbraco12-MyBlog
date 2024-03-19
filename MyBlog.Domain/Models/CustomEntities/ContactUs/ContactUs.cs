using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Domain.Models.CustomEntities.ContactUs
{
    public class ContactUs
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public required string Email { get; set; }

        public required string Subject { get; set; }

        public required string Message { get; set; }

        [NotMapped]
        public string? GoogleRecaptchaToken { get; set; }

        [NotMapped]
        public string? Errors { get; set; }
    }
}
