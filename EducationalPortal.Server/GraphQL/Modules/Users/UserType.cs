using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Grades;
using EducationalPortal.Server.GraphQL.Modules.Subjects;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Users
{
    public class UserType : ObjectGraphType<UserModel>
    {
        public UserType(GradeRepository gradeRepository, SubjectRepository subjectRepository)
        {
            Field<NonNullGraphType<IdGraphType>, Guid>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType, string>()
               .Name("FirstName")
               .Resolve(context => context.Source.FirstName);
            
            Field<StringGraphType, string>()
               .Name("LastName")
               .Resolve(context => context.Source.LastName);
            
            Field<StringGraphType, string>()
               .Name("MiddleName")
               .Resolve(context => context.Source.MiddleName);
            
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Login")
               .Resolve(context => context.Source.Login);
            
            Field<StringGraphType, string>()
               .Name("Email")
               .Resolve(context => context.Source.Email);
            
            Field<StringGraphType, string>()
               .Name("PhoneNumber")
               .Resolve(context => context.Source.PhoneNumber);

            Field<DateTimeGraphType, DateTime>()
               .Name("DateOfBirth")
               .Resolve(context => context.Source.DateOfBirth);

            Field<NonNullGraphType<UserRoleType>, UserRoleEnum>()
               .Name("Role")
               .Resolve(context => context.Source.Role);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
               .Name("UpdatedAt")
               .Resolve(context => context.Source.UpdatedAt);

            Field<IdGraphType, Guid?>()
               .Name("GradeId")
               .Resolve(context => context.Source.GradeId);
            
            Field<GradeType, GradeModel?>()
               .Name("Grade")
               .Resolve(context =>
               {
                   try { return gradeRepository.GetById(context.Source.GradeId); }
                   catch { return null; }
               });

            Field<GetEntitiesResponseType<SubjectType, SubjectModel>, GetEntitiesResponse<SubjectModel>>()
               .Name("Subjects")
               .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects Posts")
               .Resolve(context =>
               { 
                   int page = context.GetArgument<int>("Page");
                   Guid userId = context.Source.Id;
                   return subjectRepository.Get(s => s.CreatedAt, true, page, s => s.TeacherId == userId);
               });
        }
    }

    public class UserRoleType : EnumerationGraphType<UserRoleEnum>
    {
    }
}
