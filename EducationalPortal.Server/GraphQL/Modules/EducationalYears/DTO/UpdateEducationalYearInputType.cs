using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO
{
    public class UpdateEducationalYearInputType : InputObjectGraphType<EducationalYearModel>
    {
        public UpdateEducationalYearInputType()
        {
            Field<IdGraphType>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
            
            Field<BooleanGraphType>()
               .Name("IsCurrent")
               .Resolve(context => context.Source.IsCurrent);

            Field<DateTimeGraphType>()
               .Name("DateStart")
               .Resolve(context => context.Source.DateStart);

            Field<DateTimeGraphType>()
               .Name("DateEnd")
               .Resolve(context => context.Source.DateEnd);

            Field<DateTimeGraphType>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);
        }
    }
}
