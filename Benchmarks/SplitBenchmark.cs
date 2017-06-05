using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using DataReaderWriter;

namespace Benchmarks
{
    public class SplitBenchmark
    {
        [Benchmark(Baseline = true)]
        public void StringSplit()
        {
            string[] lines;
            foreach (var line in _lines)
                lines = line.Split('.');
        }

        [Benchmark]
        public void CustomSplit()
        {
            string s1, s2;
            foreach (var line in _lines)
                DataSource.Split(line, '.', out s1, out s2);
        }

        private static List<string> GetLines()
        {
            return new List<string>()
            {
                "897. lazy quick lazy brown rabbit lazy the",
                "36. grey fox penguin jumps lazy",
                "48. red lazy the rabbit dog lazy brown over the the rabbit rabbit rabbit",
                "805. the brown spotted grey over red spotted red red spotted",
                "924. rabbit lazy grey jumps spotted the lazy spotted grey",
                "319. over penguin lazy grey over lazy fox",
                "196. over rabbit the the the over jumps dog fox",
                "638. over penguin quick dog spotted jumps grey the",
                "838. penguin over fox spotted grey grey spotted dog grey",
                "318. over fox red quick quick jumps spotted grey red dog",
                "508. lazy dog the dog dog fox red over",
                "659. penguin over spotted fox dog",
                "523. the penguin red fox spotted",
                "993. penguin spotted spotted penguin rabbit rabbit the lazy jumps",
                "979. dog jumps brown penguin over quick lazy red",
                "239. over grey the over brown",
                "832. dog fox brown penguin penguin penguin penguin",
                "69. red grey grey the grey red dog",
                "742. penguin",
                "98. rabbit quick over lazy lazy fox rabbit the red",
            };
        }
        private List<string> _lines = GetLines();
    }
}
