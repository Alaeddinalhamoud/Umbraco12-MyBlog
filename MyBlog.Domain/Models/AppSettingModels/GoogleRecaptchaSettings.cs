namespace MyBlog.Domain.Models.AppSettingModels;

public class GoogleRecaptchaSettings
{
    public const string GoogleRecaptchaSetting = "GoogleRecaptchaSettings";
    public required string SiteKey { get; set; }
    public required string SecretKey { get; set; }
}
