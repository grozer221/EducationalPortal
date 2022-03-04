using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.SubjectPosts.DTO
{
    public class GetSubjectPostsResponseType : ObjectGraphType<GetEntitiesResponse<SubjectPostModel>>
    {
        public GetSubjectPostsResponseType()
        {
            Field<NonNullGraphType<ListGraphType<SubjectPostType>>>()
               .Name("Entities")
               .Resolve(context => context.Source.Entities);

            Field<NonNullGraphType<IntGraphType>>()
               .Name("Total")
               .Resolve(context => context.Source.Total);
        }
    }
}
