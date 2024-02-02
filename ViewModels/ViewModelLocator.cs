// Ignore Spelling: Locator

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTestSignalR_1.ViewModels
{
    /// <summary>
    /// этот класс представляет собой набор свойств, через которые будет осуществляться доступ к конкретным View-моделям
    /// </summary>
    internal class ViewModelLocator
    {
        public VM VMService => App.Host.Services.GetRequiredService<VM>(); //получаем из наших сервисов класс VM
    }
}
