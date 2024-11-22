using Microsoft.EntityFrameworkCore;
using RPSGame.Domain;

namespace RPSGame.DataAccess
{
    public class SqlContext :DbContext
    {
        public DbSet<Player> Players { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
            this.Database.Migrate();
        }
    }
}
