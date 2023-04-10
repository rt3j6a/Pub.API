using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Pub.Core.Provider;
using System.Text;

namespace Pub.API.Common {
    public static class DependencyInjection {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration) {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            return services;
        }

        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DBProvider>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PubQuizDB"),
                b => b.MigrationsAssembly(typeof(DBProvider).Assembly.FullName)), ServiceLifetime.Transient);

            //  services.AddScoped<DBProvider>(provider => provider.GetService<DBProvider>());

            return services;
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseMiddleware<ExceptionHandler>();
        }

        public static void ConfigureLogger(this IApplicationBuilder app) {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            //A logger service konfigurálása.
            //services.AddTransient<ILoggerManager, LoggerService>();
            //app.UseMiddleware<Logger>();
        }
    }
}
