using EducationalPortal.Business.Enums;
using EducationalPortal.Business.Models;
using EducationalPortal.Business.Repositories;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth.DTO;
using EducationalPortal.Server.Services;
using GraphQL;
using GraphQL.Types;

namespace EducationalPortal.Server.GraphQL.Modules.Auth
{
    public class AuthMutations : ObjectGraphType, IMutationMarker
    {
        public AuthMutations(IUserRepository usersRepository, AuthService authService)
        {
            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Login")
                .Argument<NonNullGraphType<LoginAuthInputType>, LoginAuthInput>("LoginAuthInputType", "Argument for login User")
                .ResolveAsync(async context =>
                {
                    LoginAuthInput loginAuthInput = context.GetArgument<LoginAuthInput>("LoginAuthInputType");
                    return new AuthResponse()
                    {
                        Token = await authService.AuthenticateAsync(loginAuthInput),
                        User = await usersRepository.GetByLoginAsync(loginAuthInput.Login),
                    };
                });

            Field<NonNullGraphType<AuthResponseType>, AuthResponse>()
                .Name("Register")
                .Argument<NonNullGraphType<LoginAuthInputType>, LoginAuthInput>("LoginAuthInputType", "Argument for register User")
                .ResolveAsync(async context =>
                {
                    List<UserModel> allUsers = await usersRepository.GetAsync();
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
                        Token = await authService.AuthenticateAsync(loginAuthInput),
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
