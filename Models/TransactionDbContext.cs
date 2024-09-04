using Microsoft.EntityFrameworkCore;
namespace CustomsTransactionSystem.Models
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
    }
}

