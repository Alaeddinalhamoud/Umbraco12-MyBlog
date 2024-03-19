using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.BackOffice.Security;
using MyBlog.Application.ExternalServices.Services;
using MyBlog.Domain.Models.AppSettingModels;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MyBlog.Application.DependencyInjections.Composers
{
    public class UmbracoAppAuthenticatorComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.Configure<UmbracoMFASettings>(builder.Config.GetSection(UmbracoMFASettings.UmbracoMFASetting));

            var identityBuilder = new BackOfficeIdentityBuilder(builder.Services);

            identityBuilder.AddTwoFactorProvider<UmbracoUserAppAuthenticator>(UmbracoUserAppAuthenticator.Name);

            builder.Services.Configure<TwoFactorLoginViewOptions>(UmbracoUserAppAuthenticator.Name, options =>
            {
                options.SetupViewPath = "..\\App_Plugins\\TwoFactorProviders\\twoFactorProviderGoogleAuthenticator.html";
            });
        }
    }
}
