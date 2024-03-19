namespace MyBlog.Domain.Models.AppSettingModels
{
    public class UmbracoMFASettings
    {
        public const string UmbracoMFASetting = "UmbracoMFASettings";

        public string Issuer { get; set; }
    }
}
