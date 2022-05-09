using EducationalPortal.Business.Enums;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks
{
    public class HomeworkType : BaseType<HomeworkModel>
    {
        public HomeworkType(ISubjectPostRepository subjectPostRepository, IUserRepository userRepository) : base()
        {
            Field<StringGraphType, string?>()
               .Name("Text")
               .Resolve(context => context.Source.Text);

            Field<StringGraphType, string?>()
               .Name("Mark")
               .Resolve(context => context.Source.Mark);
            
            Field<StringGraphType, string?>()
               .Name("ReviewResult")
               .Resolve(context => context.Source.ReviewResult);
            
            Field<HomeworkStatusType, HomeworkStatus>()
               .Name("Status")
               .Resolve(context => context.Source.Status);
            
            Field<IdGraphType, Guid?>()
               .Name("SubjectPostId")
               .Resolve(context => context.Source.SubjectPostId);

            Field<SubjectPostType, SubjectPostModel?>()
               .Name("SubjectPost")
               .ResolveAsync(async context => await subjectPostRepository.GetByIdAsync(context.Source.SubjectPostId));
            
            Field<IdGraphType, Guid?>()
               .Name("StudentId")
               .Resolve(context => context.Source.StudentId);

            Field<UserType, UserModel?>()
               .Name("Student")
               .ResolveAsync(async context => await userRepository.GetByIdAsync(context.Source.StudentId));
        }
    }

}
