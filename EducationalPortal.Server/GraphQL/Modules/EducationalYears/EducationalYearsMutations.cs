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
    public class EducationalYearsMutations : ObjectGraphType, IMutationMarker
    {
        public EducationalYearsMutations(EducationalYearRepository educationalYearRepository)
        {
            Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
                .Name("CreateEducationalYear")
                .Argument<NonNullGraphType<CreateEducationalYearInputType>, EducationalYearModel>("createEducationalYearInputType", "Argument for create new Educational year")
                .ResolveAsync(async (context) =>
                {
                    EducationalYearModel educationalYear = context.GetArgument<EducationalYearModel>("createEducationalYearInputType");
                    return await educationalYearRepository.CreateAsync(educationalYear);
                })
                .AuthorizeWith(AuthPolicies.Administrator);

            Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
                .Name("UpdateEducationalYear")
                .Argument<NonNullGraphType<UpdateEducationalYearInputType>, EducationalYearModel>("UpdateEducationalYearInputType", "Argument for update Educational year")
                .ResolveAsync(async (context) =>
                {
                    EducationalYearModel educationalYear = context.GetArgument<EducationalYearModel>("UpdateEducationalYearInputType");
                    return await educationalYearRepository.UpdateAsync(educationalYear);
                })
                .AuthorizeWith(AuthPolicies.Administrator);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("RemoveEducationalYear")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for remove Educational year")
               .ResolveAsync(async (context) =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   await educationalYearRepository.RemoveAsync(id);
                   return true;
               })
               .AuthorizeWith(AuthPolicies.Administrator);

            Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
               .Name("SetCurrentEducationalYear")
               .Argument<NonNullGraphType<IdGraphType>, Guid>("Id", "Argument for set current Educational year")
               .ResolveAsync(async (context) =>
               {
                   Guid id = context.GetArgument<Guid>("Id");
                   return await educationalYearRepository.SetCurrentEducationalYearAsync(id);
               })
               .AuthorizeWith(AuthPolicies.Administrator);

        }
    }
}
