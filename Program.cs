using ClientTestSignalR_1.Services;
using ClientTestSignalR_1.Services.Interfaces;
using ClientTestSignalR_1.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ClientTestSignalR_1
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            // создаем хост приложения            
            host = Host.CreateDefaultBuilder()
                
                // внедряем сервисы
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();

                    //Services
                    services.AddSingleton<IWriteMessageService, WriteMessageListService>();
                    services.AddSingleton<IConnectionService, ConnectionServer>();

                    //ViewModels
                    services.AddTransient<VM>();
                })
                .Build();
            
            // получаем сервис - объект класса App           
            App? app = host.Services.GetService<App>();

            // запускаем приложение
            app?.Run();
        }
        public static IHost? host;
    }
}
