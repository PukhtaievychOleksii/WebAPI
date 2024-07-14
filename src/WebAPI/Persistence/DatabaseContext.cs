
using Microsoft.EntityFrameworkCore;

using WebAPI.Domain;

public class DatabaseContext: DbContext{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlite("Data Source=reservationSystem.db");
    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
    public DbSet<Book> Books {get; set;}
    public DbSet<User> Users{get; set;}
 
}
    
