using EducationalPortal.Server.GraphQL.Modules.Auth;

namespace EducationalPortal.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return new Guid(httpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
        }
    }
}
