using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Grades
{
    public class GradeType : ObjectGraphType<GradeModel>
    {
        public GradeType(UserRepository userRepository)
        {
            Field<NonNullGraphType<IdGraphType>, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
            
            Field<NonNullGraphType<GetEntitiesResponseType<UserType, UserModel>>, GetEntitiesResponse<UserModel>>()
               .Name("Students")
               .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects Posts")
               .Resolve(context =>
               {
                   int page = context.GetArgument<int>("Page");
                   Guid gradeId = context.Source.Id;
                   return userRepository.Get(s => s.CreatedAt, true, page, p => p.GradeId == gradeId);
               });

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
               .Name("UpdatedAt")
               .Resolve(context => context.Source.UpdatedAt);
        }
    }
}
