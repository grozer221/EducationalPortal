﻿using EducationalPortal.Server.Database.Abstractions;
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
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get Users")
                .Argument<ListGraphType<UserRoleType>, IEnumerable<UserRoleEnum>?>("Roles", "Argument for get Users")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    IEnumerable<UserRoleEnum>? roles = context.GetArgument<IEnumerable<UserRoleEnum>?>("Roles");
                    if (roles == null || roles.Count() == 0)
                        return usersRepository.Get(u => u.LastName, false, page, 
                            u => (u.FirstName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                            || (u.LastName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                            || (u.MiddleName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                            || (u.Login?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                            || (u.Email?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                        );
                    else
                        return usersRepository.Get(u => u.LastName, false, page, 
                            u => roles.Contains(u.Role) 
                            && (
                                (u.FirstName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (u.LastName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (u.MiddleName?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (u.Login?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (u.Email?.Contains(like, StringComparison.OrdinalIgnoreCase) ?? false)
                            ));
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
