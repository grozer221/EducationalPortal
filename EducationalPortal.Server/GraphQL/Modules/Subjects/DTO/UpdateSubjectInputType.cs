using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects.DTO
{
    public class UpdateSubjectInputType : CreateSubjectInputType
    {
        public UpdateSubjectInputType() : base()
        {
            Field<NonNullGraphType<IdGraphType>, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType, string>()
               .Name("Link")
               .Resolve(context => context.Source.Link);
        }
    }
}
