using ClientTestSignalR_1.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using System.Windows.Threading;

namespace ClientTestSignalR_1.Services
{
    /// <summary>
    /// сервис передачи сообщений на сервер посредством HubConnection
    /// </summary>
    class ConnectionServer : IConnectionService
    {
        private readonly IWriteMessageService? writeMessageListService;
        
        #region == Constructor ==========================================================================================

        public ConnectionServer (IWriteMessageService writeMessageListService)
        {            
                this.writeMessageListService = writeMessageListService; //сервис для добавления сообщений в общий список           
        }

        #endregion == Constructor ==

        #region == Fields ==========================================================================================
        
        /// <summary>
        /// текущее соединение
        /// </summary>
        HubConnection? connection;

        #endregion == Fields ==

        #region == Properties ==========================================================================================

        /// <summary>
        /// адрес назначения соединения
        /// </summary>
        public string? Address
        {
            get; set;
        }

        /// <summary>
        /// объект лога сообщений, передаваемый через object
        /// </summary>
        public object? MessageListObj
        {
            get; set;
        }

        #endregion == Properties ==

        #region == Methods ==========================================================================================

        /// <summary>
        /// установление соединения
        /// </summary>
        public async void Connect()
        {
            try
            {
                //создание подключения к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl($"{Address}")
                    .Build();

                // регистрация функции Receive для получения данных с сервера
                connection?.On<string, string>("Receive", (user, message) =>
                {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        string? newMessage = $"{user}: {message}";

                        if (writeMessageListService != null) //добавлние сообщения в чат
                        {
                            writeMessageListService?.WriteMessage(MessageListObj, newMessage);
                        }
                    });
                });

                if (connection != null)
                {
                    await connection.StartAsync();
                }

                writeMessageListService?.WriteMessage(MessageListObj, "Соединение установлено");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка соединения с сервером ({ex.Message}). Нажмите Стоп и проверьте параметры подключения и доступность сервера");
            }
        }

        /// <summary>
        /// разрыв соединения
        /// </summary>
        public async void Disconnect()
        {
            //отключение
            if (connection != null)
            {
                await connection.StopAsync();
            }
            writeMessageListService?.WriteMessage(MessageListObj, "Соединение отключено");
        }

        /// <summary>
        /// отправить сообщение
        /// </summary>
        /// <param name="nickname">ник пользователя</param>
        /// <param name="message">сообщение</param>
        public async void SendMessage(string nickname, string message)
        {
            try
            {
                if (connection != null)
                {
                    await connection.InvokeAsync("Send", nickname, message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки данных на сервер ({ex.Message}). Нажмите Стоп и проверьте параметры подключения и доступность сервера");                
            }            
        }

        #endregion == Methods ==
    }
}
