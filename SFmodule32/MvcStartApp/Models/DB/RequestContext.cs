using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcStartApp.Models.Db;

namespace MvcStartApp.Models.DB
{
    public sealed class RequestContext : DbContext
    {
        public DbSet<Request> DbRequests { get; set; }

        public RequestContext(DbContextOptions<RequestContext> options) : base(options)
        {
           Database.EnsureCreated();
        }
    }
}
