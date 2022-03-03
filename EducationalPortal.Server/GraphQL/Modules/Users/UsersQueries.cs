using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
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
        public UsersQueries(UsersRepository usersRepository)
        {
            Field<NonNullGraphType<GetUsersResponseType>, GetEntitiesResponse<UserModel>>()
                .Name("GetUsers")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Users")
                .Resolve(context => 
                {
                    int page = context.GetArgument<int>("Page");
                    return usersRepository.Get(u => u.CreatedAt, false, page);
                });
            
            Field<NonNullGraphType<UserType>, UserModel>()
                .Name("GetUser")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get User")
                .ResolveAsync(async context => 
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return await usersRepository.GetByIdAsync(id);
                });
        }
    }
}
