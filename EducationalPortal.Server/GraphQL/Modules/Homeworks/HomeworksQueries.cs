using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Homeworks
{
    public class HomeworksQueries : ObjectGraphType, IQueryMarker
    {
        public HomeworksQueries(HomeworkRepository homeworkRepository, IHttpContextAccessor httpContextAccessor, EducationalYearRepository educationalYearRepository)
        {
            Field<NonNullGraphType<HomeworkType>, HomeworkModel>()
                .Name("GetHomework")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Homework")
                .Resolve(context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return homeworkRepository.GetById(id);
                })
                .AuthorizeWith(AuthPolicies.Student);

            Field<NonNullGraphType<GetEntitiesResponseType<HomeworkType, HomeworkModel>>, GetEntitiesResponse<HomeworkModel>>()
                .Name("GetHomeworks")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Homeworks")
                .Argument<ListGraphType<HomeworkStatusType>, List<HomeworkStatus>?>("Statuses", "Argument for get Homeworks")
                .Argument<NonNullGraphType<OrderType>, Order>("Order", "Argument for get Homeworks")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    List<HomeworkStatus>? statuses = context.GetArgument<List<HomeworkStatus>?>("Statuses");
                    Order order = context.GetArgument<Order>("Order");
                    return homeworkRepository.Get(s => s.CreatedAt, order, page, 
                        s => (statuses == null || statuses.Count == 0) ? true : statuses.Contains(s.Status)
                    );
                })
               .AuthorizeWith(AuthPolicies.Administrator);

            //Field<NonNullGraphType<GetEntitiesResponseType<SubjectType, SubjectModel>>, GetEntitiesResponse<SubjectModel>>()
            //    .Name("GetMySubjects")
            //    .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get My Subjects")
            //    .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get My Subjects")
            //    .Resolve(context =>
            //    {
            //        int page = context.GetArgument<int>("Page");
            //        string like = context.GetArgument<string>("Like");
            //        EducationalYearModel currentEducationalYear = educationalYearRepository.GetCurrent();
            //        Guid currentUserId = new Guid(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultIdClaimType).Value);
            //        UserRoleEnum currentUserRole = (UserRoleEnum)Enum.Parse(
            //            typeof(UserRoleEnum),
            //            httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == AuthClaimsIdentity.DefaultRoleClaimType).Value
            //        );
            //        switch (currentUserRole)
            //        {
            //            case UserRoleEnum.Student:
            //                UserModel currentUser = userRepository.GetById(currentUserId);
            //                return subjectRepository.Get(s => s.CreatedAt, true, page,
            //                    s => s.GradesHaveAccessRead.Any(g => g.Id == currentUser.GradeId)
            //                    && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase)
            //                    && s.EducationalYearId == currentEducationalYear.Id,
            //                    s => s.GradesHaveAccessRead
            //                );
            //            case UserRoleEnum.Teacher:
            //            case UserRoleEnum.Administrator:
            //                return subjectRepository.Get(s => s.CreatedAt, true, page,
            //                    s => (s.TeacherId == currentUserId || s.TeachersHaveAccessCreatePosts.Any(t => t.Id == currentUserId))
            //                    && s.Name.Contains(like, StringComparison.OrdinalIgnoreCase)
            //                    && s.EducationalYearId == currentEducationalYear.Id,
            //                    s => s.TeachersHaveAccessCreatePosts
            //                );
            //            default:
            //                throw new Exception("Невідома роль");
            //        }
            //    })
            //   .AuthorizeWith(AuthPolicies.Authenticated);
        }
    }
}
