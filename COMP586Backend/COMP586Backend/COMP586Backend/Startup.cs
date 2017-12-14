using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using COMP586Backend.Data;


namespace COMP586Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure service to address the cross origin resource (CORS) sharing error.
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            //services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
            //{
            //    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Cors.Internal.CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            //});


        // Dependency Injection to configure the context for entity framework (EF) for a Currency Wallet database of users, wallets, and transactions.
        services.AddDbContext<CurrencyWalletContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection for the Identity Framework.
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CurrencyWalletContext>();

            // A secure sign in key phrase usually stored in a secure config.
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()     // Assigned the signInKey and validation properties
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,       // Change to true when trying to make more secure.
                    ValidateLifetime = false,          // Change to true when trying to make more secure.
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. It configuresthe HTTP requests.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Use the Cors policy created in the service configuration.
            app.UseCors("Cors");

            // Identity Authentication.
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
