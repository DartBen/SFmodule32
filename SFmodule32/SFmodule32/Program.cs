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

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                // Для логирования данных о запросе используем свойства объекта HttpContext
                Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
                await next.Invoke();
            });

            _ = app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}"); });
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Welcome to the {app.Environment.ApplicationName}!");
            });

            app.Run();
        }
    }
}