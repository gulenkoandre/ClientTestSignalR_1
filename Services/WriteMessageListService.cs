using ClientTestSignalR_1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClientTestSignalR_1.Services
{
    /// <summary>
    /// передача сообщения message ListBox с указанием даты и времени
    /// </summary>
    public class WriteMessageListService : IWriteMessageService
    {
        public void WriteMessage(object? obj, string message)
        {
            try
            {
                //обрабатываем в главном потоке
                App.Current.Dispatcher.Invoke(() =>
                {

                    if (obj != null)
                    {
                        ((ObservableCollection<string>)obj).Add($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}/ {message}");
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
