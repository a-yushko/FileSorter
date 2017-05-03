using DataReaderWriter;
using DataSorter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [TestClass]
    public class PageSize
    {
        [TestMethod]
        public void GetPageSize()
        {
            var processor = new DataProcessor(new Logger());
            var size = processor.GetPageSize(_path);
            Assert.Inconclusive($"Page size is {size}");
        }

        [TestMethod]
        public void TestShift()
        {
            var s2 = "123. Apple";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" Apple.123", s2);

            s2 = "4294967295. Squirrel";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" Squirrel.4294967295", s2);

            StringSource.SwapString(ref s2);
            Assert.AreEqual("4294967295. Squirrel", s2);

            s2 = "Rabbit";
            StringSource.SwapString(ref s2);
            Assert.AreEqual("Rabbit", s2);
        }
        readonly string _path = @"..\..\..\Data\source16g.txt";
    }
}
