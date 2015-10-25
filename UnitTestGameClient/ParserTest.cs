using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient;
using GameClient.GameDomain;
using GameClient.Network;
using GameClient.Foundation;
using GameClient.Network.Messages;

namespace UnitTestGameClient
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GameInitiationMessageTest()
        {
            string input = "I:P0: 2, 3; 1, 2: 0, 6; 4, 5: 2, 4; 1, 6#";
            MapDetails expectedOutput = new MapDetails();
            expectedOutput.Brick = new Coordinate[] { new Coordinate(2, 3), new Coordinate(1, 2)};
            expectedOutput.Stone = new Coordinate[] { new Coordinate(0, 6), new Coordinate(4, 5) };
            expectedOutput.Water = new Coordinate[] { new Coordinate(2, 4), new Coordinate(1, 6) };

            MessageParser messageParser = MessageParser.Instance;
            GameInitiationMessage serverMessage = (GameInitiationMessage)messageParser.Parse(input);
            MapDetails output = serverMessage.mapDetails;
            
            Assert.AreEqual(expectedOutput.Brick[0], output.Brick[0]);
            Assert.AreEqual(expectedOutput.Brick[1], output.Brick[1]);
            Assert.AreEqual(expectedOutput.Stone[0], output.Stone[0]);
            Assert.AreEqual(expectedOutput.Stone[1], output.Stone[1]);
            Assert.AreEqual(expectedOutput.Water[0], output.Water[0]);
            Assert.AreEqual(expectedOutput.Water[1], output.Water[1]);
        }
    }
}
