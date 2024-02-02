using ClientTestSignalR_1.Services.Interfaces;
using ClientTestSignalR_1.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ClientTestSignalR_1.Services
{
    /// <summary>
    /// сервис передачи сообщений на сервер посредством HubConnection
    /// </summary>
    class ConnectionServer : IConnectionService
    {
        #region == Constructor ==========================================================================================

        public ConnectionServer()
        {
            if (Program.host != null)
            {
                writeMessageListService = Program.host.Services.GetService<WriteMessageListService>(); //сервис для добавления сообщений в общий список
            }
        }

        #endregion == Constructor ==

        #region == Fields ==========================================================================================

        //private readonly IConnectionService? _connectionService;

        /// <summary>
        /// текущее соединение
        /// </summary>
        HubConnection? connection;

        /// <summary>
        /// сервис добавления сообщения в чат
        /// </summary>
        IWriteMessageService? writeMessageListService = null;

        #endregion == Fields ==
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);                
            }
            
        }
    }
}
