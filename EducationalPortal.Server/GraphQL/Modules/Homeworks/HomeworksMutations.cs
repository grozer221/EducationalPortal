using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.Extensions;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Homeworks.DTO;
using EducationalPortal.Server.Services;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks
{
    public class HomeworksMutations : ObjectGraphType, IMutationMarker
    {
        public HomeworksMutations(IHomeworkRepository homeworkRepository, IHttpContextAccessor httpContextAccessor, CloudinaryService cloudinaryService, IFileRepository fileRepository)
        {
            Field<NonNullGraphType<HomeworkType>, HomeworkModel>()
                .Name("CreateHomework")
                .Argument<NonNullGraphType<CreateHomeworkInputType>, CreateHomeworkInput>("CreateHomeworkInputType", "Argument for create new Homework")
                .ResolveAsync(async context =>
                {
                    CreateHomeworkInput createHomeworkInput = context.GetArgument<CreateHomeworkInput>("CreateHomeworkInputType");
                    var homework = createHomeworkInput.ToModel();
                    Guid currentStudentId = httpContextAccessor.HttpContext.GetUserId();
                    homework.StudentId = currentStudentId;
                    await homeworkRepository.CreateAsync(homework);
                    if (createHomeworkInput.Files != null)
                    {
                        foreach(var file in createHomeworkInput.Files)
                        {
                            var fileUplaod = new FileModel
                            {
                                Name = file.FileName,
                                Path = await cloudinaryService.UploadFileAsync(file),
                                HomeworkId = homework.Id,
                                CreatorId = currentStudentId,
                            };
                            await fileRepository.CreateAsync(fileUplaod);
                        }
                    }
                    return homework;
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
