using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Views.Windows;

namespace ToDoApp.ViewModels.Services
{
    internal class ViewModelLocator
    {
        public static MainViewModel MainViewModel => App.Host.Services.GetRequiredService<MainViewModel>(); 
    }
}