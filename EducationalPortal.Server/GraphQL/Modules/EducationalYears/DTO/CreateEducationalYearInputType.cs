using EducationalPortal.Database.Models;
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
            Field<StringGraphType>()
               .Name("Name")
               .Resolve(context => context.Source.Name);

            Field<DateTimeGraphType>()
               .Name("DateStart")
               .Resolve(context => context.Source.DateStart);

            Field<DateTimeGraphType>()
               .Name("DateEnd")
               .Resolve(context => context.Source.DateEnd);
        }
    }
}
