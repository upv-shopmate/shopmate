using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopMate.Persistence;
using ShopMate.Persistence.Relational;
using System;

namespace ShopMate
{
    public class Startup
    {
        const string ConnectionStringName = "ShopMateContext";

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

            services.AddOpenApiDocument(config => {
                config.Title = "ShopMate";
                config.DocumentName = "Dev";
                config.Version = "0.0.0";
            });

            ConfigurePersistence(services);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }

        private void ConfigurePersistence(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString(ConnectionStringName))
            {
                Password = Configuration[$"Database:{ConnectionStringName}:Password"]
            };

            services.AddDbContext<ShopMateContext>(options =>
                options.UseSqlServer(builder.ConnectionString));

            services.AddScoped<IShopMateRepository, RelationalShopMateRepository>();
        }
    }
}
