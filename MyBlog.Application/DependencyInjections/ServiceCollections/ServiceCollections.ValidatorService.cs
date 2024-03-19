using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.Validators;
using MyBlog.Domain.Models.CustomEntities.ContactUs;

namespace MyBlog.Application.DependencyInjections.ServiceCollections;

public static partial class ServiceCollections
{
    public static IServiceCollection AddCustomValidatorService(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ContactUs>, ContactUsValidator>();

        return services;
    }
}
