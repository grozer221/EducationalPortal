using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO
{
    public class GetSubjectsResponseType : ObjectGraphType<GetEntitiesResponse<EducationalYearModel>>
    {
        public GetSubjectsResponseType()
        {
            Field<NonNullGraphType<ListGraphType<EducationalYearType>>>()
               .Name("Entities")
               .Resolve(context => context.Source.Entities);
            
            Field<NonNullGraphType<IntGraphType>>()
               .Name("Total")
               .Resolve(context => context.Source.Total);
        }
    }
}
