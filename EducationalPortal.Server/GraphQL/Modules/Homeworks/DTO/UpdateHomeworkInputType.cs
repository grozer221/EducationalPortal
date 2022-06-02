using EducationalPortal.Business.Enums;
using EducationalPortal.Business.Models;
using EducationalPortal.Server.GraphQL.Abstraction;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks.DTO
{
    public class UpdateHomeworkInput : IModelable<HomeworkModel>
    {
        public Guid Id { get; set; }
        public string? Mark { get; set; }
        public string? ReviewResult { get; set; }
        public HomeworkStatus Status { get; set; }

        public HomeworkModel ToModel()
        {
            return new HomeworkModel
            {
                Id = Id,
                Mark = Mark,
                ReviewResult = ReviewResult,
                Status = Status,
            };
        }
    }

    public class UpdateHomeworkInputType : InputObjectGraphType<UpdateHomeworkInput>
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
