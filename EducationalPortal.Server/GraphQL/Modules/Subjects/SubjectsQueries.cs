using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO;
using EducationalPortal.Server.GraphQL.Modules.Subjects.DTO;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects
{
    public class SubjectsQueries : ObjectGraphType, IQueryMarker
    {
        public SubjectsQueries(SubjectRepository subjectsRepository, UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("GetSubject")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Subject")
                .Resolve(context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return subjectsRepository.GetById(id);
                })
                .AuthorizeWith(AuthPolicies.Authenticated);

            Field<NonNullGraphType<GetEntitiesResponseType<SubjectType, SubjectModel>>, GetEntitiesResponse<SubjectModel>>()
                .Name("GetSubjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    return subjectsRepository.Get(s => s.CreatedAt, true, page);
                })
               .AuthorizeWith(AuthPolicies.Authenticated);
            
            Field<NonNullGraphType<GetEntitiesResponseType<SubjectType, SubjectModel>>, GetEntitiesResponse<SubjectModel>>()
                .Name("GetMySubjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get My Subjects")
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get My Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    UserModel currentUser = userRepository.GetById(currentUserId);
                    if (currentUser.Role == UserRoleEnum.Student)
                        return new GetEntitiesResponse<SubjectModel>();
                    return subjectsRepository.Get(s => s.CreatedAt, true, page, 
                        s => s.TeacherId == currentUserId && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase));
                })
               .AuthorizeWith(AuthPolicies.Authenticated);
        }
    }
}
