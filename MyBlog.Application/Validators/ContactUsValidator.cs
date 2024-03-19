using FluentValidation;
using MyBlog.Application.ExternalServices.IServices;
using MyBlog.Domain.Models.CustomEntities.ContactUs;
using MyBlog.Domain.Models.ExternalServiceModels.GoogleReCaptchaModels;

namespace MyBlog.Application.Validators;

public sealed class ContactUsValidator : AbstractValidator<ContactUs>
{
    public ContactUsValidator(IGoogleRecaptchaService googleRecaptchaService)
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("a full name is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("an email required")
            .EmailAddress().WithMessage("email address syntax is not correct");
        RuleFor(x => x.Subject).NotEmpty().WithMessage("a subject is required");
        RuleFor(x => x.Message).NotEmpty().WithMessage("a message is required");

        RuleFor(x => x.GoogleRecaptchaToken)
            .NotEmpty().WithMessage("the recaptch token is required.")
            .Must(token => googleRecaptchaService.SiteVerify(new GoogleReCaptcha { GoogleRecaptchaToken = token! }).Result)
            .WithMessage("Token Invalid");
    }
}
