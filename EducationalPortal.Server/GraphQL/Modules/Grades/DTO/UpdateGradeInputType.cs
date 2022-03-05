using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Grades.DTO
{
    public class UpdateGradeInputType : InputObjectGraphType<GradeModel>
    {
        public UpdateGradeInputType()
        {
            Field<NonNullGraphType<IdGraphType>, Guid>()
                .Name("Id")
                .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
        }
    }
}
