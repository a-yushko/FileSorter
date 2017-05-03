using DataReaderWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSorter
{
    public static class Sorter
    {
        public static IEnumerable<DataRecord> SortOrderBy(IEnumerable<DataRecord> records)
        {
            return records.OrderBy(r => r.Value).ThenBy(r => r.Key);
        }
        public static IEnumerable<DataRecord> SortOrderByComparer(IEnumerable<DataRecord> records)
        {
            return records.OrderBy(r => r, new RecordComparer());
        }

        public static void SortList(List<DataRecord> records)
        {
            records.Sort(new RecordComparer());
        }
    }
}
