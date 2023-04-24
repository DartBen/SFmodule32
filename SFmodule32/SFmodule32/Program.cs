using SFmodule32.Middlewares;
using System.Reflection.PortableExecutable;

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
            // обрабатываем ошибки HTTP
            app.UseStatusCodePages();

            //app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. " +
                    $"App running configuration: {app.Environment.EnvironmentName}");
                });
            });

            // из-за длительности обработки запроса Map происходит блокировка всего остального
            // нужно думать и многопоточности
            app.Map("/about", appBuilder => Endpoint.About(app, app.Environment));
            app.Map("/config", appBuilder => Endpoint.Config(app, app.Environment));

            app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

            app.Run();
        }
    }
}