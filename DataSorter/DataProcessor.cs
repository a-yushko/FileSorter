using DataReaderWriter;
using Sinbadsoft.Lib.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static DataSorter.NativeMethods;

namespace DataSorter
{
    public class DataProcessor
    {
        const int KB = 1024;
        const int MB = 1024 * KB;
        const int GB = 1024 * MB;
        Logger logger;
        List<Task> tasks = new List<Task>();
        List<string> filenames = new List<string>();
        public DataProcessor(Logger logger)
        {
            this.logger = logger;
        }
        public void Process(string path, ProcessingOptions options)
        {
            long pageSize = options.PageSize > 0 ? options.PageSize * MB : GetPageSize();
            if (options.Brute || CanProcessAll(path, pageSize))
                ProcessAll(path);
            else
                ProcessPaged(path, pageSize, options.KeepIntermediate);
        }

        private void ProcessAll(string path)
        {
            logger.WriteLine("Reading data...");
            var reader = new DataReader();
            var records = reader.Read(path);
            reader = null;
            records.TrimExcess();
            GC.Collect();

            logger.WriteLine("Sorting...");
            Sorter.SortList(records);

            logger.WriteLine("Saving data...");
            var writer = new DataWriter();
            writer.Write(GetOutPath(path), DataSource.ToString(records));
        }

        private void ProcessPaged(string path, long pageSize, bool keep = false)
        {
            logger.WriteLine($"Paged reading. Page size: {pageSize / MB}MB");

            var reader = new DataReader();
            reader.PageFinished += OnPageFinishedReading;

            logger.WriteLine("Reading data...");
            reader.ReadPaged(path, pageSize);

            Merge(filenames, GetOutPath(path));

            if (!keep)
            {
                logger.WriteLine("Cleanup...");
                foreach (var filename in filenames)
                    File.Delete(filename);
            }
        }

        private void OnPageFinishedReading(object sender, PageFinishedArgs e)
        {
            int id = e.Id;

            var records = e.Records;
            logger.WriteLine($"Sorting block #{id}...");
            Sorter.SortList(records);

            logger.WriteLine($"Writing block #{id}...");
            var writer = new DataWriter();
            var name = GetOutPath(e.Path, id);
            filenames.Add(name);
            writer.Write(name, DataSource.ToString(records));
        }

        public long GetPageSize()
        {
            long size = -1;
            ulong installedMemory;
            MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
            if (GlobalMemoryStatusEx(memStatus))
            {
                installedMemory = memStatus.ullTotalPhys;
                size = (long)installedMemory / 16;
            }
            return size;
        }

        private bool CanProcessAll(string path, long pageSize)
        {
            var info = new FileInfo(path);
            if (0.9 * info.Length < pageSize)
                return true;  // file small enough to be processed entirely
            return false;
        }

        private void Merge(List<string> filenames, string outfile)
        {
            logger.WriteLine($"Merging {filenames.Count} blocks...");
            var queue = new PriorityQueue<DataRecord, StreamReader>(new ReversedRecordComparer(), filenames.Count);
            List<StreamReader> streams = null;
            StreamWriter outStream = null;
            try
            {
                outStream = File.CreateText(outfile);
                streams = filenames.Select(filename => File.OpenText(filename)).ToList();
                // fill the queue
                foreach (var stream in streams)
                {
                    var record = GetRecord(stream);
                    if (record != null)
                        queue.Enqueue(record, stream);
                }
                // merge
                while (queue.Count > 0)
                {
                    var smallest = queue.Dequeue();
                    var record = smallest.Key;
                    var smallestStream = smallest.Value;
                    outStream.WriteLine(record.ToString());

                    var newRecord = GetRecord(smallestStream);
                    if (newRecord != null)
                        queue.Enqueue(newRecord, smallestStream);
                }
            }
            finally
            {
                foreach (var stream in streams)
                    stream.Dispose();
                outStream.Dispose();
            }
        }

        DataRecord GetRecord(StreamReader stream)
        {
            var line = stream.ReadLine();
            if (line != null)
                return DataSource.GetRecord(line);
            return null;
        }


        private static string GetOutPath(string path)
        {
            return Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path)),
                Path.GetFileNameWithoutExtension(path) + ".sorted" + Path.GetExtension(path));
        }

        private static string GetOutPath(string path, int id)
        {
            return Path.Combine(Path.GetDirectoryName(Path.GetFullPath(path)),
                Path.GetFileNameWithoutExtension(path) + id.ToString() + Path.GetExtension(path));
        }
    }
    public class ProcessingOptions
    {
        /// <summary>
        /// Forces entire file processing
        /// </summary>
        public bool Brute { get; set; }
        /// <summary>
        /// Keep intermediage results
        /// </summary>
        public bool KeepIntermediate { get; set; }
        /// <summary>
        /// Size in MB
        /// </summary>
        public int PageSize { get; set; }
    }
}
