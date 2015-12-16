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
using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network;
using GameClient.Network.Communicator;
using GameClient.Network.Messages;
using GameClient.GUI;

namespace GameClient
{
    /*
        DEMO
        Demonstrates the Messages Received from Server and allows to send messages to server
    */
    public partial class frmDemoParser : Form
    {
      

        public frmDemoParser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Title = "TankGame Parser Outputs";
            GameWorld.Instance.GameFinished += GameWorld_GameFinished;
            GameWorld.Instance.NegativeHonour += GameWorld_NegativeHonour;
            GameWorld.Instance.GameStarted += GameWorld_GameStarted;
        }

        private void GameWorld_GameStarted(object sender, EventArgs e)
        {
            EnableControl();
        }

        private void DisableControl()
        {
            useKeyboard = false;
            pnlControl.Enabled = false;
            btnKeyboard.Enabled = false;
            
        }

        private void EnableControl()
        {
            if(!useKeyboard)
            {
                pnlControl.Enabled = true;
                btnKeyboard.Enabled = true;
            }
           

        }
        private void GameWorld_NegativeHonour(object Sender, NegativeHonourMessage.NegativeHonourReason reason)
        {
            switch(reason)
            {
                case NegativeHonourMessage.NegativeHonourReason.CellOccupied:
                    Console.Beep();
                    break;
                case NegativeHonourMessage.NegativeHonourReason.Dead:
                    MessageBox.Show("You are Dead");
                    DisableControl();
                    break;

                case NegativeHonourMessage.NegativeHonourReason.GameHasFinished:
                    MessageBox.Show("Game has finished");
                    DisableControl();
                    break;
                case NegativeHonourMessage.NegativeHonourReason.InvalidCell:
                    Console.Beep();
                    break;
                case NegativeHonourMessage.NegativeHonourReason.InvalidContestant:
                    MessageBox.Show("Join before playing the game");
                    break;
                case NegativeHonourMessage.NegativeHonourReason.Obstacle:
                    Console.Beep();
                    break;
                case NegativeHonourMessage.NegativeHonourReason.PitFall:
                    MessageBox.Show("You have fallen into water");
                    DisableControl();
                    break;
                case NegativeHonourMessage.NegativeHonourReason.TooQuick:
                    Console.Beep();
                    break;
            }
        }

        private void GameWorld_GameFinished(object sender, EventArgs e)
        {
            MessageBox.Show("Game Over");
            this.Close();
        }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Communicator.Instance.Instalatize(new Communicator.Configuration(7000, txtHost.Text, 6000));
            Communicator.Instance.MessageReceived += Instance_MessageReceived;
            Communicator.Instance.MessageReceiveError += Instance_MessageReceiveError;
            Communicator.Instance.MessageReceiverStopped += Instance_MessageReceiverStopped;

            btnSendRAW.Enabled = true;
            btnJoin.Enabled = true;
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

        //Method triggered when a message is received from server
        private void Instance_MessageReceived(object Sender, Communicator.MessageReceivedEventArgs args)
        {
            if (chkEchoRaw.Checked)
                EchoRaw(args.Message);

            if (chkEchoGameWorld.Checked)
                Echo(GameWorld.Instance.ToString());
            
            MessageParser parser = MessageParser.Instance;
            ServerMessage message = parser.Parse(args.Message);
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
                
            Debug.WriteLine(args.Message);
        }

        //Send a raw message to Server
        private void btnSendRaw_Click(object sender, EventArgs e)
        {
            try
            {
                Communicator.Instance.SendMessage(txtSend.Text);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Unable to Send Message");
                Debug.WriteLine(ex);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Unable to Send Message");
            }
        }

        //Join to Server
        private void btnJoin_Click(object sender, EventArgs e)
        {
           
            try {
                ClientMessage msg = new JoinRequestMessage();
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                EchoSent(msg.GenerateStringMessage());
                btnJoin.Enabled = false;
            }
            catch(System.IO.IOException ex)
            {
                MessageBox.Show("Unable to Send Message");
                Debug.WriteLine(ex);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Unable to Send Message");
            }
        }

