using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SignalRSecondClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IHubProxy HubProxy { get; set; }
#if DEBUG
        const string ServerURI = "http://localhost:59524";
#else
        const string ServerURI = "http://commutecarsharing.ru";
#endif

        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ConnectAsync();
        }

        private async void ConnectAsync()
        {
            Connection = new HubConnection(ServerURI);
            //Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("ChatHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread 
            HubProxy.On("AddedMessage", () =>

            this.Dispatcher.Invoke(() => textBlock.Text = "ThereMessageFrom12"));
            try
            {
                await Connection.Start();
                await HubProxy.Invoke("Register", 12);
            }
            catch
            {

            }

        }

        private void DoMessage(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("SendMessageToUser", 22);
        }
    }
}
