using System;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Tests
{
    public class TestYouAndCthulhu
    {
        [Test]
        public void TestSimpleProgram1()
        {
            using (StringWriter sw = new StringWriter())
            {
                string output;
                Console.SetOut(sw);
                
                YouAndCthulhu.Program.CthulhuSlaver("../../../Test1");
                
                output = sw.ToString();
                Assert.AreEqual("3\n", output);
            }
        }
        
        [Test]
        public void TestSimpleProgram2()
        {
            string input = "5\n";
            using (StringReader sr = new StringReader(input))
            {
                using (StringWriter sw = new StringWriter())
                {
                    string output;
                    Console.SetIn(sr);
                    Console.SetOut(sw);
                    YouAndCthulhu.Program.CthulhuSlaver("../../../Test2");

                    output = sw.ToString();
                    Assert.AreEqual("Waiting for integer input: \n5\n4\n3\n2\n1\n0\n", output);
                }
            }
        }
        
        [Test]
        public void TestSimpleProgram3()
        {
            string input = "5\n250\n";
            using (StringReader sr = new StringReader(input))
            {
                using (StringWriter sw = new StringWriter())
                {
                    string output;
                    Console.SetIn(sr);
                    Console.SetOut(sw);
                    YouAndCthulhu.Program.CthulhuSlaver("../../../Test3");

                    output = sw.ToString();
                    Assert.AreEqual("Waiting for integer input: \nWaiting for integer input: \n255\n", output);
                }
            }
        }
        
        [Test]
        public void TestSimpleProgram4()
        {
            string input = "2244245\n";
            using (StringReader sr = new StringReader(input))
            {
                using (StringWriter sw = new StringWriter())
                {
                    string output;
                    Console.SetIn(sr);
                    Console.SetOut(sw);
                    YouAndCthulhu.Program.CthulhuSlaver("../../../Test4");

                    output = sw.ToString();
                    Assert.AreEqual("Waiting for integer input: \n289\n", output);
                }
            }
        }
    }
}