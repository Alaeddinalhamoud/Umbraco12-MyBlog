using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Common.Controllers;
using MyBlog.Data.CustomDBContexts;
using MyBlog.Domain.Models.CustomEntities.ContactUs;
using FluentValidation;
using System.Text.Json;

namespace MyBlog.Application.Controllers
{
    public class ContactUsController : UmbracoApiController
    {
        private readonly IEFCoreScopeProvider<CustomDBContext> _efCoreScopeProvider;
        private IValidator<ContactUs> _validator;

        public ContactUsController(IEFCoreScopeProvider<CustomDBContext> efCoreScopeProvider, IValidator<ContactUs> validator)
        {
            _efCoreScopeProvider = efCoreScopeProvider;
            _validator = validator;
        }            

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using IEfCoreScope<CustomDBContext> scope = _efCoreScopeProvider.CreateScope();

            IEnumerable<ContactUs> feedback = await scope.ExecuteWithContextAsync(async db => db.ContactUs.ToArray());

            scope.Complete();

            return Ok(feedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            using IEfCoreScope<CustomDBContext> scope = _efCoreScopeProvider.CreateScope();
            ContactUs? feedbacks = await scope.ExecuteWithContextAsync(async
                                                  db => db.ContactUs.FirstOrDefault(x => x.Id == id));

            scope.Complete();
            return Ok(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ContactUs feedback)
        {            
            var result = await _validator.ValidateAsync(feedback);

            if (!result.IsValid)
            {
                return BadRequest(JsonSerializer.Serialize(result.Errors));
            }
            
            //GoogleReCaptcha googleReCaptchaResponse = await _googleRecaptchaService
            //        .SiteVerify(new GoogleReCaptcha { GoogleRecaptchaToken = feedback.GoogleRecaptchaToken! });

            //if (!googleReCaptchaResponse.success)
              //  return new BadRequestResult();

            using IEfCoreScope<CustomDBContext> scope = _efCoreScopeProvider.CreateScope();

            await scope.ExecuteWithContextAsync<Task>(async db =>
            {
                db.ContactUs.Add(feedback);
                await db.SaveChangesAsync();
            });

            scope.Complete();

            return Ok();
        }
    }

}
