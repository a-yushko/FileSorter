using DataReaderWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                PrintUsage();
                return;
            }
            var stopwatch = Stopwatch.StartNew();
            try
            {
                Task.Factory.StartNew(() => ConsoleSpinner.RunAnimation());


                ProcessingOptions options = new ProcessingOptions();
                for (int i = 1; i < args.Count(); i++)
                    ParseArgs(args, i, ref options);

                var processor = new DataProcessor(new Logger());
                processor.Process(args[0], options);

                stopwatch.Stop();
                Console.WriteLine($"Operation completed in {stopwatch.Elapsed}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
                Console.WriteLine($"{stopwatch.Elapsed}");
            }
        }

        public static void ParseArgs(string[] args, int current, ref ProcessingOptions options)
        {
            if (String.Compare("-b", args[current], true) == 0)
                options.Brute = true;
            else if (String.Compare("-k", args[current], true) == 0)
                options.KeepIntermediate = true;
            else if (String.Compare("-p", args[current], true) == 0)
            {
                if (current + 1 < args.Count())
                    options.PageSize = Int32.Parse(args[current+1]);
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: <datasorter> filename [-b] [-k] [-p pageSize(MB)]");
        }
    }
}
