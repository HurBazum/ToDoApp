using Microsoft.Extensions.Hosting;

namespace ToDoApp
{

    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(App.ConfigureServices);
    }
}