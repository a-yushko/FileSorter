using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using DataSorter;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using DataReaderWriter;

namespace Benchmarks
{
    [TestClass]
    public class Benchmarks
    {
        public Benchmarks()
        {
            _lines = File.ReadLines(_path).ToList();
            _records = DataSource.ToIEnumerable(_lines).ToList();
            _linkedList = DataSource.ToLinkedList(_lines);
            _strings = _lines.ToList();
        }
        [TestMethod]
        public void ParseRecords()
        {
            var timer = Stopwatch.StartNew();

            var records = DataSource.ToIEnumerable(_lines).ToList();

            timer.Stop();
            Assert.Inconclusive($"ParseRecords took: {timer.Elapsed}");
        }

        [TestMethod]
        public void SwapRecords()
        {
            var timer = Stopwatch.StartNew();

            StringSource.Convert(ref _lines);

            timer.Stop();
            Assert.Inconclusive($"SwapRecords took: {timer.Elapsed}");
        }

        [TestMethod]
        public void SortOrderBy()
        {
            var timer = Stopwatch.StartNew();
            
            var sorted = Sorter.SortOrderBy(_records);
            sorted.ToList();

            timer.Stop();
            Assert.Inconclusive($"SortOrderBy took: {timer.Elapsed}");
        }

        [Ignore]
        public void SortWithComparer()
        {
            var timer = Stopwatch.StartNew();

            var sorted = Sorter.SortOrderByComparer(_records);
            sorted.ToList();

            timer.Stop();
            Assert.Inconclusive($"SortOrderByComparer took: {timer.Elapsed}");
        }

        [TestMethod]
        public void SortList()
        {
            var timer = Stopwatch.StartNew();

            Sorter.SortList(_records);

            timer.Stop();
            Assert.Inconclusive($"SortList took: {timer.Elapsed}");
        }

        [Ignore]
        public void SortLinkedList()
        {
            var timer = Stopwatch.StartNew();

            var sorted = Sorter.SortOrderBy(_linkedList);
            sorted.ToList();

            timer.Stop();
            Assert.Inconclusive($"SortLinkedList took: {timer.Elapsed}");
        }

        [TestMethod]
        public void SortStrings()
        {
            var timer = Stopwatch.StartNew();

            _strings.Sort();

            timer.Stop();
            Assert.Inconclusive($"SortLinkedList took: {timer.Elapsed}");
        }

        List<string> _lines;
        List<DataRecord> _records;
        IEnumerable<DataRecord> _linkedList;
        List<string> _strings;
        readonly string _path = @"..\..\..\Data\source10m.txt";
    }
}
