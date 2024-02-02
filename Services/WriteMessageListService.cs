using ClientTestSignalR_1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_1.Services
{
    public class WriteMessageListService:IWriteMessageService
    {
        public void WriteMessage(object? obj, string message)
        {
            if (obj!=null) 
            {
                ((ObservableCollection<string>)obj).Add($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}/ {message}");
            } 
        }
    }
}
