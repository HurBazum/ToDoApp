using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL
{
    public class GoalContext : DbContext
    {
        public DbSet<Goal> Goals { get; set; }

        public GoalContext(DbContextOptions<GoalContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}