using Microsoft.AspNetCore.Builder;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.Extensions;

namespace MyBlog.Application.DependencyInjections.UmbracoEndpointBuilders;

public static class UmbracoEndpointBuilder
{
    public static IUmbracoEndpointBuilderContext UseCustomRouting(this IUmbracoEndpointBuilderContext context)
    {
        if (!context.RuntimeState.UmbracoCanBoot())
            return context;

        context.EndpointRouteBuilder.MapControllerRoute(
                           "Admin",
                           "umbraco/backoffice/Feedbacks",
                           new { Controller = "Feedback", Action = "Index" });

        return context;
    }
}
