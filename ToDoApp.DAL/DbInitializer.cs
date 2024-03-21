using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.DAL;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Repositories;
using ToDoApp.Interfaces;

namespace ToDoApp.Services
{
    public static class DbInitializer
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services) => services
            .AddDbContext<GoalContext>(options => options.UseSqlite("Filename=GoalDB.db"));
    }

    public static class RepositoryInitializer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IRepository<Goal>, GoalRepository>();
    }

    public class JSONBuilder
    {
        public string Type { get; set; }
        public Dictionary<string, string> ConnectionStrings { get; set; } = new();
    }
}