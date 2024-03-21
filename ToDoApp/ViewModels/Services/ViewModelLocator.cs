using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.ViewModels.Services
{
    internal class ViewModelLocator
    {
        public static MainViewModel MainViewModel => App.Host.Services.GetRequiredService<MainViewModel>(); 
    }
}