using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace MyBlog.Application.ExternalServices.Services.Notifications
{
    public class GoogleSearchAPIKeyNotification : INotificationHandler<ContentPublishingNotification>
    {
        private readonly ILogger<GoogleSearchAPIKeyNotification> _logger;
        public GoogleSearchAPIKeyNotification(ILogger<GoogleSearchAPIKeyNotification> logger)
        {
            _logger = logger;
        }
        public void Handle(ContentPublishingNotification notification)
        {
            try
            {
                foreach (var node in notification.PublishedEntities)
                {
                    if(node?.ContentType?.Alias?.Equals("search") ?? false)
                    {
                        var googleSearchAPIKey = node.GetValue<string>("googleCustomSearchApiKey");
                        var enableGoogleSearch = node.GetValue<bool>("enableGoogleSearch");

                        if(enableGoogleSearch && string.IsNullOrWhiteSpace(googleSearchAPIKey)) {
                            notification.CancelOperation(new EventMessage("Cancelled", "Please provide the API Key", EventMessageType.Error));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error While processing {nameof(GoogleSearchAPIKeyNotification)}");
            }
        }
    }
}
