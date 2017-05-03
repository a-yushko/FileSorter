using DataReaderWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 2)
            {
                PrintUsage();
                return;
            }
            try
            {
                int size = 0;
                if (!Int32.TryParse(args[1], out size) || size < 0)
                {
                    Console.WriteLine($"Invalid size specified: {args[1]}");
                    PrintUsage();
                    return;
                }
                EnsureDirectoryExists(Path.GetDirectoryName(Path.GetFullPath(args[0])));
                Console.WriteLine($"Writing file {args[0]}");

                var stopwatch = Stopwatch.StartNew();
                var writer = new DataWriter();
                writer.Write(args[0], size);

                stopwatch.Stop();
                Console.WriteLine($"Operation completed in {stopwatch.Elapsed}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: <datagenerator> outfilename size(MB)");
        }
    }
}
