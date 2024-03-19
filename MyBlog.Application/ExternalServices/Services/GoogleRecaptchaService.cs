using Microsoft.Extensions.Options;
using System.Text.Json;
using MyBlog.Application.ExternalServices.IServices;
using MyBlog.Domain.Models.AppSettingModels;
using MyBlog.Domain.Models.ExternalServiceModels.GoogleReCaptchaModels;
using Microsoft.Extensions.Logging;

namespace MyBlog.Application.ExternalServices.Services;

public class GoogleRecaptchaService : IGoogleRecaptchaService
{
    private readonly HttpClient httpClient;
    private readonly IOptions<GoogleRecaptchaSettings> options;
    private readonly ILogger<GoogleRecaptchaService> logger;
    public GoogleRecaptchaService(HttpClient httpClient,
                                  IOptions<GoogleRecaptchaSettings> options,
                                  ILogger<GoogleRecaptchaService> logger)
    {
        this.httpClient = httpClient;
        this.options = options;
        this.logger = logger;
    }
    public async Task<bool> SiteVerify(GoogleReCaptcha googleReCaptcha)
    {
        try
        {
            var httpResponseMessage = await httpClient.GetStringAsync
          ($"https://www.google.com/recaptcha/api/siteverify?secret={options.Value.SecretKey}&response={googleReCaptcha.GoogleRecaptchaToken}");

            var data = JsonSerializer.Deserialize<GoogleReCaptcha>(httpResponseMessage);
            return data.success;
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, $"Error while processing {nameof(GoogleRecaptchaService)}");
            return false;
        }
    }
}
