using ClientTestSignalR_1.Services;
using ClientTestSignalR_1.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    services.AddTransient<WriteMessageListService>();
                    services.AddTransient<ConnectionServer>();
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
