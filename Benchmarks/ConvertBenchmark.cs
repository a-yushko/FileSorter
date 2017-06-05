using BenchmarkDotNet.Attributes;
using DataReaderWriter;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    public class ConvertBenchmark
    {
        [Benchmark(Baseline = true)]
        public void IntParse()
        {
            foreach (var i in _ints)
                Int32.Parse(i.ToString());
        }

        [Benchmark]
        public void IntConvert()
        {
            foreach (var i in _ints)
                DataSource.ToInt32(i);
        }

        [Benchmark]
        public void IntConvert16()
        {
            foreach (var i in _ints)
                DataSource.ToUInt16(i);
        }

        private static List<string> GetStrings()
        {
            var list = new List<string>();
            for (int i = 0; i < 262144; i++)
                list.Add(i.ToString());
            return list;
        }
        private List<string> _ints = GetStrings();
    }
}
