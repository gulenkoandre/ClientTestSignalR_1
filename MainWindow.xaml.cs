
using ClientTestSignalR_1.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

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