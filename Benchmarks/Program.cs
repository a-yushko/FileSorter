using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks
{
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<SplitBenchmark>();
            summary = BenchmarkRunner.Run <ConvertBenchmark>();
        }
    }
}
