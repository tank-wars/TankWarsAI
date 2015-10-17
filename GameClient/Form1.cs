using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Communicator.Instance.Instalatize(new Communicator.Configuration(7000, "localhost", 6000));
            Communicator.Instance.MessageReceived += Instance_MessageReceived;
            Communicator.Instance.MessageReceiveError += Instance_MessageReceiveError;
            Communicator.Instance.MessageReceiverStopped += Instance_MessageReceiverStopped;

            
        }

        private void Instance_MessageReceiverStopped(object sender, EventArgs e)
        {
            MessageBox.Show("Message Receiver has stopped");
        }

        private void Instance_MessageReceiveError(object Sender, Communicator.MessageReceiveErrorEventArgs args)
        {
            MessageBox.Show("Unable to instalize receiver..." + Environment.NewLine + args.Error.ToString(), "Error");

        }

        private void Instance_MessageReceived(object Sender, Communicator.MessageReceivedEventArgs args)
        {
            txtReceived.AppendText(args.Message + Environment.NewLine);
            MessageParser parser = MessageParser.Instance;
            Messages.ServerMessage message = parser.Parse(args.Message);
            if(message != null)
                txtReceived.AppendText(message.ToString() + Environment.NewLine);
            Debug.WriteLine(args.Message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Communicator.Instance.SendMessage(txtSend.Text);
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            try {
                Messages.ClientMessage msg = new Messages.JoinRequestMessage();
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                txtReceived.AppendText(msg.ToString() + Environment.NewLine);
             }
            catch(System.IO.IOException)
            {
                MessageBox.Show("Unable to Send Message");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Unable to Send Message");
            }
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            try {
                Messages.ClientMessage msg = new Messages.PlayerMovementMessage(Direction.North);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                txtReceived.AppendText(msg.ToString() + Environment.NewLine);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Unable to Send Message");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Unable to Send Message");
            }
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            try { 
                Messages.ClientMessage msg = new Messages.PlayerMovementMessage(Direction.South);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                txtReceived.AppendText(msg.ToString() + Environment.NewLine);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Unable to Send Message");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Unable to Send Message");
            }
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            try
            {
                Messages.ClientMessage msg = new Messages.PlayerMovementMessage(Direction.West);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                txtReceived.AppendText(msg.ToString() + Environment.NewLine);
            }
            catch(System.IO.IOException)
            {
                MessageBox.Show("Unable to Send Message");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Unable to Send Message");
            }

        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            try { 
                Messages.ClientMessage msg = new Messages.PlayerMovementMessage(Direction.East);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                txtReceived.AppendText(msg.ToString() + Environment.NewLine);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Unable to Send Message");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Unable to Send Message");
            }
        }
    }
}
