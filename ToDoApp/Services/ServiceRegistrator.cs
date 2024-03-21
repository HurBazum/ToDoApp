using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IGoalService, GoalService>();
    }
}