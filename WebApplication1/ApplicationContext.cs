using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TeamMember> TeamMembers { get; set; } = null!;
        public DbSet<State> States { get; set; } = null!;
        public DbSet<Animal> Animals { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
           Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, StateDefinition = "Можете забрать" },
                new State { Id = 2, StateDefinition = "Забронировано"},
                new State { Id = 3, StateDefinition = "Уже забрали"}
        );

        }
    }
}
