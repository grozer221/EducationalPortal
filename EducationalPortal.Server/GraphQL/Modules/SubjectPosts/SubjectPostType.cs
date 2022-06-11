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
        public SubjectPostType() : base()
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
               .ResolveAsync(async context =>
               {
                   var userRepository = context.RequestServices.GetRequiredService<IUserRepository>();
                   return await userRepository.GetByIdOrDefaultAsync(context.Source.TeacherId);
               });

            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("SubjectId")
               .Resolve(context => context.Source.SubjectId);

            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("Subject")
                .ResolveAsync(async context =>
                {
                    var subjectRepository = context.RequestServices.GetRequiredService<ISubjectRepository>();
                    return await subjectRepository.GetByIdAsync(context.Source.SubjectId);
                });

            Field<NonNullGraphType<ListGraphType<HomeworkType>>, IEnumerable<HomeworkModel>>()
                .Name("Homeworks")
                .ResolveAsync(async context =>
                {
                    var homeworkRepository = context.RequestServices.GetRequiredService<IHomeworkRepository>();
                    return await homeworkRepository.GetOrDefaultAsync(h => h.SubjectPostId == context.Source.Id);
                });
            
            Field<NonNullGraphType<ListGraphType<SubjectPostStatisticType>>, IEnumerable<SubjectPostStatistic>>()
                .Name("Statistics")
                .ResolveAsync(async context =>
                {
                    if (context.Source.Type != PostType.Homework)
                        return new List<SubjectPostStatistic>();

                    var subjectId = context.Source.SubjectId;
                    var subjectPostId = context.Source.Id;

                    var subjectRepository = context.RequestServices.GetRequiredService<ISubjectRepository>();
                    var subject = await subjectRepository.GetByIdAsync(subjectId, s => s.GradesHaveAccessRead);

                    var gradeRepository = context.RequestServices.GetRequiredService<IGradeRepository>();
                    int studentsHaveAccessReadCount = 0;
                    foreach (var gradeHaveAccessRead in subject.GradesHaveAccessRead)
                    {
                        var grade = await gradeRepository.GetByIdAsync(gradeHaveAccessRead.Id, s => s.Students);
                        studentsHaveAccessReadCount += grade.Students.Count;
                    }

                    var homeworkRepository = context.RequestServices.GetRequiredService<IHomeworkRepository>();
                    var homeworks = await homeworkRepository.GetOrDefaultAsync(h => h.SubjectPostId == subjectPostId);

                    int sentCount = homeworks.DistinctBy(h => h.StudentId).Count();
                    int notSentCount = studentsHaveAccessReadCount - sentCount;

                    return new List<SubjectPostStatistic>
                    {
                        new SubjectPostStatistic { Key = "Надіслали", Value = sentCount, HashColor = "#FF6384" },
                        new SubjectPostStatistic { Key = "Не надіслали", Value = notSentCount, HashColor = "#2ecc71" },
                    };
                });
        }
    }
    public class PostTypeType : EnumerationGraphType<PostType>
    {
    }
}
