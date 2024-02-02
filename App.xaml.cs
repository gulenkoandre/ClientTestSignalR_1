// Ignore Spelling: App

using ClientTestSignalR_1.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ClientTestSignalR_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //создаем Хост
        private static IHost? _host;
        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build(); //при первом обращении Хост будет создан, будут сконфигурированы все его сервисы и  ими можно пользоваться
                                                                                                                   //теперь через статическое поле Host класса App мы всегда можем обратиться к хосту


        //при запуске приложения, мы запускаем и Хост
        protected override async void OnStartup(StartupEventArgs e)
        {
            IHost host = Host;

            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        // при выходе из приложения, мы закрываем и Хост
        protected override async void OnExit(ExitEventArgs e)
        {
            IHost host = Host;

            base.OnExit(e);

            await host.StopAsync().ConfigureAwait(false);

            host.Dispose();

            _host = null;
        }

        /// <summary>
        /// метод конфигурации сервисов (забирается в классе Program)
        /// </summary>
        /// <param name="host"></param>
        /// <param name="services"></param>
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //здесь добавляем все сервисы, которые нам потребуются для дальнейшей работы
            services.AddSingleton<MainWindow>();
            services.AddSingleton<VM>();

            //AddTransient() создает transient-объекты. Такие объекты создаются при каждом обращении к ним.
            //AddScoped() создает один экземпляр объекта для всего запроса.
            //AddSingleton() создает один объект для всех последующих запросов, при этом объект создается только тогда, когда он непосредственно необходим.
        }
    }

}
