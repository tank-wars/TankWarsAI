using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network;
using GameClient.Network.Messages;

namespace UnitTestGameClient.Network.Messages
{
    [TestClass]
    public class LifePackAvailableMessageTest
    {
        [TestMethod]
        public void TryParseTest()
        {
            string input = "L:4,6:1250#";
            LifePack expectedOutput = new LifePack();
            expectedOutput.Position = new Coordinate(4,6);
            expectedOutput.TimeLimit = 1250;

            MessageParser messageParser = MessageParser.Instance;
            LifePackAvailableMessage serverMessage = (LifePackAvailableMessage)messageParser.Parse(input);
            LifePack output = serverMessage.lifePack;

            Assert.AreEqual<Coordinate>(expectedOutput.Position, output.Position);
            Assert.AreEqual(expectedOutput.TimeLimit, output.TimeLimit);
        }
    }
}
