namespace Company.Project.UI
{
    using Domain.Entities.Config;
    using Infra.Data.Contexts;
    using Infra.IoC.ConfigureServicesExtensions;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Startup class. 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var authConfig = Configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
            var dbConfig = Configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSpaStaticFiles(c => c.RootPath = "ClientApp/dist/ClientApp");
            services.AddDbContext<SecurityContext>(options => options.UseSqlite(dbConfig.ConnectionString), ServiceLifetime.Singleton);
            services.ConfigureRepository();
            services.ConfigureService();
            services.ConfigureApplication();
            services.Configure<MvcJsonOptions>(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication(x =>
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AppTitle API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = authConfig.Description,
                    Name = authConfig.Name,
                    In = (Microsoft.OpenApi.Models.ParameterLocation) authConfig.In,
                    Type = (Microsoft.OpenApi.Models.SecuritySchemeType) authConfig.Type,
                    Reference = {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(scheme.Reference.Id, scheme);
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                    { scheme, Array.Empty<string>() }
                });
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppTitle V1");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
