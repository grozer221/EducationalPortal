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
        public SubjectsQueries(SubjectRepository subjectRepository, UserRepository userRepository, IHttpContextAccessor httpContextAccessor, EducationalYearRepository educationalYearRepository)
        {
            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("GetSubject")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Subject")
                .Resolve(context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return subjectRepository.GetById(id);
                })
                .AuthorizeWith(AuthPolicies.Authenticated);

            Field<NonNullGraphType<GetEntitiesResponseType<SubjectType, SubjectModel>>, GetEntitiesResponse<SubjectModel>>()
                .Name("GetSubjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects")
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get My Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    EducationalYearModel currentEducationalYear = educationalYearRepository.GetCurrent();
                    return subjectRepository.Get(s => s.CreatedAt, Order.Descend, page, s => 
                        s.EducationalYearId == currentEducationalYear.Id
                        && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase)
                    );
                })
               .AuthorizeWith(AuthPolicies.Teacher);
            
            Field<NonNullGraphType<GetEntitiesResponseType<SubjectType, SubjectModel>>, GetEntitiesResponse<SubjectModel>>()
                .Name("GetMySubjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get My Subjects")
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get My Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    EducationalYearModel currentEducationalYear = educationalYearRepository.GetCurrent();
                    Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
                    UserRoleEnum currentUserRole = (UserRoleEnum)Enum.Parse(
                        typeof(UserRoleEnum),
                        httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultRoleClaimType).Value
                    );
                    switch (currentUserRole)
                    {
                        case UserRoleEnum.Student:
                            UserModel currentUser = userRepository.GetById(currentUserId);
                            return subjectRepository.Get(s => s.CreatedAt, Order.Descend, page,
                                s => s.GradesHaveAccessRead.Any(g => g.Id == currentUser.GradeId)
                                && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase)
                                && s.EducationalYearId == currentEducationalYear.Id,
                                s => s.GradesHaveAccessRead
                            );
                        case UserRoleEnum.Teacher:
                        case UserRoleEnum.Administrator:
                            return subjectRepository.Get(s => s.CreatedAt, Order.Descend, page,
                                s => (s.TeacherId == currentUserId || s.TeachersHaveAccessCreatePosts.Any(t => t.Id == currentUserId))
                                && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase)
                                && s.EducationalYearId == currentEducationalYear.Id,
                                s => s.TeachersHaveAccessCreatePosts
                            );
                        default:
                            throw new Exception("Невідома роль");
                    }
                   
                })
               .AuthorizeWith(AuthPolicies.Authenticated);
        }
    }
}
