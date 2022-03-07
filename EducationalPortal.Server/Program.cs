using EducationalPortal.Server.Database;
using EducationalPortal.Server.Database.Abstractions;
using EducationalPortal.Server.Database.Enums;
using EducationalPortal.Server.Database.Repositories;
using EducationalPortal.Server.GraphQL;
using EducationalPortal.Server.GraphQL.Abstraction;
using EducationalPortal.Server.GraphQL.Modules.Auth;
using EducationalPortal.Server.GraphQL.Modules.EducationalYears;
using EducationalPortal.Server.GraphQL.Modules.Grades;
using EducationalPortal.Server.GraphQL.Modules.SubjectPosts;
using EducationalPortal.Server.GraphQL.Modules.Subjects;
using EducationalPortal.Server.GraphQL.Modules.Users;
using GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .WithMethods("POST")
               .WithOrigins("https://localhost:44418");
    });
});

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 27))), 
    ServiceLifetime.Transient);
builder.Services.AddTransient(typeof(BaseRepository<>));
builder.Services.AddTransient<EducationalYearRepository>();
builder.Services.AddTransient<GradeRepository>();
builder.Services.AddTransient<SubjectPostRepository>();
builder.Services.AddTransient<SubjectRepository>();
builder.Services.AddTransient<UserRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = Environment.GetEnvironmentVariable("AuthValidAudience"),
        ValidIssuer = Environment.GetEnvironmentVariable("AuthValidIssuer"),
        RequireSignedTokens = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("AuthIssuerSigningKey"))),
    };
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
});

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//services.AddTransient<IDocumentExecuter, SubscriptionDocumentExecuter>();
builder.Services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

builder.Services.AddTransient<IQueryMarker, AuthQueries>();
builder.Services.AddTransient<IMutationMarker, AuthMutations>();
builder.Services.AddTransient<AuthService>();

builder.Services.AddTransient<IQueryMarker, EducationalYearsQueries>();
builder.Services.AddTransient<IMutationMarker, EducationalYearsMutations>();

builder.Services.AddTransient<IQueryMarker, GradesQueries>();
builder.Services.AddTransient<IMutationMarker, GradesMutations>();

builder.Services.AddTransient<IMutationMarker, SubjectPostsMutations>();

builder.Services.AddTransient<IQueryMarker, SubjectsQueries>();
builder.Services.AddTransient<IMutationMarker, SubjectsMutations>();

builder.Services.AddTransient<IQueryMarker, UsersQueries>();
builder.Services.AddTransient<IMutationMarker, UsersMutations>();

builder.Services.AddTransient<AppSchema>();
builder.Services
    .AddGraphQL(options =>
    {
        options.EnableMetrics = true;
        options.UnhandledExceptionDelegate = (context) =>
        {
            Console.WriteLine(context.Exception.StackTrace);
            context.ErrorMessage = context.Exception.Message;
        };
    })
    .AddSystemTextJson()
    .AddWebSockets()
    .AddDataLoader()
    .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Transient)
    .AddGraphQLAuthorization(options =>
    {
        options.AddPolicy(AuthPolicies.Authenticated, p => p.RequireAuthenticatedUser());
        options.AddPolicy(AuthPolicies.Student, p => p.RequireClaim(ClaimTypes.Role, UserRoleEnum.Student.ToString(), UserRoleEnum.Teacher.ToString(), UserRoleEnum.Administrator.ToString()));
        options.AddPolicy(AuthPolicies.Teacher, p => p.RequireClaim(ClaimTypes.Role, UserRoleEnum.Teacher.ToString(), UserRoleEnum.Administrator.ToString()));
        options.AddPolicy(AuthPolicies.Administrator, p => p.RequireClaim(ClaimTypes.Role, UserRoleEnum.Administrator.ToString()));
    });


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("DefaultPolicy");
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseWebSockets();
app.UseGraphQLWebSockets<AppSchema>();
app.UseGraphQL<AppSchema>();
app.UseGraphQLAltair();

app.MapFallbackToFile("index.html"); ;

app.Run();


string GetConnectionString()
{
    string? connectionString = Environment.GetEnvironmentVariable("JAWSDB_URL");
    if (string.IsNullOrEmpty(connectionString))
        return "server=localhost;database=educational-portal;user=root;password=;port=3306;";
    connectionString = connectionString.Split("//")[1];
    string user = connectionString.Split(':')[0];
    connectionString = connectionString.Replace(user, "").Substring(1);
    string password = connectionString.Split('@')[0];
    if (!string.IsNullOrEmpty(password))
        connectionString = connectionString.Replace(password, "");
    connectionString = connectionString.Substring(1);
    string server = connectionString.Split(':')[0];
    connectionString = connectionString.Replace(server, "").Substring(1);
    string port = connectionString.Split('/')[0];
    string database = connectionString.Split('/')[1];
    connectionString = $"server={server};database={database};user={user};password={password};port={port};";
    return connectionString;
}