        //Look, Move North
        private void btnNorth_Click(object sender, EventArgs e)
        {
            try {
                if (!GameWorld.Instance.InputAllowed)
                    return;

                GameWorld.Instance.InputAllowed = false;
                ClientMessage msg = new PlayerMovementMessage(Direction.North);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                EchoSent(msg.GenerateStringMessage());
                
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

        //Look, Move South
        private void btnSouth_Click(object sender, EventArgs e)
        {
            try {
                if (!GameWorld.Instance.InputAllowed)
                    return;
                GameWorld.Instance.InputAllowed = false;

                ClientMessage msg = new PlayerMovementMessage(Direction.South);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                EchoSent(msg.GenerateStringMessage());
                
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

        //Look, Move West
        private void btnWest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GameWorld.Instance.InputAllowed)
                    return;
                GameWorld.Instance.InputAllowed = false;

                ClientMessage msg = new PlayerMovementMessage(Direction.West);
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

        //Look, Move East
        private void btnEast_Click(object sender, EventArgs e)
        {
            try {
                if (!GameWorld.Instance.InputAllowed)
                    return;
                GameWorld.Instance.InputAllowed = false;

                ClientMessage msg = new PlayerMovementMessage(Direction.East);
                Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                EchoSent(msg.GenerateStringMessage());
                
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

        //Shoot
        private void btnShoot_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GameWorld.Instance.InputAllowed)
                    return;
                GameWorld.Instance.InputAllowed = false;

                ClientMessage msg = new ShootMessage();
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

        //Display the Map in UI
        private void pnlMapGUI_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            e.Graphics.Clear(Color.White);
            
            //DemoGUI class is used to assist drawing
            DemoGUI gui = new DemoGUI(graphics, new Point(), Math.Min(pnlMapGUI.Width, pnlMapGUI.Height));
            gui.DrawGrid(); //Draw the grid of cells

            GameWorld world = GameWorld.Instance;           
            
            if(world.Map != null) //Has the map being loaded?
            {
                //Has the map properly instalized?
                if (world.Map.Water != null)
                {
                    foreach (Coordinate water in world.Map.Water)
                    {
                        gui.DrawWater(water);
                    }
                }
                //Do we have updated brick states?
                if (world.BrickState != null)
                {
                    foreach (Brick brick in world.BrickState)
                    {
                        gui.DrawBrick(brick.Postition, brick.DamageLevel);
                    }
                }
                //Else, do we have brick locations sent with map?
                else if (world.Map.Brick != null)
                {                    
                    foreach (Coordinate brick in world.Map.Brick)
                    {
                        gui.DrawBrick(brick,0);
                    }
                }
                //Has the map properly instalized?
                if (world.Map.Stone != null)
                {
                    foreach (Coordinate stone in world.Map.Stone)
                    {
                        gui.DrawStone(stone);
                    }
                }
            }

            //Has the map properly instalized?
            if (world.LifePacks != null)
                foreach(LifePack lifePack in world.LifePacks)
                {
                    if(lifePack.IsAlive) //only draw lifepacks that are alive
                        gui.DrawMedi(lifePack.Position);
                }

            //Has the map properly instalized?
            if (world.Coins != null)
                foreach (Coin coin in world.Coins)
                {
                    if(coin.IsAlive) //only draw coins that are alive
                        gui.DrawCoin(coin.Position);
                }
            //Has the map properly instalized?

            if (world.Players != null)
                foreach (PlayerDetails player in world.Players)
                {
                    int index = Convert.ToInt32(player.Name.Substring(1));
                    gui.DrawTank(index, player.Position, player.Direction, player.IsShooting);
                }

        
        }

        
        
        private void pnlMapGUI_Resize(object sender, EventArgs e)
        {
            //Redraw GUI
            pnlMapGUI.Invalidate();
        }

        bool useKeyboard = false;

        private void frmDemoParser_KeyDown(object sender, KeyEventArgs e)
        {



            if (!useKeyboard)
                return;

            switch(e.KeyCode)
            {
                case Keys.Up:
                    if (!GameWorld.Instance.InputAllowed)
                        break;

                    GameWorld.Instance.InputAllowed = false;

                    try
                    {
                        ClientMessage msg = new PlayerMovementMessage(Direction.North);
                        Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                        EchoSent(msg.GenerateStringMessage());

                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    break;
                case Keys.Down:
                    if (!GameWorld.Instance.InputAllowed)
                        break;

                    GameWorld.Instance.InputAllowed = false;
                    try
                    {
                        ClientMessage msg = new PlayerMovementMessage(Direction.South);
                        Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                        EchoSent(msg.GenerateStringMessage());

                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    break;
                case Keys.Left:
                    if (!GameWorld.Instance.InputAllowed)
                        break;

                    GameWorld.Instance.InputAllowed = false;
                    try
                    {
                        ClientMessage msg = new PlayerMovementMessage(Direction.West);
                        Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                        EchoSent(msg.GenerateStringMessage());

                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    break;
                case Keys.Right:
                    if (!GameWorld.Instance.InputAllowed)
                        break;

                    GameWorld.Instance.InputAllowed = false;
                    try
                    {
                        ClientMessage msg = new PlayerMovementMessage(Direction.East);
                        Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                        EchoSent(msg.GenerateStringMessage());

                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    break;
                case Keys.Space:
                    if (!GameWorld.Instance.InputAllowed)
                        break;

                    GameWorld.Instance.InputAllowed = false;
                    try
                    {
                        ClientMessage msg = new ShootMessage();
                        Communicator.Instance.SendMessage(msg.GenerateStringMessage());
                        EchoSent(msg.GenerateStringMessage());

                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Unable to Send Message");
                    }
                    break;
                case Keys.Escape:
                    pnlControl.Enabled = true;
                    
                    txtSend.Enabled = true;
                    btnSendRAW.Enabled = true;
                    btnKeyboard.Enabled = true;
                    chkEchoConsole.Enabled = true;
                    chkEchoGameWorld.Enabled = true;
                    chkEchoParsed.Enabled = true;
                    chkEchoRaw.Enabled = true;
                    chkEchoSent.Enabled = true;
                 
                    useKeyboard = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use Arrowkeys to Move and Space to Shoot. Press Escape to return to Manual Mode");
            pnlControl.Enabled = false;
            btnJoin.Enabled = false;
            btnConnect.Enabled = false;
            txtHost.Enabled = false;
            txtSend.Enabled = false;
            btnSendRAW.Enabled = false;
            btnKeyboard.Enabled = false;
            chkEchoConsole.Enabled = false;
            chkEchoGameWorld.Enabled = false;
            chkEchoParsed.Enabled = false;
            chkEchoRaw.Enabled = false;
            chkEchoSent.Enabled = false;
            useKeyboard = true;
        }

    }
}
