using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MvcStartApp.Controllers;
using MvcStartApp.Models.DB;

namespace MvcStartApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            string logConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connection);

            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));
            builder.Services.AddTransient<IBlogRepository, BlogRepository>();

            builder.Services.AddDbContext<RequestContext>(options => options.UseSqlServer(logConnection));
            builder.Services.AddTransient<IRequestRepository, RequestRepository>();
            //builder.Services.AddTransient<RequestRepository>();

            builder.Services.AddControllersWithViews();

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

            app.UseMiddleware<LoggingMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Users",
                pattern: "{controller=Users}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "Feedback",
                pattern: "{controller=Feedback}/{action=Add}/{id?}");

            app.Run();
        }
    }
}