using NUnit.Framework;
using YouAndCthulhu;

namespace Tests
{
    public class TestFunction
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateFunction()
        {
            Function f = new Function(0, 'A', "iii", null);
            Assert.Pass();
        }

        [Test]
        public void ExecuteSimpleFunction1()
        {
            Function f = new Function(0, 'A', "iii", null);
            f.Execute();
            Assert.AreEqual(3, f.Accumulator);
        }
        
        [Test]
        public void ExecuteSimpleFunction2()
        {
            Function f = new Function(0, 'A', "ididididiiii", null);
            f.Execute();
            Assert.AreEqual(4, f.Accumulator);
        }
        
        [Test]
        public void FunctionMoveIn1()
        {
            FunctionTable ftable = new FunctionTable();
            Function f = new Function(0, 'A', "e2C", ftable);
            Function g = new Function(2, 'C', "i", ftable);
            ftable.Add(f);
            ftable.Add(g);
            f.Execute();
            Assert.AreEqual(0, f.Accumulator);
        }
        
        [Test]
        public void FunctionMoveIn2()
        {
            FunctionTable ftable = new FunctionTable();
            Function f = new Function(0, 'A', "e2C", ftable);
            Function g = new Function(2, 'C', "iiiii", ftable);
            ftable.Add(f);
            ftable.Add(g);
            f.Execute();
            Assert.AreEqual(0, f.Accumulator);
            Assert.AreEqual(0, g.Accumulator);
            g.Execute();
            f.Execute();
            Assert.AreEqual(5, f.Accumulator);
            Assert.AreEqual(5, g.Accumulator);
        }
        
        [Test]
        public void FunctionMoveOut()
        {
            FunctionTable ftable = new FunctionTable();
            Function f = new Function(0, 'A', "E2C", ftable);
            Function g = new Function(2, 'C', "dddd", ftable);
            ftable.Add(f);
            ftable.Add(g);
            g.Execute();
            Assert.AreEqual(-4, g.Accumulator);
            f.Execute();
            Assert.AreEqual(0, g.Accumulator);
        }
        
    }
}