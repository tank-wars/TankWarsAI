using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameClient.GameDomain;
using GameClient.Foundation;
using GameClient.Network;
using GameClient.Network.Messages;

namespace UnitTestGameClient.Network.Messages
{
    [TestClass]
    public class CoinsAvailableMessageTest
    {
        [TestMethod]
        public void TryParseTest()
        {
            string input = "C:1,4:2000:7500#";
            Coin expectedOutput = new Coin();
            expectedOutput.Position = new Coordinate(1, 4);
            expectedOutput.TimeLimit = 2000;
            expectedOutput.Value = 7500;

            MessageParser messageParser = MessageParser.Instance;
            CoinsAvailableMessage serverMessage = (CoinsAvailableMessage)messageParser.Parse(input);
            Coin output = serverMessage.coin;

            Assert.AreEqual<Coordinate>(expectedOutput.Position, output.Position);
            Assert.AreEqual(expectedOutput.TimeLimit, output.TimeLimit);
            Assert.AreEqual(expectedOutput.Value, output.Value);
        }
    }
}
