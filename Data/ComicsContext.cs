using Microsoft.EntityFrameworkCore;
using MvcComicsExamen.Models;

namespace MvcComicsExamen.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options)
            : base(options)
        {
        }
        public DbSet<Comic> Comic { get; set; }
    }
}
