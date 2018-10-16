using Microsoft.EntityFrameworkCore;
using Missio.Users;

namespace MissioServer
{
    public class MissioContext : DbContext
    {
        public MissioContext(DbContextOptions<MissioContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}