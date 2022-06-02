﻿using EducationalPortal.Business.Abstractions;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Grades
{
    public class GradeType : BaseType<GradeModel>
    {
        public GradeType() : base()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(context => context.Source.Name);
            
            Field<NonNullGraphType<GetEntitiesResponseType<UserType, UserModel>>, GetEntitiesResponse<UserModel>>()
               .Name("Students")
               .Argument<NonNullGraphType<IntGraphType>, int>("Page", "Argument for get Subjects Posts")
               .ResolveAsync(async context =>
               {
                   int page = context.GetArgument<int>("Page");
                   Guid gradeId = context.Source.Id;
                   var userRepository = context.RequestServices.GetRequiredService<IUserRepository>();
                   return await userRepository.WhereOrDefaultAsync(s => s.CreatedAt, Order.Descend, page, p => p.GradeId == gradeId);
               })
               .AuthorizeWith(AuthPolicies.Teacher);
        }
    }
}
