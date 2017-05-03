using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWriter
{
    public class DataRecord : IComparable<DataRecord>
    {
        public DataRecord(ushort key, string value)
        {
            Key = key;
            Value = value;
        }
        public ushort Key { get; set; }
        public string Value { get; set; }

        public int CompareTo(DataRecord o)
        {
            if (o == null)
                return 1;
            int result = String.Compare(Value, o.Value);
            if (result == 0)
                result = Key - o.Key;
            return result;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(Value))
                return $"{Key}.";
            return $"{Key}. {Value}";
        }
    }

    public class RecordComparer : Comparer<DataRecord>
    {
        public override int Compare(DataRecord x, DataRecord y)
        {
            if (x == null)
                return -1;
            return x.CompareTo(y);
        }
    }

    public class ReversedRecordComparer : Comparer<DataRecord>
    {
        public override int Compare(DataRecord x, DataRecord y)
        {
            if (x == null)
                return -1;
            return -x.CompareTo(y);
        }
    }
}
