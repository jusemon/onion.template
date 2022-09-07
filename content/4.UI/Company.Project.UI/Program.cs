using Company.Project.Domain.Entities.Config;
using Company.Project.Infra.Data.Contexts;
using Company.Project.Infra.IoC.ConfigureServicesExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var authConfig = builder.Configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
var dbConfig = builder.Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();

builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(c => c.RootPath = "ClientApp/dist/ClientApp");
builder.Services.AddDbContext<SecurityContext>(options => options.UseSqlite(dbConfig.ConnectionString), ServiceLifetime.Singleton);

builder.Services.ConfigureRepository();
builder.Services.ConfigureService();
builder.Services.ConfigureApplication();
builder.Services.Configure<MvcNewtonsoftJsonOptions>(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.Configure<MvcNewtonsoftJsonOptions>(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(authConfig.Key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AppTitle API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    var inasd = (Microsoft.OpenApi.Models.ParameterLocation)authConfig.In;
    var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = authConfig.Description,
        Name = authConfig.Name,
        In = (Microsoft.OpenApi.Models.ParameterLocation)authConfig.In,
        Type = (Microsoft.OpenApi.Models.SecuritySchemeType)authConfig.Type,
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(scheme.Reference.Id, scheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                    { scheme, Array.Empty<string>() }
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppTitle V1");
});


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.Run();
