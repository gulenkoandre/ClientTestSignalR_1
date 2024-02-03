using ClientTestSignalR_1.Services.Interfaces;
using ClientTestSignalR_1.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace ClientTestSignalR_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {            
            InitializeComponent();

            if (Program.host != null)
            {
                DataContext = Program.host.Services.GetService<VM>();
            }            
        }        
    }
}