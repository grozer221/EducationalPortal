﻿using EducationalPortal.Server.Database.Models;
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
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
            
            Field<NonNullGraphType<ListGraphType<IdGraphType>>, IEnumerable<Guid>>()
               .Name("GradesHaveAccessReadIds")
               .Resolve(context => context.Source.GradesHaveAccessReadIds);
            
            Field<NonNullGraphType<ListGraphType<IdGraphType>>, IEnumerable<Guid>>()
               .Name("TeachersHaveAccessCreatePostsIds")
               .Resolve(context => context.Source.TeachersHaveAccessCreatePostsIds);
        }
    }
}
