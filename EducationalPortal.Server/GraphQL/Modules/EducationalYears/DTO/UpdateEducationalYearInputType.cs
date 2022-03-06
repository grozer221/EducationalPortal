using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.EducationalYears.DTO
{
    public class UpdateEducationalYearInputType : CreateEducationalYearInputType
    {
        public UpdateEducationalYearInputType() : base()
        {
            Field<IdGraphType>()
               .Name("Id")
               .Resolve(context => context.Source.Id);
            
            Field<BooleanGraphType>()
               .Name("IsCurrent")
               .Resolve(context => context.Source.IsCurrent);
        }
    }
}
