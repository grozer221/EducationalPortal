using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Users.DTO;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Users
{
    public class UsersQueries : ObjectGraphType, IQueryMarker
    {
        public UsersQueries(UserRepository usersRepository)
        {
            Field<NonNullGraphType<GetEntitiesResponseType<UserType, UserModel>>, GetEntitiesResponse<UserModel>>()
                .Name("GetUsers")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Users")
                .Argument<UserRoleType, UserRoleEnum?>("Role", "Argument for get Users")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    UserRoleEnum? role = context.GetArgument<UserRoleEnum?>("Role");
                    if (role == null)
                        return usersRepository.Get(u => u.LastName, false, page);
                    else
                        return usersRepository.Get(u => u.LastName, false, page, u => u.Role == role);
                })
                .AuthorizeWith(AuthPolicies.Authenticated);

            Field<NonNullGraphType<UserType>, UserModel>()
                .Name("GetUser")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get User")
                .Resolve(context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return usersRepository.GetById(id);
                })
                .AuthorizeWith(AuthPolicies.Authenticated);
        }
    }
}
