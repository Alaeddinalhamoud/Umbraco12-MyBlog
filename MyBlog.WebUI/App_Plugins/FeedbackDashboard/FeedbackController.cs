using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Filters;
using MyBlog.Data.CustomDBContexts;
using MyBlog.Domain.Models.CustomEntities.ContactUs;

namespace MyBlog.WebUI.App_Plugins.FeedbackDashboard
{
    [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
    [DisableBrowserCache]
    public class FeedbackController : Controller
    {
        private readonly IEFCoreScopeProvider<CustomDBContext> _efCoreScopeProvider;

        public FeedbackController(IEFCoreScopeProvider<CustomDBContext> efCoreScopeProvider)
        {
                _efCoreScopeProvider = efCoreScopeProvider;
        }
        public async Task<IActionResult> Index()
        {
            using IEfCoreScope<CustomDBContext> scope = _efCoreScopeProvider.CreateScope();

            IEnumerable<ContactUs> feedback = await scope.ExecuteWithContextAsync(async db => db.ContactUs.ToArray());

            scope.Complete();
            return View(feedback);
        }
    }
}
