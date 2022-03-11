using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts.DTO;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.SubjectPosts
{
    public class SubjectPostsMutations : ObjectGraphType, IMutationMarker
    {
        public SubjectPostsMutations(SubjectRepository subjectRepository, SubjectPostRepository subjectPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<SubjectPostType>, SubjectPostModel>()
                .Name("CreateSubjectPost")
                .Argument<NonNullGraphType<CreateSubjectPostInputType>, SubjectPostModel>("CreateSubjectPostInputType", "Argument for create new Subject Post")
                .ResolveAsync(async (context) =>
                {
                    SubjectPostModel subjectPost = context.GetArgument<SubjectPostModel>("CreateSubjectPostInputType");
                    Guid currentTeacherId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    UserRoleEnum currentTeacherRole = (UserRoleEnum)Enum.Parse(
                        typeof(UserRoleEnum),
                        httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultRoleClaimType).Value
                    );
                    SubjectModel subject = subjectRepository.GetByIdWithTeachersHaveAccessCreatePosts(subjectPost.SubjectId);
                    if (subject.TeacherId != currentTeacherId && !subject.TeachersHaveAccessCreatePosts.Any(t => t.Id == currentTeacherId) && currentTeacherRole != UserRoleEnum.Administrator)
                        throw new Exception($"Ви не маєте прав на додавання посту для даного предмета");
                    subjectPost.TeacherId = currentTeacherId;
                    return await subjectPostRepository.CreateAsync(subjectPost);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<SubjectPostType>, SubjectPostModel>()
                .Name("UpdateSubjectPost")
                .Argument<NonNullGraphType<UpdateSubjectPostInputType>, SubjectPostModel>("UpdateSubjectPostInputType", "Argument for update Subject Post")
                .ResolveAsync(async (context) =>
                {
                    SubjectPostModel newSubjectPost = context.GetArgument<SubjectPostModel>("UpdateSubjectPostInputType");
                    Guid currentTeacherId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    UserRoleEnum currentTeacherRole = (UserRoleEnum)Enum.Parse(
                      typeof(UserRoleEnum),
                      httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultRoleClaimType).Value
                    );
                    SubjectPostModel oldSubjectPost = subjectPostRepository.GetById(newSubjectPost.Id);
                    SubjectModel subject = subjectRepository.GetByIdWithTeachersHaveAccessCreatePosts(oldSubjectPost.SubjectId);
                    if (subject.TeacherId != currentTeacherId && !subject.TeachersHaveAccessCreatePosts.Any(t => t.Id == currentTeacherId) && currentTeacherRole != UserRoleEnum.Administrator)
                        throw new Exception($"Ви не маєте прав на редагування посту для даного предмета");
                    return await subjectPostRepository.UpdateAsync(newSubjectPost);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("RemoveSubjectPost")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Subject Post")
               .ResolveAsync(async (context) =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   SubjectPostModel subjectPost = subjectPostRepository.GetById(id);
                   Guid currentTeacherId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                   UserRoleEnum currentTeacherRole = (UserRoleEnum)Enum.Parse(
                     typeof(UserRoleEnum),
                     httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultRoleClaimType).Value
                    );
                   SubjectModel subject = subjectRepository.GetByIdWithTeachersHaveAccessCreatePosts(subjectPost.SubjectId);
                   if (subject.TeacherId != currentTeacherId && !subject.TeachersHaveAccessCreatePosts.Any(t => t.Id == currentTeacherId) && currentTeacherRole != UserRoleEnum.Administrator)
                        throw new Exception($"Ви не маєте прав на видалення даного посту");
                   await subjectPostRepository.RemoveAsync(id);
                   return true;
               })
               .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
