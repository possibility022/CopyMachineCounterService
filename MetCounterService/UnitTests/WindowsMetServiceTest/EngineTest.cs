using System;
using System.Diagnostics;
using System.Threading;
using WindowsMetService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.WindowsMetServiceTest
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void CheckEngine()
        {
            Engine engine = new Engine();
            Thread.Sleep(engine.StartDelay + (2 * 1000));

            Trace.WriteLine(engine.tickTime.ToString());
        }
    }
}
