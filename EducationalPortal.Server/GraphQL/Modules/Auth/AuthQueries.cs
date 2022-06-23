using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.Extensions;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth.DTO;
using EducationalPortal.Server.Services;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Auth
{
    public class AuthQueries : ObjectGraphType, IQueryMarker
    {
        public AuthQueries(IUserRepository usersRepository, IHttpContextAccessor httpContextAccessor, AuthService authService)
        {
            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Me")
                .ResolveAsync(async context =>
                {
                    string userLogin = httpContextAccessor.HttpContext.GetUserLogin();
                    UserModel currentUser = await usersRepository.GetByLoginAsync(userLogin);
                    return new AuthResponse()
                    {
                        Token = authService.GenerateAccessToken(currentUser.Id, currentUser.Login, currentUser.Role),
                        User = currentUser,
                    };
                })
                .AuthorizeWith(AuthPolicies.Authenticated);
        }
    }
}
