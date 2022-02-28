using EducationalPortal.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Users.DTO
{
    public class CreateUserInputType : InputObjectGraphType<UserModel>
    {
        public CreateUserInputType()
        {
            Field<StringGraphType>()
               .Name("FirstName")
               .Resolve(context => context.Source.FirstName);

            Field<StringGraphType>()
               .Name("LastName")
               .Resolve(context => context.Source.LastName);

            Field<StringGraphType>()
               .Name("MiddleName")
               .Resolve(context => context.Source.MiddleName);

            Field<StringGraphType>()
               .Name("Login")
               .Resolve(context => context.Source.Login);

            Field<StringGraphType>()
               .Name("Email")
               .Resolve(context => context.Source.Email);

            Field<BooleanGraphType>()
               .Name("IsEmailConfirmed")
               .Resolve(context => context.Source.IsEmailConfirmed);

            Field<StringGraphType>()
               .Name("PhoneNumber")
               .Resolve(context => context.Source.PhoneNumber);

            Field<DateTimeGraphType>()
               .Name("DateOfBirth")
               .Resolve(context => context.Source.DateOfBirth);

            Field<UserRoleType>()
               .Name("Role")
               .Resolve(context => context.Source.Role);

            Field<IdGraphType>()
               .Name("GradeId")
               .Resolve(context => context.Source.GradeId);
        }
    }
}
