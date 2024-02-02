using ClientTestSignalR_1.Commands;
using ClientTestSignalR_1.Services;
using ClientTestSignalR_1.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ClientTestSignalR_1.ViewModels
{
    public class VM : BaseVM
    {
        #region == Constructor ====================================================================================================
        public VM()
        {
            //_dispatcher = Dispatcher.CurrentDispatcher;
            
            if (Program.host != null) 
            {
                //получаем сервис записи сообщений в ListBox 
                //writeMessageListService = Program.host.Services.GetService<WriteMessageListService>();

                //получаем сервис работы с сервером
                connectionServer = Program.host.Services.GetService<ConnectionServer>();
            }
                        
        }

        #endregion == Constructor ==

        #region == Fields ====================================================================================================

        //HubConnection? connection; // объявляем подключение для работы с сервером (хабом)

        //public Dispatcher _dispatcher; // для работы с элементами WPF в главном потоке

        /// <summary>
        /// //для получения из Хаба сервиса записи сообщений в ListBox
        /// </summary>
        //public IWriteMessageService? writeMessageListService;

        /// <summary>
        /// сервис для работы с сервером
        /// </summary>
        public IConnectionService? connectionServer;

        #endregion == Fields ==

        #region == Properties ================================================================================================

        public bool ButtonSendEnable
        {
            get => _buttonSendEnable;

            set
            {
                _buttonSendEnable = value;
                OnPropertyChanged(nameof(ButtonSendEnable));
            }
        }
        bool _buttonSendEnable = false;

        public bool ButtonConnectEnable
        {
            get => _buttonConnectEnable;

            set
            {
                _buttonConnectEnable = value;
                OnPropertyChanged(nameof(ButtonConnectEnable));
            }
        }
        bool _buttonConnectEnable = true;

        public bool ButtonDisconnectEnable
        {
            get => _buttonDisconnectEnable;

            set
            {
                _buttonDisconnectEnable = value;
                OnPropertyChanged(nameof(ButtonDisconnectEnable));
            }
        }
        bool _buttonDisconnectEnable = false;

        public string RequestPath
        {
            get => _requestPath;

            set
            {
                _requestPath = value;
                OnPropertyChanged(nameof(RequestPath));
            }
        }
        string _requestPath = "/str";

        public string ServerAddress
        {
            get => _serverAddress;

            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
            }
        }
        string _serverAddress = "https://localhost:7018";


        /// <summary>
        /// имя в чате
        /// </summary>
        public string Nickname
        {
            get => _nickname;

            set
            {
                _nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }
        string _nickname = "Chat";

        /// <summary>
        /// исходящее сообщение
        /// </summary>
        public string OutputMessage
        {
            get => _outputMessage;

            set
            {
                _outputMessage = value;
                OnPropertyChanged(nameof(OutputMessage));
            }
        }
        string _outputMessage = "Test";

        /// <summary>
        /// входящее сообщение
        /// </summary>
        public string InputMessage
        {
            get => _inputMessage;

            set
            {
                _inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }
        string _inputMessage="";

        // <summary>
        /// история сообщений
        /// </summary>
        public ObservableCollection<string> MessageList
        {
            get => _messageList;

            set
            {
                _messageList = value;
                OnPropertyChanged(nameof(MessageList));
            }
        }
        ObservableCollection<string> _messageList = new ObservableCollection<string>();

        #endregion == Properties ==

        #region == Commands ===================================================================================================

        DelegateCommand? commandConnect;
        public ICommand CommandConnect
        {
            get
            {
                if (commandConnect == null)
                {
                    commandConnect = new DelegateCommand(Connect);
                }
                return commandConnect;
            }

        }

        DelegateCommand? commandDisconnect;
        public ICommand CommandDisconnect
        {
            get
            {
                if (commandDisconnect == null)
                {
                    commandDisconnect = new DelegateCommand(Disconnect);
                }
                return commandDisconnect;
            }

        }

        DelegateCommand? commandSendMessage;
        public ICommand CommandSendMessage
        {
            get
            {
                if (commandSendMessage == null)
                {
                    commandSendMessage = new DelegateCommand(SendMessage);
                }
                return commandSendMessage;
            }

        }

        #endregion == Commands ==

        #region == Methods for Commands ===================================================================================================

        private void Connect(object? obj) //private async void Connect(object? obj)
        {
            //OpenConnectionServer(ServerAddress, RequestPath);
            /*connection = new HubConnectionBuilder()
                .WithUrl($"{ServerAddress}{RequestPath}")
                .Build();*/
            try
            {
            
                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";

                    //connectionServer.ConnectionObj = connection;

                    connectionServer.MessageListObj = MessageList;

                    connectionServer.Connect();

                    //connection = connectionServer.ConnectionObj as HubConnection;
                }
                            
                ButtonConnectEnable = false;

                //подключение к хабу
               /* if (connection != null)
                {
                    await connection.StartAsync();
                }

                writeMessageListService?.WriteMessage(MessageList, "Соединение установлено");*/
                                
                ButtonSendEnable = true;

                ButtonDisconnectEnable = true;
            }
            catch (Exception ex)
            {
                ButtonConnectEnable = true;

                MessageBox.Show(ex.Message); 
            }
        }

        private void Disconnect(object? obj)
        {        
            try
            {
                ButtonDisconnectEnable = false;


                if (connectionServer != null)
                {
                    //connectionServer.ConnectionObj = connection;

                    connectionServer.MessageListObj = MessageList;

                    connectionServer.Disconnect();

                    //connection = connectionServer.ConnectionObj as HubConnection;
                }
                //подключение к хабу
                /*if (connection != null)
                 {
                     await connection.StopAsync();
                 }               

                 writeMessageListService?.WriteMessage(MessageList, "Соединение отключено");*/

                ButtonConnectEnable = true;

                ButtonSendEnable = false;
            }
            catch (Exception ex)
            {
                ButtonDisconnectEnable = true;

                MessageBox.Show(ex.Message); 
            }
        }

        private void SendMessage(object? obj)
        {
            try
            {
                //отправка сообщения на сервер
                if (connectionServer != null)
                {
                    connectionServer.SendMessage(Nickname, OutputMessage);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                 
            }
        }

        #endregion == Methods for Commands ==

        #region == Methods ===================================================================================================

        /*/// <summary>
        /// Открыть соединение с сервером
        /// </summary>
        /// <param name="serverAddress">в формате "https://localhost:7018" </param>
        /// <param name="requestPath"> путь запроса в формате "/chat" </param>
        private void OpenConnectionServer (string serverAddress, string requestPath) // serverAddress в формате "https://localhost:7018", путь запроса в формате "/str"
        {
            //создание подключения к хабу
            connection = new HubConnectionBuilder()
                .WithUrl($"{serverAddress}{requestPath}")
                .Build();

            // регистрация функции Receive для получения данных
            connection.On<string, string>("Receive", (user, message) =>
            {
                _dispatcher.Invoke(() =>
                {
                    string? newMessage = $"{user}: {message}";
                    
                    writeMessageListService?.WriteMessage(MessageList, newMessage);

                });
            });
            
        }*/
        #endregion == Methods ==

    }
}
