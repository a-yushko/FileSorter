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
            return new DataRecord(ToUInt16(s[0]), s[1].TrimStart());
        }

        public static IEnumerable<string> ToString(IEnumerable<DataRecord> records)
        {
            foreach (var record in records)
                yield return record.ToString();
        }

        public static ushort ToUInt16(string str)
        {
            ushort r = 0;
            for (int i = 0; i < str.Length; i++)
                r = (ushort)(r * 10 + (str[i] - '0'));
            return r;
        }

        public static int ToInt32(string str)
        {
            int r = 0;
            for (int i = 0; i < str.Length; i++)
                r = r * 10 + (str[i] - '0');
            return r;
        }
    }
}
