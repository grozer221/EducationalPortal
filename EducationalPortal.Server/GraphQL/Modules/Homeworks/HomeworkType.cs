using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;
using Newtonsoft.Json;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks
{
    public class HomeworkType : BaseType<HomeworkModel>
    {
        public HomeworkType(SubjectPostRepository subjectPostRepository, UserRepository userRepository) : base()
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
               .Resolve(context => subjectPostRepository.GetById(context.Source.SubjectPostId));
            
            Field<IdGraphType, Guid?>()
               .Name("StudentId")
               .Resolve(context => context.Source.StudentId);

            Field<UserType, UserModel?>()
               .Name("Student")
               .Resolve(context => userRepository.GetById(context.Source.StudentId));
        }
    }

}
