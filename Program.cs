using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_1
{
    /// <summary>
    /// класс для создания своей точки входа в приложения, организации Hosting, организации Dependency Injection
    /// </summary>
    public static class Program 
    {
        [STAThread] //необходим обязательный атрибут 
        public static void Main() // метод - точка входа в приложение (можно скопировать из файла .../obj/Debug/.../App.g.cs - 64я строчка
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
        //теперь мы контролируем факт запуска приложения в его самой начальной точке

        //метод в котором мы будем создавать Host. Обязательно: он должен быть публичным; должен возвращать интерфейс IHostBuilder;
        //должен иметь имя CreateHostBuilder 
        public static IHostBuilder CreateHostBuilder(string[] Args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(Args);

            //настраиваем ряд конфигурационных возможностей
            hostBuilder.UseContentRoot(Environment.CurrentDirectory); //рабочий каталог нашего приложения

            //добавляем конфигурацию нашего приложения (host - ссылка на host, cfg - ссылка на объект конфигурации
            hostBuilder.ConfigureAppConfiguration((host, cfg) => 
            {
                //добавляем базовый путь
                cfg.SetBasePath(Environment.CurrentDirectory);

                // добавляем файл конфигурации с выбранным именем,
                // optional:true - означает, что если файла нет, то ошибки не будет
                // reloadOnChange:true - означает, что файл надо перечитать, если система обнаруживает, что он изменился
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange:true);

            });

            //у hostBuilder вызываем метод ConfigureServices, куда передаем свой метод конфигурации сервисов
            hostBuilder.ConfigureServices(App.ConfigureServices);
            //свой метод конфигурации сервисов, который мы тоже назвали ConfigureServises опишем в классе App

            //сами сервисы опишем внутри нашего приложения (App)


            return hostBuilder;
        }
    }
}
