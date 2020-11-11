using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopMate.Persistence;
using ShopMate.Persistence.Relational;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMate
{
    public class Startup
    {
        private const string ConnectionStringName = "ShopMateContext";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddOpenApiDocument(config =>
            {
                config.Title = "ShopMate";
                config.DocumentName = "Dev";
                config.Version = "0.0.0";
            });

            ConfigurePersistenceServices(services);

            ConfigureAuthenticationServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        private void ConfigurePersistenceServices(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString(ConnectionStringName))
            {
                Password = Configuration[$"Database:{ConnectionStringName}:Password"]
            };

            services.AddDbContext<ShopMateContext>(options =>
                options.UseSqlServer(builder.ConnectionString));

            services.AddScoped<IShopMateRepository, RelationalShopMateRepository>();
        }

        private void ConfigureAuthenticationServices(IServiceCollection services)
        {
            var secret = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = !Configuration.GetValue<bool>("Jwt:UnsafeMode");
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuers = Configuration.GetSection("Jwt:Issuers").Get<List<string>>(),
                    ValidAudiences = Configuration.GetSection("Jwt:Audiences").Get<List<string>>(),
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        }
    }
}
