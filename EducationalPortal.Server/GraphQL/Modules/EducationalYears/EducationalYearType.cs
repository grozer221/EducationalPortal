using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.GraphQL.Abstraction;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears
{
    public class EducationalYearType : ObjectGraphType<EducationalYearModel>
    {
        public EducationalYearType()
        {
            Field<IdGraphType>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType>()
               .Name("Name")
               .Resolve(context => context.Source.Name);

            Field<DateTimeGraphType>()
               .Name("DateStart")
               .Resolve(context => context.Source.DateStart);

            Field<DateTimeGraphType>()
               .Name("DateEnd")
               .Resolve(context => context.Source.DateEnd);
            
            Field<BooleanGraphType>()
               .Name("IsCurrent")
               .Resolve(context => context.Source.IsCurrent);
            
            //Field<ListGraphType<SubjectType>()
            //   .Name("Subjects")
            //   .Resolve(context => context.Source.Subjects);


            Field<DateTimeGraphType>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);

            Field<DateTimeGraphType>()
               .Name("UpdatedAt")
               .Resolve(context => context.Source.UpdatedAt);
        }
    }
}
