using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO;
using EducationalPortal.Server.GraphQL.Modules.Grades.DTO;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Grades
{
    public class GradesQueries : ObjectGraphType, IQueryMarker
    {
        public GradesQueries(GradeRepository gradeRepository)
        {
            Field<NonNullGraphType<GetEntitiesResponseType<GradeType, GradeModel>>, GetEntitiesResponse<GradeModel>>()
                .Name("GetGrades")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Grades")
                .Argument<NonNullGraphType<StringGraphType>, string>("Like", "Argument for get Grades")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    string like = context.GetArgument<string>("Like");
                    return gradeRepository.Get(y => y.Name, Order.Ascend, page, g => g.Name.Contains(like, StringComparison.OrdinalIgnoreCase));
                })
                .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<GradeType>, GradeModel>()
                .Name("GetGrade")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Grade")
                .Resolve(context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return gradeRepository.GetById(id);
                })
                .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
