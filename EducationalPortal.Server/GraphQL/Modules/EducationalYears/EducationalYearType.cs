using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Subjects;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears
{
    public class EducationalYearType : BaseType<EducationalYearModel>
    {
        public EducationalYearType(SubjectRepository subjectRepository) : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(context => context.Source.Name);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
                .Name("DateStart")
                .Resolve(context => context.Source.DateStart);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
                .Name("DateEnd")
                .Resolve(context => context.Source.DateEnd);
            
            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsCurrent")
                .Resolve(context => context.Source.IsCurrent);

            Field<GetEntitiesResponseType<SubjectType, SubjectModel>, GetEntitiesResponse<SubjectModel>>()
                .Name("Subjects")
                .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects")
                .Resolve(context =>
                {
                    int page = context.GetArgument<int>("Page");
                    Guid educationalYearId = context.Source.Id;
                    return subjectRepository.GetOrDefault(s => s.CreatedAt, Order.Descend, page, s => s.EducationalYearId == educationalYearId);
                })
                .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}