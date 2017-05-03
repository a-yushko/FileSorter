using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWriter
{
    public class DataReader
    {
        public List<DataRecord> Read(string path)
        {
            var result = new List<DataRecord>();
            using (var reader = File.OpenText(path))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var record = DataSource.GetRecord(line);
                    if (record != null)
                        result.Add(record);
                }
            }
            return result;
        }

        public List<string> ReadLines(string path)
        {
            var result = new List<string>();
            using (var reader = File.OpenText(path))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    StringSource.SwapString(ref line);
                    result.Add(line);
                }
            }
            return result;
        }

        public IEnumerable<DataRecord> ReadList(string path)
        {
            var result = new LinkedList<DataRecord>();
            using (var reader = File.OpenText(path))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var record = DataSource.GetRecord(line);
                    if (record != null)
                        result.AddLast(record);
                }
            }
            return result;
        }

        public IEnumerable<DataRecord> ReadArray(string path, long maxCount)
        {
            var result = new DataRecord[maxCount];
            using (var reader = File.OpenText(path))
            {
                long i = 0;
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null && i < maxCount)
                {
                    var record = DataSource.GetRecord(line);
                    if (record != null)
                        result[i] = record;
                    i++;
                }
            }
            return result;
        }

        public void ReadPaged(string path, long pageSize)
        {
            var result = new List<DataRecord>();
            int i = 0;
            using (var reader = File.OpenText(path))
            {
                string line = String.Empty;
                long start = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    var record = DataSource.GetRecord(line);
                    if (record != null)
                        result.Add(record);
                    if (reader.BaseStream.Position - start > pageSize)
                    {
                        RaisePageFinished(path, i, result);
                        start = reader.BaseStream.Position;
                        i++;
                    }
                }
            }
            RaisePageFinished(path, i, result);
        }

        public event EventHandler<PageFinishedArgs> PageFinished;

        protected void RaisePageFinished(string path, int id, List<DataRecord> records)
        {
            if (records.Count > 0)
            {
                PageFinished?.Invoke(this, new PageFinishedArgs { Records = records, Path = path, Id = id });
                records.Clear();
                records.TrimExcess();
                GC.Collect();
            }
        }
    }

    public class PageFinishedArgs : EventArgs
    {
        public List<DataRecord> Records { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
    }
}
