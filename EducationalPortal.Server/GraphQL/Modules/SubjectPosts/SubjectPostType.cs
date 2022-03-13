using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Subjects;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.SubjectPosts
{
    public class SubjectPostType : BaseType<SubjectPostModel>
    {
        public SubjectPostType(UserRepository usersRepository, SubjectRepository subjectRepository) : base()
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
               .Resolve(context => usersRepository.GetByIdOrDefault(context.Source.TeacherId));

            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("SubjectId")
               .Resolve(context => context.Source.SubjectId);

            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("Subject")
                .Resolve(context => subjectRepository.GetById(context.Source.SubjectId));
        }
    }
    public class PostTypeType : EnumerationGraphType<PostType>
    {
    }
}
