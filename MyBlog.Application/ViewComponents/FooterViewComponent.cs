using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Models.ViewComponentModels;
using NUglify.Helpers;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace MyBlog.Application.ViewComponents
{
    [ViewComponent]
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILogger<FooterViewComponent> logger;
        private readonly UmbracoHelper umbracoHelper;
        public FooterViewComponent(ILogger<FooterViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            this.logger = logger;
            this.umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke()
        {
            FooterView footerView = new();
            try
            {
                var homePage = umbracoHelper?.ContentAtRoot()?.FirstOrDefault(x => x.IsDocumentType("home") && x.IsVisible()) as Home;

                if (homePage == null) return View(footerView);

                homePage?.FooterLinks?.ForEach(x => footerView?.FooterLinks?.Add(new FooterLink
                {
                    Name = x.Name,
                    Url = x.Url,
                    Target = x.Target
                }));

                foreach (var item in homePage?.SoicalMediaPicker)
                {
                    var socialMediaElement = item.Content as SocialMediaElement;

                    footerView.SocialMedias?.Add(new SocialMedia
                    {
                        Icon = socialMediaElement?.Icon.ToString(),
                        SocialMediaUrl = socialMediaElement?.SocialMediaUrl,
                        Target = socialMediaElement?.Target
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing {nameof(FooterViewComponent)}");
            }
            return View(footerView);
        }
    }
}
