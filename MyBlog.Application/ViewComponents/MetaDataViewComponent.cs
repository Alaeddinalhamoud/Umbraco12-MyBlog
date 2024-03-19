using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Models.ViewComponentModels;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace MyBlog.Application.ViewComponents
{
    [ViewComponent]
    public class MetaDataViewComponent:ViewComponent
    {
        private readonly ILogger<MetaDataViewComponent> logger;
        private readonly IUmbracoContextAccessor context;

        public MetaDataViewComponent(ILogger<MetaDataViewComponent> logger, IUmbracoContextAccessor context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            MetaDataView metaDataView = new(); 
            try
            {
                var content = context?.GetRequiredUmbracoContext()?.PublishedRequest?.PublishedContent;

                if (content == null) return View(metaDataView);

                metaDataView.Author = content?.Value<string>("author");
                metaDataView.Description = content?.Value<string>("description");
                metaDataView.Title = content?.Value<string>("title");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error While Processing {nameof(MetaDataViewComponent)}");
            }
            return View(metaDataView);
        }
    }
}
