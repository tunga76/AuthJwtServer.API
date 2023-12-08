using AuthJwtServer.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwtServer.API.Extentions
{
    public static class CustomValidatorExtention
    {
        public static IServiceCollection UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(w=> w.Errors.Count() > 0).SelectMany(w => w.Errors).Select(w => w.ErrorMessage);
                    var response = Response.Fail(errors);
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
    }
}
