using Microsoft.EntityFrameworkCore;

namespace TestForDotNet.Models
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options)
            : base(options)
        { }
        public DbSet<TarefaItem> TarefaItems { get; set;}
    }
}
