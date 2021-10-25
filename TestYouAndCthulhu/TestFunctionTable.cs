using System;
using System.Collections.Generic;
using NUnit.Framework;
using YouAndCthulhu;
using Program = Microsoft.VisualStudio.TestPlatform.TestHost.Program;

namespace Tests
{
    public class TestFunctionTable
    {
        [Test]
        public void TestAdd()
        {
            FunctionTable ftable = new FunctionTable();
        }
        
        [Test]
        public void TestSearchEasy()
        {
            FunctionTable ftable = new FunctionTable();
            Function first = new Function(0, 'A', "", ftable);
            ftable.Add(first);
            for (int i = 0; i < 100; i++)
            {
                var rand = new Random();
                Function f = new Function((ulong) rand.Next(), 
                    (char) rand.Next(65, 69), "", ftable);
                ftable.Add(f);
            }
            
            Assert.AreEqual(first, ftable.Search(0, 'A'));
        }
        
        [Test]
        public void TestSearchMedium()
        {
            FunctionTable ftable = new FunctionTable();
            Function first = new Function(50, 'B', "", ftable);
            ftable.Add(first);
            for (int i = 0; i < 1000; i++)
            {
                var rand = new Random();
                Function f = new Function((ulong) rand.Next(), 
                    (char) rand.Next(65, 69), "", ftable);
                ftable.Add(f);
            }
            
            Assert.AreEqual(first, ftable.Search(50, 'B'));
        }
        
        [Test]
        public void TestSearchHard()
        {
            FunctionTable ftable = new FunctionTable();
            Function first = new Function(420, 'D', "", ftable);
            ftable.Add(first);
            for (int i = 0; i < 10000; i++)
            {
                var rand = new Random();
                Function f = new Function((ulong) rand.Next(), 
                    (char) rand.Next(65, 69), "", ftable);
                ftable.Add(f);
            }
            
            Assert.AreEqual(first, ftable.Search(420, 'D'));
        }

        [Test]
        public void TestSearchOut()
        {
            try
            {
                YouAndCthulhu.Program.CthulhuSlaver(
                    "../../../../TestYouAndCthulhu/Test5");
            }
            catch
            {
                Assert.Fail();
            }
            
            Assert.Pass();
        }
    }
}