using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Models.DB
{
    public class RequestRepository : IRequestRepository
    {
        private RequestContext _context;
        public RequestRepository(RequestContext requestContext)
        {
            _context = requestContext;
        }
        public async Task AddRequest(Request request)
        {
            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.DbRequests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequest()
        {
            return await _context.DbRequests.ToArrayAsync();
        }
    }
}
