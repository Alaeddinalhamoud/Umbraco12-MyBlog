using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using MyBlog.Data.CustomDBContexts;

namespace MyBlog.Application.ExternalServices.Services.Notifications
{
    public class RunCustomDBContextMigrationNotification : INotificationAsyncHandler<UmbracoApplicationStartedNotification>
    {
        private readonly CustomDBContext _customDBContexts;

        public RunCustomDBContextMigrationNotification(CustomDBContext customDBContexts) =>
            _customDBContexts = customDBContexts;

        public async Task HandleAsync(UmbracoApplicationStartedNotification notification, CancellationToken cancellationToken)
        {
            IEnumerable<string> pendingMigrations = _customDBContexts.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
                _customDBContexts.Database.Migrate();
        }
    }
}
