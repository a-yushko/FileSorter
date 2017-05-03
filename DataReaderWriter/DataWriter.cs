using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWriter
{
    public class DataWriter
    {
        const int KB = 1024;
        const int MB = 1024*KB;
        public void Write(string path, long size)
        {
            using (var textWriter = File.CreateText(path))
            {
                var generator = new DataGenerator(textWriter);
                generator.GenerateContent(size*MB);
            }
        }
        public void Write(string path, IEnumerable<string> data)
        {
            using (var textWriter = File.CreateText(path))
            {
                foreach (var line in data)
                    textWriter.WriteLine(line);
            }
        }
    }
}
