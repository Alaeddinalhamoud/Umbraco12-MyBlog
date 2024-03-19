using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Models.Pagination;
using MyBlog.Domain.Models.ViewComponentModels;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace MyBlog.Application.ViewComponents
{
    [ViewComponent]
    public class BlogViewComponent : ViewComponent
    {
        private readonly ILogger<BlogViewComponent> logger;
        private readonly UmbracoHelper umbracoHelper;

        public BlogViewComponent(ILogger<BlogViewComponent> logger, UmbracoHelper umbracoHelper)
        {
            this.logger = logger;
            this.umbracoHelper = umbracoHelper;
        }
        public IViewComponentResult Invoke(int? page = 0)
        {
            List<BlogView> blogView = new();
            int pageSize = 1;

            try
            {
                var content = umbracoHelper?.ContentAtRoot()?
                    .FirstOrDefault(x => x.IsDocumentType("home"))?
                    .Children()?
                    .FirstOrDefault(x => x.IsVisible()
                    && x.IsDocumentType("blog")) as Blog;

                if (content == null) return View(blogView);

                pageSize = content?.NumberOfBlogs?.Equals(0) ?? false ?
                                    pageSize :
                                    Convert.ToInt32(content?.NumberOfBlogs);

                foreach (Post item in content?.Children() ?? new List<Post>())
                {
                    blogView?.Add(new BlogView
                    {
                        Title = item?.Name,
                        SubTitle = item?.SubTitle,
                        BlogUrl = item?.Url(),
                        CreatedBy = item?.CreatorName(),
                        AuthorUrl = item?.AuthorPageUrl?.Url(),
                        CreatedDate = item?.CreateDate.ToString("D")
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing {nameof(BlogViewComponent)}");
            }

            return View(PaginatedList<BlogView>.Create(blogView.AsQueryable(), page ?? 1, pageSize));
        }
    }
}
