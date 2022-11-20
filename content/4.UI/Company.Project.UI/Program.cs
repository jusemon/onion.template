using System.Reflection;
using System.Text;
using Company.Project.Domain.Entities.Config;
using Company.Project.Infra.Data.Contexts;
using Company.Project.Infra.IoC.ConfigureServicesExtensions;
using Company.Project.Infra.Utils.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var authConfig = builder.Configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.ConfigureRepository();
builder.Services.ConfigureService();
builder.Services.ConfigureApplication();
builder.Services.Configure<MvcNewtonsoftJsonOptions>(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

bool tryParse = Boolean.TryParse(Environment.GetEnvironmentVariable("ENABLE_SWAGGER"), out bool enableSwagger);

if (app.Environment.IsDevelopment() || (tryParse && enableSwagger))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
