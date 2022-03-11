using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO
{
    public class CreateEducationalYearInputType : InputObjectGraphType<EducationalYearModel>
    {
        public CreateEducationalYearInputType()
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
        }
    }
}
