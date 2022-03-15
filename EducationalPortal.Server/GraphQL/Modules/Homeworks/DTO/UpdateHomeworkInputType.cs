using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks.DTO
{
    public class UpdateHomeworkInputType : InputObjectGraphType<HomeworkModel>
    {
        public UpdateHomeworkInputType()
        {
            Field<NonNullGraphType<IdGraphType>, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);
            
            Field<StringGraphType, string?>()
               .Name("Mark")
               .Resolve(context => context.Source.Mark);
            
            Field<StringGraphType, string?>()
               .Name("ReviewResult")
               .Resolve(context => context.Source.ReviewResult);
            
            Field<NonNullGraphType<HomeworkStatusType>, HomeworkStatus>()
               .Name("Status")
               .Resolve(context => context.Source.Status);

        }
    }
}
