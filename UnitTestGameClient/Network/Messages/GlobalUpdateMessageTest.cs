using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient.Network.Messages;
using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network;

namespace UnitTestGameClient.Network.Messages
{
    [TestClass]
    public class GlobalUpdateMessageTest
    {
        [TestMethod]
        public void TryParseTest()
        {
            string input = "G:P0;1,2;0;0;100;1000;500:P1;5,6;2;1;80;500;250:1,4,0;2,5,1;4,2,3#";
            GlobalUpdateMessage.GlobalUpdate expectedOutput = new GlobalUpdateMessage.GlobalUpdate();
            PlayerDetails[] playerDetails = new PlayerDetails[2];
            Brick[] brickUpdate = new Brick[3];

            playerDetails[0] = new PlayerDetails();
            playerDetails[0].Name = "P0";
            playerDetails[0].Position = new Coordinate(1, 2);
            playerDetails[0].Direction = (Direction)0;
            playerDetails[0].IsShooting = false;
            playerDetails[0].Health = 100;
            playerDetails[0].Coins = 1000;
            playerDetails[0].Points = 500;

            playerDetails[1] = new PlayerDetails();
            playerDetails[1].Name = "P1";
            playerDetails[1].Position = new Coordinate(5, 6);
            playerDetails[1].Direction = (Direction)2;
            playerDetails[1].IsShooting = true;
            playerDetails[1].Health = 80;
            playerDetails[1].Coins = 500;
            playerDetails[1].Points = 250;

            brickUpdate[0] = new Brick();
            brickUpdate[0].Postition = new Coordinate(1, 4);
            brickUpdate[0].DamageLevel = 0;

            brickUpdate[1] = new Brick();
            brickUpdate[1].Postition = new Coordinate(2,5);
            brickUpdate[1].DamageLevel = 1;

            brickUpdate[2] = new Brick();
            brickUpdate[2].Postition = new Coordinate(4,2);
            brickUpdate[2].DamageLevel = 3;

            expectedOutput.PlayerUpdates = playerDetails;
            expectedOutput.brickUpdate = brickUpdate;

            MessageParser messageParser = MessageParser.Instance;
            GlobalUpdateMessage serverMessage = (GlobalUpdateMessage)messageParser.Parse(input);
            GlobalUpdateMessage.GlobalUpdate output = serverMessage.globalUpdate;
            
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].Name, output.PlayerUpdates[0].Name);
            Assert.AreEqual<Coordinate>(expectedOutput.PlayerUpdates[0].Position, output.PlayerUpdates[0].Position);
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].Direction, output.PlayerUpdates[0].Direction);
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].IsShooting, output.PlayerUpdates[0].IsShooting);
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].Health, output.PlayerUpdates[0].Health);
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].Coins, output.PlayerUpdates[0].Coins);
            Assert.AreEqual(expectedOutput.PlayerUpdates[0].Points, output.PlayerUpdates[0].Points);

            Assert.AreEqual(expectedOutput.PlayerUpdates[1].Name, output.PlayerUpdates[1].Name);
            Assert.AreEqual<Coordinate>(expectedOutput.PlayerUpdates[1].Position, output.PlayerUpdates[1].Position);
            Assert.AreEqual(expectedOutput.PlayerUpdates[1].Direction, output.PlayerUpdates[1].Direction);
            Assert.AreEqual(expectedOutput.PlayerUpdates[1].IsShooting, output.PlayerUpdates[1].IsShooting);
            Assert.AreEqual(expectedOutput.PlayerUpdates[1].Health, output.PlayerUpdates[1].Health);
            Assert.AreEqual(expectedOutput.PlayerUpdates[1].Coins, output.PlayerUpdates[1].Coins);
            Assert.AreEqual(expectedOutput.PlayerUpdates[1].Points, output.PlayerUpdates[1].Points);

            Assert.AreEqual(expectedOutput.brickUpdate[0].Postition, output.brickUpdate[0].Postition);
            Assert.AreEqual(expectedOutput.brickUpdate[0].DamageLevel, output.brickUpdate[0].DamageLevel);

            Assert.AreEqual(expectedOutput.brickUpdate[1].Postition, output.brickUpdate[1].Postition);
            Assert.AreEqual(expectedOutput.brickUpdate[1].DamageLevel, output.brickUpdate[1].DamageLevel);

            Assert.AreEqual<Coordinate>(expectedOutput.brickUpdate[2].Postition, output.brickUpdate[2].Postition);
            Assert.AreEqual(expectedOutput.brickUpdate[2].DamageLevel, output.brickUpdate[2].DamageLevel);
        }
    }
}
