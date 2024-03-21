using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.ViewModels.Services
{
    internal static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddTransient<MainViewModel>()
            .AddTransient<GoalViewModel>();
    }
}