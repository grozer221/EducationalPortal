using EducationalPortal.Database.Models;
using EducationalPortal.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
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
        public SubjectsQueries(SubjectsRepository subjectsRepository)
        {
            Field<NonNullGraphType<ListGraphType<SubjectType>>, IEnumerable<SubjectModel>>()
                .Name("GetSubjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    return subjectsRepository.Get(page);
                })
               .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<SubjectType>, SubjectModel>()
                .Name("GetSubject")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Subject")
                .ResolveAsync(async context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return await subjectsRepository.GetByIdAsync(id);
                })
                .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
