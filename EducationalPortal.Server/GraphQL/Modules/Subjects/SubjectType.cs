using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears;
using EducationalPortal.Server.GraphQL.Modules.Grades;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts.DTO;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Subjects
{
    public class SubjectType : BaseType<SubjectModel>
    {
        public SubjectType(UserRepository usersRepository, EducationalYearRepository educationalYearRepository, SubjectPostRepository subjectPostRepository, SubjectRepository subjectRepository) : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);

            Field<StringGraphType, string>()
               .Name("Link")
               .Resolve(context => context.Source.Link);

            Field<GetEntitiesResponseType<SubjectPostType, SubjectPostModel>, GetEntitiesResponse<SubjectPostModel>>()
               .Name("Posts")
               .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects Posts")
               .Resolve(context =>
               {
                   int page = context.GetArgument<int>("Page");
                   Guid subjectId = context.Source.Id;
                   return subjectPostRepository.GetOrDefault(p => p.CreatedAt, true, page, p => p.SubjectId == subjectId);
               });

            Field<ListGraphType<GradeType>, IEnumerable<GradeModel>?>()
               .Name("GradesHaveAccessRead")
               .Resolve(context => subjectRepository.GetByIdWithGradesHaveAccessRead(context.Source.Id)?.GradesHaveAccessRead);

            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("TeacherId")
               .Resolve(context => context.Source.TeacherId);

            Field<NonNullGraphType<UserType>, UserModel>()
               .Name("Teacher")
               .Resolve(context => 
               {
                   return usersRepository.GetByIdOrDefault(context.Source.TeacherId);
               });
            
            Field<NonNullGraphType<IdGraphType>, Guid?>()
               .Name("EducationalYearId")
               .Resolve(context => context.Source.EducationalYearId);

            Field<NonNullGraphType<EducationalYearType>, EducationalYearModel>()
               .Name("EducationalYear")
               .Resolve(context =>
               {
                   return educationalYearRepository.GetByIdOrDefault(context.Source.EducationalYearId);
               });
        }
    }
}
