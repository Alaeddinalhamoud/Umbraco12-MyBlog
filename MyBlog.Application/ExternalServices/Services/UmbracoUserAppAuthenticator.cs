using Google.Authenticator;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using MyBlog.Domain.Models.AppSettingModels;
using MyBlog.Domain.Models.UmbracoMFAModels;
using Umbraco.Extensions;

namespace MyBlog.Application.ExternalServices.Services
{
    public class UmbracoUserAppAuthenticator : ITwoFactorProvider
    {
        private readonly IUserService _userService;
        public const string Name = "MyBlog";
        private readonly IOptions<UmbracoMFASettings> _options;
        public UmbracoUserAppAuthenticator(IUserService userService, IOptions<UmbracoMFASettings> options)
        {
            _userService = userService;
            _options = options;
        }
        public string ProviderName => Name;

        public Task<object> GetSetupDataAsync(Guid userOrMemberKey, string secret)
        {
            var twoFactorAuthenticator = new TwoFactorAuthenticator();

            SetupCode setupInfo = twoFactorAuthenticator.GenerateSetupCode(_options?.Value?.Issuer,
                                  _userService.GetByKey(userOrMemberKey)?.Username,
                                  secret, false);

            return Task.FromResult<object>(new TwoFactorAuthInfo()
            {
                QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl,
                Secret = secret
            });
        }

        public bool ValidateTwoFactorPIN(string secret, string token) =>
           new TwoFactorAuthenticator().ValidateTwoFactorPIN(secret, token);

        public bool ValidateTwoFactorSetup(string secret, string token) => ValidateTwoFactorPIN(secret, token);
    }
}
