using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Homeworks.DTO;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks
{
    public class HomeworksMutations : ObjectGraphType, IMutationMarker
    {
        public HomeworksMutations(IHomeworkRepository homeworkRepository, ISubjectPostRepository subjectPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<HomeworkType>, HomeworkModel>()
                .Name("CreateHomework")
                .Argument<NonNullGraphType<CreateHomeworkInputType>, HomeworkModel>("CreateHomeworkInputType", "Argument for create new Homework")
                .ResolveAsync(async (context) =>
                {
                    HomeworkModel homework = context.GetArgument<HomeworkModel>("CreateHomeworkInputType");
                    //SubjectPostModel subjectPost = subjectPostRepository.GetById(homework.SubjectPostId);
                    Guid currentStudentId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    homework.StudentId = currentStudentId;
                    return await homeworkRepository.CreateAsync(homework);
                })
                .AuthorizeWith(AuthPolicies.Student);

            Field<NonNullGraphType<HomeworkType>, HomeworkModel>()
                .Name("UpdateHomework")
                .Argument<NonNullGraphType<UpdateHomeworkInputType>, HomeworkModel>("UpdateHomeworkInputType", "Argument for update Grade")
                .ResolveAsync(async (context) =>
                {
                    HomeworkModel homework = context.GetArgument<HomeworkModel>("UpdateHomeworkInputType");
                    return await homeworkRepository.UpdateAsync(homework);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            //Field<NonNullGraphType<BooleanGraphType>, bool>()
            //   .Name("RemoveGrade")
            //   .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Grade")
            //   .ResolveAsync(async (context) =>
            //   {
            //       Guid id = context.GetArgument<Guid>("Id");
            //       await gradeRepository.RemoveAsync(id);
            //       return true;
            //   })
            //   .AuthorizeWith(AuthPolicies.Administrator);
        }
    }
}
