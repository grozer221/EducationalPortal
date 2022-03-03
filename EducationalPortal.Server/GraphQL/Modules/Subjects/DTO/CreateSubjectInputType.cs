using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects.DTO
{
    public class CreateSubjectInputType : InputObjectGraphType<SubjectModel>
    {
        public CreateSubjectInputType()
        {
            Field<StringGraphType>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
        }
    }
}
