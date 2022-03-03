using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Users
{
    public class UserType : ObjectGraphType<UserModel>
    {
        public UserType()
        {
            Field<NonNullGraphType<IdGraphType>>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

            Field<StringGraphType>()
               .Name("FirstName")
               .Resolve(context => context.Source.FirstName);
            
            Field<StringGraphType>()
               .Name("LastName")
               .Resolve(context => context.Source.LastName);
            
            Field<StringGraphType>()
               .Name("MiddleName")
               .Resolve(context => context.Source.MiddleName);
            
            Field<NonNullGraphType<StringGraphType>>()
               .Name("Login")
               .Resolve(context => context.Source.Login);
            
            Field<StringGraphType>()
               .Name("Email")
               .Resolve(context => context.Source.Email);
            
            Field<StringGraphType>()
               .Name("PhoneNumber")
               .Resolve(context => context.Source.PhoneNumber);

            Field<DateTimeGraphType>()
               .Name("DateOfBirth")
               .Resolve(context => context.Source.DateOfBirth);

            Field<NonNullGraphType<UserRoleType>>()
               .Name("Role")
               .Resolve(context => context.Source.Role);

            Field<NonNullGraphType<DateTimeGraphType>>()
               .Name("CreatedAt")
               .Resolve(context => context.Source.CreatedAt);

            Field<NonNullGraphType<DateTimeGraphType>>()
               .Name("UpdatedAt")
               .Resolve(context => context.Source.UpdatedAt);
            
            //Field<GradeType>()
            //   .Name("Grade")
            //   .Resolve(context => context.Source.Grade);
            
            //Field<ListGraphType<SubjectType>>()
            //   .Name("Subjects")
            //   .Resolve(context => context.Source.Subjects);
        }
    }

    public class UserRoleType : EnumerationGraphType<UserRoleEnum>
    {
    }
}
