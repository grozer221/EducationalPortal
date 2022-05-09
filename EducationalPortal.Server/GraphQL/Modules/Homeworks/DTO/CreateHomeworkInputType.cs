using EducationalPortal.Business.Models;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks.DTO
{
    public class CreateHomeworkInputType : InputObjectGraphType<HomeworkModel>
    {
        public CreateHomeworkInputType()
        {
            Field<StringGraphType, string?>()
               .Name("Text")
               .Resolve(context => context.Source.Text);
            
            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("SubjectPostId")
               .Resolve(context => context.Source.SubjectPostId);
        }
    }
}
