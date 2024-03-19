using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.DependencyInjection;
using MyBlog.Application.ExternalServices.Services.Notifications;

namespace MyBlog.Application.DependencyInjections.UmbracoBuilders
{
    public static partial class UmbracoBuilder
    {
        public static IUmbracoBuilder AddCustomNotificationServices(this IUmbracoBuilder builer)
        {
            builer.AddNotificationHandler<ContentPublishingNotification, GoogleSearchAPIKeyNotification>();
            builer.AddNotificationAsyncHandler<UmbracoApplicationStartedNotification, RunCustomDBContextMigrationNotification>();
            return builer;
        }
    }
}
