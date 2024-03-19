using MyBlog.Domain.Models.ExternalServiceModels.GoogleReCaptchaModels;

namespace MyBlog.Application.ExternalServices.IServices;

public interface IGoogleRecaptchaService
{
    Task<bool> SiteVerify(GoogleReCaptcha googleReCaptcha);
}
