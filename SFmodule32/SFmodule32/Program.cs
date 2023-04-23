using Microsoft.AspNetCore.Builder;

namespace SFmodule32
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                // Для логирования данных о запросе используем свойства объекта HttpContext
                Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
                await next.Invoke();
            });

            app.Map("/about", appBuilder => Endpoint.About(app, app.Environment));
            app.Map("/config", appBuilder => Endpoint.Config(app, app.Environment));


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. " +
                    $"App running configuration: {app.Environment.EnvironmentName}");
                });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });

            app.Run();
        }
    }
    public static class Endpoint{

        public static void About(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{env.ApplicationName}- ASP.Net Core tutorial project");
            });
        }

        public static void Config(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}");
            });
        }
    }


}