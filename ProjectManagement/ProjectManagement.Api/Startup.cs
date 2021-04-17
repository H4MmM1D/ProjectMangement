using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.Api.Data;
using ProjectManagement.Api.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSwaggerGen();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ProjectOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Project API",
                    Version = "1",
                    Description = "Project API"
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("TaskOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Task API",
                    Version = "1",
                    Description = "Task API"
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("UserOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "User API",
                    Version = "1",
                    Description = "User API"
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("MemberOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Member API",
                    Version = "1",
                    Description = "Member API"
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("MeetingOpenApi", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Meeting API",
                    Version = "1",
                    Description = "Meeting API"
                });
            });

            services.AddAutoMapper(typeof(Mappings));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ProjectOpenApi/swagger.json", "Project API");
                options.SwaggerEndpoint("/swagger/TaskOpenApi/swagger.json", "Task API");
                options.SwaggerEndpoint("/swagger/UserOpenApi/swagger.json", "User API");
                options.SwaggerEndpoint("/swagger/MemberOpenApi/swagger.json", "Member API");
                options.SwaggerEndpoint("/swagger/MeetingOpenApi/swagger.json", "Meeting API");
                options.RoutePrefix = "";
            });

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
