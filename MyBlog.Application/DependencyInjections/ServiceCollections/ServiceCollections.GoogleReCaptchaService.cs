using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.ExternalServices.IServices;
using MyBlog.Application.ExternalServices.Services;
using MyBlog.Domain.Models.AppSettingModels;

namespace MyBlog.Application.DependencyInjections.ServiceCollections;

public static partial class ServiceCollections
{
    public static IServiceCollection AddCustomGoogleReCaptchaService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<GoogleRecaptchaSettings>(config.GetSection(GoogleRecaptchaSettings.GoogleRecaptchaSetting));

        services.AddHttpClient<IGoogleRecaptchaService, GoogleRecaptchaService>();

        return services;
    }
}
