using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RiverStationWatch.Data;
using System;
using System.Linq;

namespace RiverStationWatch.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenAuthFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            var token = context.HttpContext.Request.Headers["Security-Token"].FirstOrDefault();

            // Retrieve the latest token from the database
            var latestToken = dbContext.StationTokens
                .OrderByDescending(t => t.Validity)
                .FirstOrDefault();

            // Check if the token is valid
            if (latestToken == null || token != latestToken.Token)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
