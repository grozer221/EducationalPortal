using EducationalPortal.MsSql.Extensions;
using EducationalPortal.Server.Extensions;
using EducationalPortal.Server.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .WithMethods("POST")
               .WithOrigins("https://localhost:44481");
    });
});

builder.Services.AddJwtAuthorization();
builder.Services.AddMsSql();
builder.Services.AddGraphQLApi();
builder.Services.AddServices();

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
app.UseGraphQL<AppSchema>()
    .UseGraphQLUpload<AppSchema>();
app.UseGraphQLAltair();

app.MapFallbackToFile("index.html"); ;

app.Run();