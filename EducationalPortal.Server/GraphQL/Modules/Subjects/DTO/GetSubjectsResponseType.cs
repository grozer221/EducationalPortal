using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects.DTO
{
    public class GetSubjectsResponseType : ObjectGraphType<GetEntitiesResponse<SubjectModel>>
    {
        public GetSubjectsResponseType()
        {
            Field<NonNullGraphType<ListGraphType<SubjectType>>>()
               .Name("Entities")
               .Resolve(context => context.Source.Entities);
            
            Field<NonNullGraphType<IntGraphType>>()
               .Name("Total")
               .Resolve(context => context.Source.Total);
        }
    }
}
