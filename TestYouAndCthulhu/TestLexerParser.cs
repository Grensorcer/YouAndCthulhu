using System;
using NUnit.Framework;
using YouAndCthulhu;

namespace Tests
{
    public class TestLexerParser
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void TestLexingId1()
        {
            string id = "0A";
            int index = 0;
            Assert.AreEqual(0, LexerParser.GetIdNatural(id, ref index));
            Assert.AreEqual('A', LexerParser.GetIdLetter(id, ref index));
        }

        [Test]
        public void TestLexingId2()
        {
            string id = "9D";
            int index = 0;
            Assert.AreEqual(9, LexerParser.GetIdNatural(id, ref index));
            Assert.AreEqual('D', LexerParser.GetIdLetter(id, ref index));
        }
        
        [Test]
        public void TestLexingId3()
        {
            string id = "00000A";
            int index = 0;
            Assert.AreEqual(0, LexerParser.GetIdNatural(id, ref index));
            Assert.AreEqual('A', LexerParser.GetIdLetter(id, ref index));
        }
        
        [Test]
        public void TestLexingId4()
        {
            string id = "110A";
            int index = 0;
            Assert.AreEqual(110, LexerParser.GetIdNatural(id, ref index));
            Assert.AreEqual('A', LexerParser.GetIdLetter(id, ref index));
        }
        
        [Test]
        public void TestLexingId5()
        {
            string id = "5092C[167B";
            int index = 0;
            Assert.AreEqual(5092, LexerParser.GetIdNatural(id, ref index));
            Assert.AreEqual('C', LexerParser.GetIdLetter(id, ref index));
        }
        
        [Test]
        public void TestLexingId6()
        {
            string id = "5092g[167B";
            int index = 0;
            Assert.AreEqual(5092, LexerParser.GetIdNatural(id, ref index));
            try
            {
                LexerParser.GetIdLetter(id, ref index);
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

    }
}