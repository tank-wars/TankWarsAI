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
    public partial class frmDemoParser : Form
    {
        public frmDemoParser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Title = "TankGame Parser Outputs";               
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Communicator.Instance.Instalatize(new Communicator.Configuration(7000, txtHost.Text, 6000));
            Communicator.Instance.MessageReceived += Instance_MessageReceived;
            Communicator.Instance.MessageReceiveError += Instance_MessageReceiveError;
            Communicator.Instance.MessageReceiverStopped += Instance_MessageReceiverStopped;

            btnSendRAW.Enabled = true;
            pnlControl.Enabled = true;
            btnConnect.Enabled = false;
            txtHost.Enabled = false;
        }

        private void Instance_MessageReceiverStopped(object sender, EventArgs e)
        {
            MessageBox.Show("Message Receiver has stopped");
        }

        private void Instance_MessageReceiveError(object Sender, Communicator.MessageReceiveErrorEventArgs args)
        {
            MessageBox.Show("Unable to instalize receiver..." + Environment.NewLine + args.Error.ToString(), "Error");
        }

        /*
            Writes to console
        */
        public void Echo(String message)
        {
            if (chkEchoConsole.Checked)
            {
                Console.Write(message);
                Console.Out.Flush();
               // txtReceived.AppendText(message);
            }
        }
        /*
            Echo a raw message received
        */
        public void EchoRaw(String message)
        {
            Echo("Raw Message: " + message + Environment.NewLine);
        }

        /*
            Echo a sent message
        */
        public void EchoSent(String message)
        {
            if(chkEchoSent.Checked)
                Echo("Sent: " + message + Environment.NewLine);
        }
        /*
            Echo an output from parser
        */
        public void EchoParsed(String message)
        {
            if (chkEchoParsed.Checked)
                Echo(message + Environment.NewLine);
        }
        private void Instance_MessageReceived(object Sender, Communicator.MessageReceivedEventArgs args)
        {
            if (chkEchoRaw.Checked)
                EchoRaw(args.Message);

            if (chkEchoGameWorld.Checked)
                Echo(GameWorld.Instance.ToString());
            //txtReceived.AppendText(args.Message + Environment.NewLine);
            MessageParser parser = MessageParser.Instance;
            Messages.ServerMessage message = parser.Parse(args.Message);
            if (message != null)
            {
                EchoParsed(message.ToString());
                message.Execute();
                pnlMapGUI.Invalidate();
            }
            else
            {
                MessageBox.Show("Unidentified Message " + args.Message);
            }
                //txtReceived.AppendText(message.ToString() + Environment.NewLine);
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
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Messages.ClientMessage msg = new Messages.ShootMessage();
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                EchoSent(msg.GenerateStringMessage());
                //txtReceived.AppendText(msg.ToString() + Environment.NewLine);
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

        private void pnlMapGUI_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            e.Graphics.Clear(Color.White);
            DemoGUI gui = new DemoGUI(graphics, new Point(), Math.Min(pnlMapGUI.Width, pnlMapGUI.Height));
            gui.DrawGrid();

            GameWorld world = GameWorld.Instance;
            
            
            if(world.Map != null)
            {
                if (world.Map.Water != null)
                {
                    foreach (Coordinate water in world.Map.Water)
                    {
                        gui.DrawWater(water);
                    }
                }
                if(world.BrickState != null)
                {
                    foreach (Brick brick in world.BrickState)
                    {
                        gui.DrawBrick(brick.Postition, brick.DamageLevel);
                    }
                }
                else if(world.Map.Brick != null)
                {                    
                    foreach (Coordinate brick in world.Map.Brick)
                    {
                        gui.DrawBrick(brick,0);
                    }
                }
                if (world.Map.Stone != null)
                {
                    foreach (Coordinate stone in world.Map.Stone)
                    {
                        gui.DrawStone(stone);
                    }
                }
            }
            if(world.LifePacks != null)
                foreach(LifePack lifePack in world.LifePacks)
                {
                    gui.DrawMedi(lifePack.Position);
                }

            if (world.Coins != null)
                foreach (Coin coin in world.Coins)
                {
                    gui.DrawCoin(coin.Position);
                }

            if (world.Players != null)
                foreach (PlayerDetails player in world.Players)
                {
                    int index = Convert.ToInt32(player.Name.Substring(1));
                    gui.DrawTank(index, player.Position, player.Direction, player.IsShooting);
                }

            /*gui.DrawTank(0, new Coordinate(2, 2), Direction.North);
            gui.DrawTank(1, new Coordinate(5, 2), Direction.East);
            gui.DrawTank(2, new Coordinate(2, 5), Direction.South);
            gui.DrawTank(3, new Coordinate(3, 3), Direction.West);*/
        }

        private void pnlMapGUI_Resize(object sender, EventArgs e)
        {
            pnlMapGUI.Invalidate();
        }

        private void chkEchoGameWorld_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEchoConsole_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEchoParsed_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
