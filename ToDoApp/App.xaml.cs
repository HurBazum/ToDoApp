using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using ToDoApp.Services;
using ToDoApp.ViewModels.Services;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static IHost? _host;

        public static IHost Host => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            host.StartAsync().ConfigureAwait(false);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddViewModels()
            .AddDatabase()
            .AddRepositories()
            .AddServices();

        public static string CurrentDirectory => Environment.CurrentDirectory;
    }
}