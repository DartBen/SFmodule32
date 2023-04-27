using MvcStartApp.Models.Db;
using MvcStartApp.Models.DB;

namespace MvcStartApp.Controllers
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private IRequestRepository _repository;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestRepository requestRepository)
        {
            _repository = requestRepository;
            LogConsole(context);
            await LogFile(context);
            await LogBD(context);
            await _next.Invoke(context);
        }

        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFile(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        private async Task LogBD(HttpContext context)
        {
            //context.RequestServices
            var request = new Request() { Date = DateTime.Now, Id = Guid.NewGuid(), Url = $"http://{context.Request.Host.Value + context.Request.Path}" };
            await _repository.AddRequest(request);
        }
    }
}
