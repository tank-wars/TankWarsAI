using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network;
using GameClient.Network.Messages;

namespace UnitTestGameClient.Network.Messages
{
    [TestClass]
    public class JoinAcceptanceMessageTest
    {
        [TestMethod]
        public void TryParse()
        {
            string input = "S:P0;1,3;0:P1;4,6;3#";
            PlayerDetails[] expectedOutput = new PlayerDetails[2];

            expectedOutput[0] = new PlayerDetails();
            expectedOutput[0].Name = "P0";
            expectedOutput[0].Position = new Coordinate(1, 3);
            expectedOutput[0].Direction = (Direction)0;

            expectedOutput[1] = new PlayerDetails();
            expectedOutput[1].Name = "P1";
            expectedOutput[1].Position = new Coordinate(4, 6);
            expectedOutput[1].Direction = (Direction)3;

            MessageParser messageParser = MessageParser.Instance;
            JoinAcceptanceMessage serverMessage = (JoinAcceptanceMessage)messageParser.Parse(input);
            PlayerDetails[] output = serverMessage.PlayerDetails;

            Assert.AreEqual(expectedOutput[0].Name, output[0].Name);
            Assert.AreEqual(expectedOutput[0].Position, output[0].Position);
            Assert.AreEqual(expectedOutput[0].Direction, output[0].Direction);

            Assert.AreEqual(expectedOutput[1].Name, output[1].Name);
            Assert.AreEqual(expectedOutput[1].Position, output[1].Position);
            Assert.AreEqual(expectedOutput[1].Direction, output[1].Direction);
        }
    }
}
