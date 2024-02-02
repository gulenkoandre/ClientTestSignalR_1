using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_1.Services.Interfaces
{
    public interface IWriteMessageService
    {
        public void WriteMessage(object? obj, string message);   
    }
}
