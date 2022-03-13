﻿using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Auth.DTO
{
    public class LoginAuthInputType : InputObjectGraphType<LoginAuthInput>
    {
        public LoginAuthInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Login")
                .Resolve(context => context.Source.Login);
            
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Password")
                .Resolve(context => context.Source.Password);
        }
    }
}
