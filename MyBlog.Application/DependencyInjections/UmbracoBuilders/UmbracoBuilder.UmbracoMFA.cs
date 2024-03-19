using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.BackOffice.Security;
using MyBlog.Domain.Models.AppSettingModels;
using Umbraco.Cms.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.ExternalServices.Services;
using Umbraco.Extensions;

namespace MyBlog.Application.DependencyInjections.UmbracoBuilders
{
    public static partial class UmbracoBuilder
    {
        public static IUmbracoBuilder AddCustomUmbracoMFAServices(this IUmbracoBuilder builder, IConfiguration config)
        {
            builder.Services.Configure<UmbracoMFASettings>(config.GetSection(UmbracoMFASettings.UmbracoMFASetting));

            var identityBuilder = new BackOfficeIdentityBuilder(builder.Services);

            identityBuilder.AddTwoFactorProvider<UmbracoUserAppAuthenticator>(UmbracoUserAppAuthenticator.Name);

            builder.Services.Configure<TwoFactorLoginViewOptions>(UmbracoUserAppAuthenticator.Name, options =>
            {
                options.SetupViewPath = "..\\App_Plugins\\TwoFactorProviders\\twoFactorProviderGoogleAuthenticator.html";
            });

            return builder;
        }
    }
}
