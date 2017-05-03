﻿using DataReaderWriter;
using DataSorter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetPageSize()
        {
            var processor = new DataProcessor(new Logger());
            var size = processor.GetPageSize(@"..\..\..\Data\source16g.txt");
            Assert.Inconclusive($"Page size is {size}");
        }

        [TestMethod]
        public void TestShift()
        {
            var s2 = "123. Apple";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" Apple.123", s2);

            s2 = "1. C";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" C.1", s2);

            s2 = "12345678. Melon";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" Melon.12345678", s2);

            s2 = "584. spotted lazy spotted penguin jumps penguin brown penguin penguin the dog dog lazy";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" spotted lazy spotted penguin jumps penguin brown penguin penguin the dog dog lazy.584", s2);

            s2 = "Rabbit";
            StringSource.SwapString(ref s2);
            Assert.AreEqual("Rabbit", s2);
        }

        [TestMethod]
        public void TestShiftBack()
        {
            var s2 = " brown grey.1";
            StringSource.SwapString(ref s2);
            Assert.AreEqual("1. brown grey", s2);
        }

        [TestMethod]
        public void TestShiftBoth()
        {
            string s2 = "4294967295. Squirrel";
            StringSource.SwapString(ref s2);
            Assert.AreEqual(" Squirrel.4294967295", s2);

            StringSource.SwapString(ref s2);
            Assert.AreEqual("4294967295. Squirrel", s2);
        }

        [TestMethod]
        public void TestSort1()
        {
            var list = new List<string>()
            {
                "brown",
                "brown brown",
                "brown brown grey"
            };
            list.Sort();
            Assert.Inconclusive($"{list[0]}, {list[1]}, {list[2]}");
        }

        [TestMethod]
        public void TestSort2()
        {
            var list = new List<string>()
            {
                " brown.0",
                " brown brown.871",
                " brown brown grey.25"
            };
            list.Sort();
            Assert.Inconclusive($"{list[0]}, {list[1]}, {list[2]}");
        }

        [TestMethod]
        public void TestSort3()
        {
            var list = new List<string>()
            {
                "brown.0",
                "brown brown.871",
                "brown brown grey.25"
            };
            list.Sort((x, y) => String.Compare(x, y, CultureInfo.InvariantCulture, CompareOptions.IgnoreSymbols));
            Assert.Inconclusive($"{list[0]}, {list[1]}, {list[2]}");
        }

        [TestMethod]
        public void TestToUInt16()
        {
            var v1 = DataSource.ToUInt16("0");
            Assert.AreEqual(0, v1);

            v1 = DataSource.ToUInt16("125");
            Assert.AreEqual(125, v1);

            v1 = DataSource.ToUInt16("65535");
            Assert.AreEqual(65535, v1);
        }
    }
}