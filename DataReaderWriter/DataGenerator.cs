using System;
using System.IO;

namespace DataReaderWriter
{
    internal class DataGenerator
    {
        private TextWriter textWriter;

        public DataGenerator(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        /// <summary>
        /// Generates content of given size
        /// </summary>
        /// <param name="size">Size in bytes</param>
        public void GenerateContent(long size)
        {
            long contentSize = 0;
            if (size > 0)
            {
                const int NEWLINE = 2; // account for line break symbols
                while(contentSize < size)
                {
                    var line = $"{Randomizer.RandomInteger(999).ToString()}. {Randomizer.RandomSentense()}";
                    if (contentSize + line.Length + NEWLINE > size)
                    {
                        line = line.Substring(0, (int)(size - contentSize - NEWLINE));
                        if (line.Length > 1 && !line.Contains("."))
                            line = line.Remove(line.Length - 1) + "."; // prevent malformed strings if possible
                    }
                    textWriter.WriteLine(line);
                    contentSize += line.Length + NEWLINE;
                }
            }
        }
    }
}