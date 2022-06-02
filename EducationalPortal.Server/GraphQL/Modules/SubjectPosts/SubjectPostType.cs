using EducationalPortal.Business.Enums;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Homeworks;
using EducationalPortal.Server.GraphQL.Modules.Subjects;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.SubjectPosts
{
    public class SubjectPostType : BaseType<SubjectPostModel>
    {
        public SubjectPostType(IUserRepository usersRepository, ISubjectRepository subjectRepository, IHomeworkRepository homeworkRepository) : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Title")
               .Resolve(context => context.Source.Title);
            
            Field<StringGraphType, string>()
               .Name("Text")
               .Resolve(context => context.Source.Text);
            
            Field<NonNullGraphType<PostTypeType>, PostType>()
               .Name("Type")
               .Resolve(context => context.Source.Type);
            
            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("TeacherId")
               .Resolve(context => context.Source.TeacherId);
            
            Field<NonNullGraphType<UserType>, UserModel>()
               .Name("Teacher")
               .ResolveAsync(async context => await usersRepository.GetByIdOrDefaultAsync(context.Source.TeacherId));

            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("SubjectId")
               .Resolve(context => context.Source.SubjectId);

            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("Subject")
                .ResolveAsync(async context => await subjectRepository.GetByIdAsync(context.Source.SubjectId));

            Field<NonNullGraphType<ListGraphType<HomeworkType>>, IEnumerable<HomeworkModel>>()
                .Name("Homeworks")
                .ResolveAsync(async context => await homeworkRepository.GetOrDefaultAsync(h => h.SubjectPostId == context.Source.Id));
        }
    }
    public class PostTypeType : EnumerationGraphType<PostType>
    {
    }
}
