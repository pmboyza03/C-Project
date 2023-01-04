using API01.Models;
using Microsoft.EntityFrameworkCore;

namespace API01.Data
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext > options)
            : base(options)
        {

        }
        public DbSet<Issue> Issues { get; set; }
    }
}
