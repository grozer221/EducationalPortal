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
        public SubjectPostsMutations(SubjectPostRepository subjectPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<SubjectPostType>, SubjectPostModel>()
                .Name("CreateSubjectPost")
                .Argument<NonNullGraphType<CreateSubjectPostInputType>, SubjectPostModel>("CreateSubjectPostInputType", "Argument for create new Subject Post")
                .ResolveAsync(async (context) =>
                {
                    SubjectPostModel subjectPostModel = context.GetArgument<SubjectPostModel>("CreateSubjectPostInputType");
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    return await subjectPostRepository.CreateAsync(subjectPostModel, currentUserId);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<SubjectPostType>, SubjectPostModel>()
                .Name("UpdateSubjectPost")
                .Argument<NonNullGraphType<UpdateSubjectPostInputType>, SubjectPostModel>("UpdateSubjectPostInputType", "Argument for update Subject Post")
                .ResolveAsync(async (context) =>
                {
                    SubjectPostModel newSubjectPost = context.GetArgument<SubjectPostModel>("UpdateSubjectPostInputType");
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    return await subjectPostRepository.UpdateAsync(newSubjectPost, currentUserId);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("RemoveSubjectPost")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Subject Post")
               .ResolveAsync(async (context) =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                   await subjectPostRepository.RemoveAsync(id, currentUserId);
                   return true;
               })
               .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
