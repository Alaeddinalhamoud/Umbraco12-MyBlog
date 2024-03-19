using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Models.ViewComponentModels;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Umbraco10Blog.App_Code.ViewComponents
{
    [ViewComponent]
    public class NavBarViewComponent : ViewComponent
    {
        private readonly ILogger<NavBarViewComponent> logger;
        private readonly UmbracoHelper umbracoHelper;

        public NavBarViewComponent(ILogger<NavBarViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            this.logger = logger;
            this.umbracoHelper = umbracoHelper;
        }
        public IViewComponentResult Invoke()
        {
            NavbarView navbarView = new();
            try
            {
                var homePage = umbracoHelper?.ContentAtRoot()?.FirstOrDefault(x => x.IsDocumentType("home") && x.IsVisible()) as Home;

                if (homePage == null) return View(navbarView);

                navbarView.SiteName = homePage?.SiteName;
                navbarView.LogoUrl = homePage?.Logo?.Url();

                foreach (var item in homePage.Children)
                {
                    if (item?.Value<bool>("displayOnNavbar") ?? false)
                    {
                        navbarView.NavbarChildren.Add(
                       new NavbarChild
                       {
                           Name = item?.Name,
                           Url = item?.Url()
                       }
                       );
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing {nameof(NavBarViewComponent)}");
            }
            return View(navbarView);
        }
    }
}
