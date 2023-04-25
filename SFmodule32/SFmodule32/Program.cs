using SFmodule32.Middlewares;
using System.Reflection.PortableExecutable;

namespace SFmodule32
{
    public class Program
    {
        public static async void Main(string[] args)
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

            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. " +
                    $"App running configuration: {app.Environment.EnvironmentName}");
                });

                //endpoints.MapGet("/about",async () => await Endpoint.About(app, app.Environment));
            });

            // из-за длительности обработки запроса Map происходит блокировка всего остального
            // нужно думать о многопоточности
            app.Map("/about", () => Endpoint.About(app, app.Environment));

            //endpoints.MapGet("/about", async context =>
            //{
            //    await context.Response.WriteAsync($"{app.Environment.ApplicationName}- ASP.Net Core tutorial project");
            //});

            app.Map("/config",  () =>  Endpoint.Config(app, app.Environment));
            //endpoints.MapGet("/config", async context =>
            //{
            //    await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. " +
            //    $"App running configuration: {app.Environment.EnvironmentName}"); ;
            //});

            app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

            app.Run();
        }
    }
}