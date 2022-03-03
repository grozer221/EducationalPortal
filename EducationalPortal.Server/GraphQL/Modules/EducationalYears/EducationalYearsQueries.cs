using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears
{
    public class EducationalYearsQueries : ObjectGraphType, IQueryMarker
    {
        public EducationalYearsQueries(EducationalYearRepository educationalYearRepository)
        {
            Field<NonNullGraphType<GetSubjectsResponseType>, GetEntitiesResponse<EducationalYearModel>>()
                .Name("GetEducationalYears")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Educational years")
                .Resolve(context => 
                {
                    int page = context.GetArgument<int>("Page");
                    return educationalYearRepository.Get(y => y.CreatedAt, true, page);
                })
               .AuthorizeWith(AuthPolicies.Teacher);

            Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
                .Name("GetEducationalYear")
                .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for get Educational year")
                .ResolveAsync(async context =>
                {
                    Guid id = context.GetArgument<Guid>("Id");
                    return await educationalYearRepository.GetByIdAsync(id);
                })
                .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
