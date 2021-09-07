using Microsoft.EntityFrameworkCore;

namespace EFCFirstApp
{
    public class linqExampledbContext : DbContext
    {
        public DbSet<Company_Linq> Companies { get; set; }
        public DbSet<User_Linq> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}
