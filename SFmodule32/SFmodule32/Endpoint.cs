using System.Runtime.CompilerServices;

namespace SFmodule32
{
    public static class Endpoint
    {
        public static void About(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.Run(async context => {
                var str = $"{env.ApplicationName}- ASP.Net Core tutorial project";
                Console.WriteLine(str);
                await context.Response.WriteAsync("aasda");
                });
        }

        public static async Task Config(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var str = $"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}";
            Console.WriteLine(str);
            app.Run(handler: async context => { await context.Response.WriteAsync(str); });
                   

        }
    }
}

