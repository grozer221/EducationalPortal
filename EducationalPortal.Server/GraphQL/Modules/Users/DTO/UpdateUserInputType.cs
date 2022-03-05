using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Users.DTO
{
    public class UpdateUserInputType : InputObjectGraphType<UserModel>
    {
        public UpdateUserInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("FirstName")
                .Resolve(context => context.Source.FirstName);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("LastName")
               .Resolve(context => context.Source.LastName);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("MiddleName")
               .Resolve(context => context.Source.MiddleName);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Login")
               .Resolve(context => context.Source.Login);
            
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Password")
               .Resolve(context => context.Source.Password);

            Field<StringGraphType, string>()
               .Name("Email")
               .Resolve(context => context.Source.Email);

            Field<StringGraphType, string>()
               .Name("PhoneNumber")
               .Resolve(context => context.Source.PhoneNumber);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
               .Name("DateOfBirth")
               .Resolve(context => context.Source.DateOfBirth);

            Field<NonNullGraphType<UserRoleType>, UserRoleEnum>()
               .Name("Role")
               .Resolve(context => context.Source.Role);

            Field<IdGraphType, Guid?>()
               .Name("GradeId")
               .Resolve(context => context.Source.GradeId);
        }
    }
}
