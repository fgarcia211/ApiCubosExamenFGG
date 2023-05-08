using ApiCubosExamenFGG.Data;
using ApiCubosExamenFGG.Helpers;
using ApiCubosExamenFGG.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

//BBDD
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddDbContext<CubosContext> (options => options.UseSqlServer(connectionString));

//SEGURIDAD
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Cubos";
    document.Description = "Api Cubos del examen";
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});
/*builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api BBDD Examen Cubos",
        Description = "Estamos realizando un Api con BBDD en Azure de cubos",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "Fernando",
        }
    });
});*/

var app = builder.Build();

app.UseOpenApi();
//app.UseSwaggerUi3();

//app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/bootstrap.css");
    options.InjectStylesheet("/css/material3x.css");
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    options.RoutePrefix = "";
    options.DocExpansion(DocExpansion.None);
});

app.UseCors("corsapp");

/*app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    options.RoutePrefix = "";
});*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
