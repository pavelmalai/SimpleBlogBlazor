using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleBlogBlazor.API.DataAccess;
using SimpleBlogBlazor.Shared.Models;
using System;

namespace SimpleBlogBlazor.API
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<BloggingContext>(options => options.UseInMemoryDatabase(databaseName: "Blog"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BloggingContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData(dbContext);
            }
            app.UseCors(options => options.WithOrigins("https://localhost:5001").AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void SeedData(BloggingContext db)
        {
            db.Posts.Add(new Post
            {
                PostId = Guid.NewGuid(),
                Title = "Article 1",
                Content = "content1",
                CreatedOn = DateTime.Now
            });
            db.Posts.Add(new Post
            {
                PostId = Guid.NewGuid(),
                Title = "Article 2",
                Content = "Content 2",
                CreatedOn = DateTime.Now
            });
            db.SaveChanges();
        }
    }
}
