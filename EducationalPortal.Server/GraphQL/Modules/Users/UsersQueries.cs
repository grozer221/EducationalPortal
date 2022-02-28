using EducationalPortal.Database.Models;
using EducationalPortal.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
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
            Field<NonNullGraphType<ListGraphType<UserType>>, IEnumerable<UserModel>>()
                .Name("GetUsers")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Users")
                .Resolve(context => 
                {
                    int page = context.GetArgument<int>("Page");
                    return usersRepository.Get(page);
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
