using EducationalPortal.Database.Models;
using EducationalPortal.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth.DTO;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Auth
{
    public class AuthQueries : ObjectGraphType, IQueryMarker
    {
        public AuthQueries(UsersRepository usersRepository, IHttpContextAccessor httpContextAccessor, AuthService authService)
        {
            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Me")
                .Resolve(context =>
                {
                    string userLogin = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultLoginClaimType).Value;
                    UserModel currentUser = usersRepository.GetByLogin(userLogin);
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
