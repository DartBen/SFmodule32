namespace SFmodule32.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Для логирования данных о запросе используем свойства объекта HttpContext
            string log = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}";
            string pathToLoger = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");
            Console.WriteLine(log);

            using (StreamWriter streamWriter = new StreamWriter(File.Open(pathToLoger, FileMode.Append)))
            {
                streamWriter.WriteLineAsync(log);
            }

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
