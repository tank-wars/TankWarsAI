using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Foundation;

namespace GameClient.Network.Tests
{
    [TestClass()]
    public class TokenizerTests
    {
        [TestMethod()]
        public void TokenizeSectionsTest()
        {
            string message1 = "L:12,33:132#";
            string[] result1 = Tokenizer.TokenizeSections(message1);

            Assert.AreEqual(result1[0], "L", false);
            Assert.AreEqual(result1[1], "12,33", false);
            Assert.AreEqual(result1[2], "132", false);
            Assert.AreEqual(result1.Length, 3);
        }

        [TestMethod()]
        public void TokernizeParametersTest()
        {
            String section1 = "P0;0,0;0;0;100;0;0";
            string[] result1 = Tokenizer.TokernizeParameters(section1);

            Assert.AreEqual(result1[0], "P0", false);
            Assert.AreEqual(result1[1], "0,0", false);
            Assert.AreEqual(result1[2], "0", false);
            Assert.AreEqual(result1[3], "0", false);
            Assert.AreEqual(result1[4], "100", false);
            Assert.AreEqual(result1[5], "0", false);
            Assert.AreEqual(result1[6], "0", false);
            Assert.AreEqual(result1.Length, 7);

            String section2 = "3,1,0;5,7,0;1,4,0;3,6,0;4,8,0;8,4,0;7,6,0";

            string[] result2 = Tokenizer.TokernizeParameters(section2);

            Assert.AreEqual(result2[0], "3,1,0", false);
            Assert.AreEqual(result2[1], "5,7,0", false);
            Assert.AreEqual(result2[2], "1,4,0", false);
            Assert.AreEqual(result2[3], "3,6,0", false);
            Assert.AreEqual(result2[4], "4,8,0", false);
            Assert.AreEqual(result2[5], "8,4,0", false);
            Assert.AreEqual(result2[6], "7,6,0", false);
            Assert.AreEqual(result2.Length, 7);


        }

        [TestMethod()]
        public void TokernizeCoordinatesTest()
        {
            string cord1 = "1,2";
            Coordinate result1 = Tokenizer.TokernizeCoordinates(cord1);
            Assert.AreEqual(result1.X, 1);
            Assert.AreEqual(result1.Y, 2);

            string cord2 = "-2,-5";
            Coordinate result2 = Tokenizer.TokernizeCoordinates(cord2);
            Assert.AreEqual(result2.X, -2);
            Assert.AreEqual(result2.Y, -5);


            string cord3 = " 1,4 ";
            Coordinate result3 = Tokenizer.TokernizeCoordinates(cord3);
            Assert.AreEqual(result3.X, 1);
            Assert.AreEqual(result3.Y, 4);

        }

        [TestMethod()]
        public void TokernizeIntArrayTest()
        {
            string arr1 = "1,2,3";
            int[] result1 = Tokenizer.TokernizeIntArray(arr1);
            Assert.AreEqual(result1[0], 1);
            Assert.AreEqual(result1[1], 2);
            Assert.AreEqual(result1[2], 3);
            Assert.AreEqual(result1.Length, 3);

           
            string arr2 = " 1 , 2,3 ";
            int[] result2 = Tokenizer.TokernizeIntArray(arr2);
            Assert.AreEqual(result2[0], 1);
            Assert.AreEqual(result2[1], 2);
            Assert.AreEqual(result2[2], 3);
            Assert.AreEqual(result2.Length, 3);

            string arr3 = "-1,-2,3";
            int[] result3 = Tokenizer.TokernizeIntArray(arr3);
            Assert.AreEqual(result3[0], -1);
            Assert.AreEqual(result3[1], -2);
            Assert.AreEqual(result3[2], 3);
            Assert.AreEqual(result3.Length, 3);

        }
    }
}