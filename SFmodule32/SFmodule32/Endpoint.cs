namespace SFmodule32
{
    public static class Endpoint
    {
        public static void About(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context => await context.Response.WriteAsync($"{env.ApplicationName}- ASP.Net Core tutorial project"));
        }

        public static void Config(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context => await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}"));
            
        }
    }
}
