using DataReaderWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWriter
{
    public static class DataSource
    {
        public static IEnumerable<DataRecord> ToIEnumerable(IEnumerable<string> rawData)
        {
            foreach (var line in rawData)
            {
                var r = GetRecord(line);
                if (r == null)
                    continue; // skip malformed strings
                yield return r;
            }
        }

        public static IEnumerable<DataRecord> ToLinkedList(IEnumerable<string> rawData)
        {
            var result = new LinkedList<DataRecord>();
            foreach (var line in rawData)
            {
                var r = GetRecord(line);
                if (r == null)
                    continue; // skip malformed strings
                result.AddLast(r);
            }
            return result;
        }

        /// <summary>
        /// Parses string and returns record. null if error occurrs
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static DataRecord GetRecord(string line)
        {
            var s = line.Split('.');
            if (s.Length < 2)
                return null; // skip malformed strings
            return new DataRecord(UInt16.Parse(s[0]), s[1].TrimStart());
        }

        public static IEnumerable<string> ToString(IEnumerable<DataRecord> records)
        {
            foreach (var record in records)
                yield return record.ToString();
        }
    }
}
