using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Models.ViewComponentModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace MyBlog.Application.ViewComponents
{
    [ViewComponent]
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILogger<HeaderViewComponent> logger;
        private readonly IUmbracoContextAccessor context;
        public HeaderViewComponent(ILogger<HeaderViewComponent> logger, IUmbracoContextAccessor context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            HeaderView headerView = new();
            try
            {
                var content = context?.GetRequiredUmbracoContext()?.PublishedRequest?.PublishedContent;

                if (content is null) return View(headerView);

                headerView.Title = content?.Name;
                headerView.SubTitle = content?.Value<string>("subTitle");
                headerView.Imageurl = content?.Value<IPublishedContent>("pageBanner")?.Url();
                headerView.IsBlog = content?.IsDocumentType("post");
                headerView.CreatedBy = content?.CreatorName();
                headerView.AuthorUrl = content?.Value<IPublishedContent>("authorPageUrl")?.Url();
                headerView.CreatedDate = content?.CreateDate.ToString("D");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing {nameof(HeaderViewComponent)}");
            }
            return View(headerView);
        }
    }
}
