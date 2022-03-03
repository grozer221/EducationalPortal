using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Users.DTO
{
    public class GetUsersResponseType : ObjectGraphType<GetEntitiesResponse<UserModel>>
    {
        public GetUsersResponseType()
        {
            Field<NonNullGraphType<ListGraphType<UserType>>>()
               .Name("Entities")
               .Resolve(context => context.Source.Entities);
            
            Field<NonNullGraphType<IntGraphType>>()
               .Name("Total")
               .Resolve(context => context.Source.Total);
        }
    }
}
