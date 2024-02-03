// Ignore Spelling: App
using System.Windows;

namespace ClientTestSignalR_1
{
    public class App:Application
    {
        readonly MainWindow mainWindow;

        // в конструкторе через систему внедрения зависимостей получаем объект главного окна
        public App(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        //OnStartup - метод который запускается при старте приложения
        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();  // отображаем главное окно на экране
            base.OnStartup(e);
        }
    }
}
