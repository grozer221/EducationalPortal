using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Subjects.DTO;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects
{
    public class SubjectsMutations : ObjectGraphType, IMutationMarker
    {
        public SubjectsMutations(SubjectRepository subjectsRepository, IHttpContextAccessor httpContextAccessor, EducationalYearRepository educationalYearRepository)
        {
            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("CreateSubject")
                .Argument<NonNullGraphType<CreateSubjectInputType>, SubjectModel>("CreateSubjectInputType", "Argument for create new Subject")
                .ResolveAsync(async (context) =>
                {
                    SubjectModel subject = context.GetArgument<SubjectModel>("CreateSubjectInputType");
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    return await subjectsRepository.CreateAsync(subject, currentUserId);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("UpdateSubject")
                .Argument<NonNullGraphType<UpdateSubjectInputType>, SubjectModel>("UpdateSubjectInputType", "Argument for update Subject")
                .ResolveAsync(async (context) =>
                {
                    SubjectModel newSubject = context.GetArgument<SubjectModel>("UpdateSubjectInputType");
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    return await subjectsRepository.UpdateAsync(newSubject, currentUserId);
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("RemoveSubject")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Subject")
               .ResolveAsync(async (context) =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                   await subjectsRepository.RemoveAsync(id, currentUserId);
                   return true;
               })
               .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
