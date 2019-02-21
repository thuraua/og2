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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MqttClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new MqttClient("iot.eclipse.org");
            client.MqttMsgPublishReceived += MqttMsgReceived;
            client.Connect("12345678921122018");
            client.Subscribe(new string[] { "htlvillach/4BHIF/Chat" }, new byte[] { 2 });
        }

        private void MqttMsgReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            Dispatcher.Invoke(delegate { txtVerlauf.Text += ReceivedMessage+"\n"; });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string meldung = txtNickname.Text + ": " + txtMeldung.Text;
            client.Publish("htlvillach/4BHIF/Chat", Encoding.UTF8.GetBytes(meldung));
        }
    }
}
