using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KyntoLib.Encryption;

namespace KyntoServerTester.DebugClient
{
    public partial class DebugClientForm : Form
    {
        private TcpConnection _Connection;

        public DebugClientForm()
        {
            InitializeComponent();

            this._Connection = new TcpConnection(this.InBoundListBox, this.OutBoundListBox);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            this._Connection.Connect();
            this.ConnectButton.Enabled = false;
            this.DisconnectButton.Enabled = true;
            this.SendButton.Enabled = true;
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            this._Connection.Disconnect();
            this.ConnectButton.Enabled = true;
            this.DisconnectButton.Enabled = false;
            this.SendButton.Enabled = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {

        }
    }

    public class TcpConnection
    {
        private ListBox _InBound;

        private ListBox _OutBound;

        private Socket _Connection;

        private byte[] _Buffer = new byte[16 * 1024];

        public TcpConnection(ListBox InBound, ListBox OutBound)
        {
            this._InBound = InBound;
            this._OutBound = OutBound;
        }

        public void Connect()
        {
            this._Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this._InBound.Invoke(new MethodInvoker(delegate
            {
                this._InBound.Items.Clear();
                this._OutBound.Items.Clear();
            }));

            this._Connection.BeginConnect(new IPEndPoint(IPAddress.Parse("127.0.0.8"), 3500), new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult Result)
        {
            this._Connection.EndConnect(Result);
            this._Connection.BeginReceive(this._Buffer, 0, this._Buffer.Length, SocketFlags.None, new AsyncCallback(OnData), null);
        }

        public void Disconnect()
        {
            this._Connection.Disconnect(false);
            this._Connection = null;
        }

        public void OnData(IAsyncResult Result)
        {
            try
            {
                int Bytes = this._Connection.EndReceive(Result);

                // Get the string.
                string Buffer = Encoding.ASCII.GetString(this._Buffer, 0, Bytes - 1);
                Buffer = RC4Encryption.Decrypt(Buffer, "10078904dd982fbe5682651f1e313b56");

                this._InBound.Invoke(new MethodInvoker(delegate
                {
                    this._InBound.Items.Add(Buffer);
                }));

                this._Connection.BeginReceive(this._Buffer, 0, this._Buffer.Length, SocketFlags.None, new AsyncCallback(OnData), null);
            }
            catch
            {
                this._InBound.Invoke(new MethodInvoker(delegate
                {
                    this._InBound.Items.Add(">>>>>>>>>> ERROR <<<<<<<<<<");
                }));
            }
        }

        public void Send(string Data)
        {
            this._Connection.Send(Encoding.ASCII.GetBytes(Data));
        }
    }
}
