using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Models;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth.DTO;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPortal.Server.GraphQL.Modules.Auth
{
    public class AuthMutations : ObjectGraphType, IMutationMarker
    {
        public AuthMutations(UserRepository usersRepository, AuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Login")
                .Argument<NonNullGraphType<LoginAuthInputType>, LoginAuthInput>("LoginAuthInputType", "Argument for login User")
                .Resolve(context =>
                {
                    LoginAuthInput loginAuthInput = context.GetArgument<LoginAuthInput>("LoginAuthInputType");
                    return new AuthResponse()
                    {
                        Token = authService.Authenticate(loginAuthInput),
                        User = usersRepository.GetByLogin(loginAuthInput.Login),
                    };
                });

            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Register")
                .Argument<NonNullGraphType<LoginAuthInputType>, LoginAuthInput>("LoginAuthInputType", "Argument for register User")
                .ResolveAsync(async context =>
                {
                    List<UserModel> allUsers = usersRepository.Get().ToList();
                    if (allUsers.Count > 0)
                        throw new Exception("Ви не можете самостійно зареєструватися. Зверніться до адміністратора");

                    LoginAuthInput loginAuthInput = context.GetArgument<LoginAuthInput>("LoginAuthInputType");
                    UserModel user = await usersRepository.CreateAsync(new UserModel
                    {
                        Login = loginAuthInput.Login,
                        Password = loginAuthInput.Password,
                        Role = UserRoleEnum.Administrator,
                    });
                    return new AuthResponse()
                    {
                        Token = authService.Authenticate(loginAuthInput),
                        User = user,
                    };
                });
            
            Field<BooleanGraphType, bool>()
                .Name("ChangePassword")
                .Argument<NonNullGraphType<ChangePasswordInputType>, ChangePassword>("ChangePasswordInputType", "Argument for change User password")
                .ResolveAsync(async context =>
                {
                    LoginAuthInput loginAuthInput = context.GetArgument<LoginAuthInput>("LoginAuthInputType");
                    return true;
                });
        }
    }
}
