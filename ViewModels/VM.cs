using ClientTestSignalR_1.Commands;
using ClientTestSignalR_1.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace ClientTestSignalR_1.ViewModels
{
    public class VM : BaseVM
    {
        #region == Constructor ====================================================================================================
        public VM ()
        {
            if (Program.host != null) 
            {         
                //получаем сервис работы с сервером
                connectionServer = Program.host.Services.GetService<IConnectionService> ();

                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";

                    connectionServer.MessageListObj = MessageList;
                }
            }          
        }

        #endregion == Constructor ==

        #region == Fields ====================================================================================================
        
        /// <summary>
        /// сервис для работы с сервером получаем в конструкторе класса
        /// </summary>
        IConnectionService? connectionServer;

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
        
        /// <summary>
        /// путь запроса в формате "/str"
        /// </summary>
        public string RequestPath
        {
            get => _requestPath;

            set
            {
                _requestPath = value;
                OnPropertyChanged(nameof(RequestPath));
                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";
                }
            }
        }
        string _requestPath = "/str";

        /// <summary>
        /// адрес сервера для подкдлючения в формате "https://localhost:7018"
        /// </summary>
        public string ServerAddress
        {
            get => _serverAddress;

            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
                if (connectionServer != null)
                {
                    connectionServer.Address = $"{ServerAddress}{RequestPath}";
                }
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
        /// история сообщений
        /// </summary>
        public ObservableCollection<string> MessageList
        {
            get => _messageList;

            set
            {
                _messageList = value;
                OnPropertyChanged(nameof(MessageList));
                if (connectionServer != null)
                {
                    connectionServer.MessageListObj = MessageList;
                }
            }
        }
        ObservableCollection<string> _messageList = new ObservableCollection<string>();

        #endregion == Properties ==

        #region == Commands ===================================================================================================

        /// <summary>
        /// по кнопке Подключиться - подключение к серверу
        /// </summary>
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

        /// <summary>
        ///по кнопке Стоп - отключиться от сервера
        /// </summary>
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

        /// <summary>
        /// по кнопке Отправить - отправить сообщение на сервер
        /// </summary>
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

        /// <summary>
        /// по кнопке Очистить - очистка чата
        /// </summary>
        DelegateCommand? сommandClear;
        public ICommand CommandClear
        {
            get
            {
                if (сommandClear == null)
                {
                    сommandClear = new DelegateCommand(Clear);
                }
                return сommandClear;
            }
        }

        #endregion == Commands ==

        #region == Methods for Commands ===================================================================================================

        /// <summary>
        /// подключение к серверу
        /// </summary>
        /// <param name="obj"></param>
        private void Connect(object? obj)
        {
            if (connectionServer != null)
            {                
                ButtonConnectEnable = false;

                ButtonSendEnable = true;

                ButtonDisconnectEnable = true;

                connectionServer.Connect();
            }
            else
            {
                MessageBox.Show("\"Сервис подключения к серверу не активирован");
            }
        }

        /// <summary>
        /// отключение от сервера
        /// </summary>
        /// <param name="obj"></param>
        private void Disconnect(object? obj)
        {
            if (connectionServer != null)
                {
                    connectionServer.Disconnect();

                    ButtonDisconnectEnable = false;

                    ButtonConnectEnable = true;

                    ButtonSendEnable = false;
            }
            else
            {
                MessageBox.Show("\"Сервис подключения к серверу не активирован");
            }            
        }

        /// <summary>
        /// отправить сообщение серверу
        /// </summary>
        /// <param name="obj"></param>
        private void SendMessage(object? obj)
        {
           
          //отправка сообщения на сервер
            if (connectionServer != null)
            {
                connectionServer.SendMessage(Nickname, OutputMessage);
            }
            else
            {
                MessageBox.Show("Сервис подключения к серверу не активирован. Нажмите Стоп и проверьте параметры подключения");
            }            
        }

        /// <summary>
        /// очистка чата
        /// </summary>
        /// <param name="obj"></param>
        private void Clear(object? obj)
        {
            MessageList.Clear();
        }

        #endregion == Methods for Commands ==

        #region == Methods ===================================================================================================

        #endregion == Methods ==

    }
}
