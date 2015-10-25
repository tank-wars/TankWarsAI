using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient;
using GameClient.GameDomain;
using GameClient.Network;
using GameClient.Foundation;
using GameClient.Network.Messages;

namespace UnitTestGameClient.Network.Messages
{
    [TestClass]
    public class GameInitiationMessageTest
    {
        [TestMethod]
        public void TryParseTest()
        {
            string input = "I:P0: 2, 3; 1, 2: 0, 6; 4, 5: 2, 4; 1, 6#";
            MapDetails expectedOutput = new MapDetails();
            expectedOutput.Brick = new Coordinate[] { new Coordinate(2, 3), new Coordinate(1, 2)};
            expectedOutput.Stone = new Coordinate[] { new Coordinate(0, 6), new Coordinate(4, 5) };
            expectedOutput.Water = new Coordinate[] { new Coordinate(2, 4), new Coordinate(1, 6) };

            MessageParser messageParser = MessageParser.Instance;
            GameInitiationMessage serverMessage = (GameInitiationMessage)messageParser.Parse(input);
            MapDetails output = serverMessage.mapDetails;
            
            CollectionAssert.AreEqual(expectedOutput.Brick, output.Brick);
            CollectionAssert.AreEqual(expectedOutput.Stone, output.Stone);
            CollectionAssert.AreEqual(expectedOutput.Water, output.Water);

        }
    }
}
