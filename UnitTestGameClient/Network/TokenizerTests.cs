using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Foundation;

namespace UnitTestGameClient.Network
{
    [TestClass()]
    public class TokenizerTests
    {
        [TestMethod()]
        public void TokenizeSectionsTest()
        {
            string message1 = "L:12,33:132#";
            string[] result1 = Tokenizer.TokenizeSections(message1);

            Assert.AreEqual("L", result1[0], false);
            Assert.AreEqual("12,33", result1[1], false);
            Assert.AreEqual("132", result1[2], false);
            Assert.AreEqual(result1.Length, 3);
        }

        [TestMethod()]
        public void TokernizeParametersTest()
        {
            String section1 = "P0;0,0;0;0;100;0;0";
            string[] result1 = Tokenizer.TokernizeParameters(section1);

            Assert.AreEqual("P0", result1[0] , false);
            Assert.AreEqual("0,0", result1[1], false);
            Assert.AreEqual("0", result1[2], false);
            Assert.AreEqual("0", result1[3], false);
            Assert.AreEqual("100", result1[4],false);
            Assert.AreEqual("0", result1[5], false);
            Assert.AreEqual("0", result1[6], false);
            Assert.AreEqual(7, result1.Length);

            String section2 = "3,1,0;5,7,0;1,4,0;3,6,0;4,8,0;8,4,0;7,6,0";

            string[] result2 = Tokenizer.TokernizeParameters(section2);

            Assert.AreEqual("3,1,0", result2[0], false);
            Assert.AreEqual("5,7,0", result2[1], false);
            Assert.AreEqual("1,4,0", result2[2], false);
            Assert.AreEqual("3,6,0", result2[3],false);
            Assert.AreEqual("4,8,0", result2[4], false);
            Assert.AreEqual("8,4,0", result2[5], false);
            Assert.AreEqual("7,6,0", result2[6], false);
            Assert.AreEqual(7, result2.Length);


        }

        [TestMethod()]
        public void TokernizeCoordinatesTest()
        {
            string cord1 = "1,2";
            Coordinate result1 = Tokenizer.TokernizeCoordinates(cord1);
            Assert.AreEqual(1, result1.X);
            Assert.AreEqual(2, result1.Y);

            string cord2 = "-2,-5";
            Coordinate result2 = Tokenizer.TokernizeCoordinates(cord2);
            Assert.AreEqual(-2, result2.X);
            Assert.AreEqual(-5, result2.Y);


            string cord3 = " 1,4 ";
            Coordinate result3 = Tokenizer.TokernizeCoordinates(cord3);
            Assert.AreEqual(1, result3.X);
            Assert.AreEqual(4, result3.Y);

        }

        [TestMethod()]
        public void TokernizeIntArrayTest()
        {
            string arr1 = "1,2,3";
            int[] result1 = Tokenizer.TokernizeIntArray(arr1);
            Assert.AreEqual(1, result1[0]);
            Assert.AreEqual(2, result1[1]);
            Assert.AreEqual(3, result1[2]);
            Assert.AreEqual(3, result1.Length);

           
            string arr2 = " 1 , 2,3 ";
            int[] result2 = Tokenizer.TokernizeIntArray(arr2);
            Assert.AreEqual(1, result2[0]);
            Assert.AreEqual(2, result2[1]);
            Assert.AreEqual(3, result2[2]);
            Assert.AreEqual(3, result2.Length);

            string arr3 = "-1,-2,3";
            int[] result3 = Tokenizer.TokernizeIntArray(arr3);
            Assert.AreEqual(-1, result3[0]);
            Assert.AreEqual(-2, result3[1]);
            Assert.AreEqual(3, result3[2]);
            Assert.AreEqual(3, result3.Length);

        }
    }
}