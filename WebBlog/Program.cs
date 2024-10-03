using Microsoft.EntityFrameworkCore;
using WebBlog.Models.Context;
using WebBlog.Models.Entities;
using WebBlog.Repository;

namespace WebBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BlogDBContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("myDB"));
                }
                );
            builder.Services.AddScoped<IMainRepository<User>, UserRepository>();
            builder.Services.AddScoped<IMainRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IMainRepository<Post>, PostRepository>();
            builder.Services.AddScoped<IMainRepository<Comment>, CommentRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PostRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
