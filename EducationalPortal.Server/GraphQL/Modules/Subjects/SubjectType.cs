using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects
{
    public class SubjectType : ObjectGraphType<SubjectModel>
    {
        public SubjectType(UsersRepository usersRepository, EducationalYearRepository educationalYearRepository)
        {
            Field<IdGraphType, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);

            Field<StringGraphType, string>()
               .Name("Link")
               .Resolve(context => context.Source.Link);

            //Field<NonNullGraphType<ListGraphType<SubjectPostType>>, IEnumerable<SubjectPostModel>>()
            //   .Name("Posts")
            //   .Resolve(context => context.Source.Posts);

            //Field<NonNullGraphType<ListGraphType<GradeType>>, IEnumerable<GradeModel>>()
            //   .Name("GradesHaveAccessRead")
            //   .Resolve(context => context.Source.GradesHaveAccessRead);

            Field<IdGraphType, Guid?>()
               .Name("TeacherId")
               .Resolve(context => context.Source.TeacherId);

            Field<UserType, UserModel>()
               .Name("Teacher")
               .ResolveAsync(async context => 
               {
                   return await usersRepository.GetByIdAsync(context.Source.TeacherId);
               });
            
            Field<IdGraphType, Guid?>()
               .Name("EducationalYearId")
               .Resolve(context => context.Source.EducationalYearId);

            Field<EducationalYearType, EducationalYearModel>()
               .Name("EducationalYear")
               .ResolveAsync(async context => 
               {
                   return await educationalYearRepository.GetByIdAsync(context.Source.EducationalYearId);
               });


            Field<NonNullGraphType<DateTimeGraphType>>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);

            Field<NonNullGraphType<DateTimeGraphType>>()
               .Name("UpdatedAt")
               .Resolve(context => context.Source.UpdatedAt);
        }
    }
}
