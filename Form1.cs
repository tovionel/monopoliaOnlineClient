using Microsoft.AspNet.SignalR.Client;
using System;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        IHubProxy _hub;
        HubConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private Boolean CreateConnection()
        {
#if DEBUG
            string url = @"http://localhost:64913/";
#else
            string url = @"http://monopoliaservice.somee.com/";
#endif
            try
            {
                connection = new HubConnection(url);
                _hub = connection.CreateHubProxy("MonopoliaHub");
                connection.StateChanged += Connection_StateChanged;

                AddingHubMetods();

                connection.Start().Wait();
            }
            catch
            {
                MessageBox.Show("can't create connection!");
                return false;
            }
            return true;
        }

        private void AddingHubMetods()
        {
            //_hub.
            _hub.On("Hello", new Action<string>(Hello));
        }        

        private void Hello(string m)
        {
            MessageBox.Show(string.Format("Hello World: {0}", m));
        }

        private void Connection_StateChanged(StateChange obj)
        {
         //   MessageBox.Show(obj.NewState.ToString());            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _hub.Invoke("Hello").Wait();
        }
    }
}
