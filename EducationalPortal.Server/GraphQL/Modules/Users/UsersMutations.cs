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
    public class UsersMutations : ObjectGraphType, IMutationMarker
    {
        public UsersMutations(UsersRepository usersRepository)
        {
            Field<NonNullGraphType<UserType>, UserModel>()
                .Name("CreateUser")
                .Argument<NonNullGraphType<CreateUserInputType>, UserModel>("CreateUserInputType", "Argument for create new User")
                .ResolveAsync(async context =>
                {
                    UserModel user = context.GetArgument<UserModel>("CreateUserInputType");
                    await usersRepository.CreateAsync(user);
                    return user;
                })
                .AuthorizeWith(AuthPolicies.Administrator);
        }
    }
}
