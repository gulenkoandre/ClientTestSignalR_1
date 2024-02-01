using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_1
{
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
    }
}
