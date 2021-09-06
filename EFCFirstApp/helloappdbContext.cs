using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EFCFirstApp
{
    public partial class helloappdbContext : DbContext
    {
        public helloappdbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public helloappdbContext(DbContextOptions<helloappdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
               //optionsBuilder.LogTo(System.Console.WriteLine); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
