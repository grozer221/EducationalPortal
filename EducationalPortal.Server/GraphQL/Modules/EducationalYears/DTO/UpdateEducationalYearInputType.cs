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
            Field<NonNullGraphType<IdGraphType>, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);
            
            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("IsCurrent")
               .Resolve(context => context.Source.IsCurrent);
        }
    }
}
