using System.Data.Entity;

namespace GoDutchServer.Model
{
    public class DaoContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
    }
}