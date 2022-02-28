using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Auth.DTO
{
    public class AuthResponseType : ObjectGraphType<AuthResponse>
    {
        public AuthResponseType()
        {
            Field<UserType>()
                .Name("User")
                .Resolve(context => context.Source.User);

            Field<StringGraphType>()
                .Name("Token")
                .Resolve(context => context.Source.Token);
        }
    }
}
