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
        public UsersMutations(UserRepository usersRepository)
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
            
            Field<NonNullGraphType<UserType>, UserModel>()
                .Name("UpdateUser")
                .Argument<NonNullGraphType<UpdateUserInputType>, UserModel>("UpdateUserInputType", "Argument for update User")
                .ResolveAsync(async context =>
                {
                    UserModel user = context.GetArgument<UserModel>("UpdateUserInputType");
                    await usersRepository.UpdateAsync(user);
                    return user;
                })
                .AuthorizeWith(AuthPolicies.Administrator);
           
            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("RemoveUser")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove User")
                .ResolveAsync(async (context) =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    await usersRepository.RemoveAsync(id);
                    return true;
                })
                .AuthorizeWith(AuthPolicies.Administrator);
        }
    }
}
