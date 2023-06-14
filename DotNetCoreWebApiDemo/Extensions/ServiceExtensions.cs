using Common.Interface;
using Common.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DotNetCoreWebApiDemo.Extensions
{
    public static class ServiceExtensions
    {
        //CORS Policy
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }

        //Authentication Policy
        public static void ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key"))),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        //Swagger Gen Security Configuration
        public static void SwaggerGenSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration["Application_Title"],
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = configuration.GetValue<string>("Jwt:Description")
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                   Type = ReferenceType.SecurityScheme,
                                   Id = "Bearer"
                                }
                            },
                        new string[]
                        {
                        }
                    }
                });
            });
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // ************************** Services Configuration *****************************
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<ISerializeService, SerializeService>();
            services.AddScoped<ICustomLogger, CustomLogger>();
            services.AddScoped<IDeserializeService, DeserializeService>();
            //services.AddScoped<IAuthorizeRepository, AuthorizeRepository>();

            // ************************** Servivces Configuration STORE *****************************
            //services.AddTransient<IUsersService, UsersService>();
            //services.AddScoped<IStoreUnitOfWork, StoreUnitOfWork>();


            // ************************** Database Configuration *****************************
            //services.AddDbContext<DemoDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("NTPEPConnection"),
            //sqlServerOptions => sqlServerOptions.CommandTimeout(600)));
        }

        //ApiController model state verification skip
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
