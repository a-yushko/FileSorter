using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReaderWriter
{
    public static class StringSource
    {
        public static void Convert(ref List<string> rawData)
        {
            int count = rawData.Count;
            for(int i = 0; i < count; i++)
            {
                var line = rawData[i];
                SwapString(ref line);
            }
        }

        public static void SwapString(ref string str)
        {
            char dot = '.';
            int pos = str.IndexOf(dot);
            if (pos == -1)
                return; // no change required
            unsafe
            {
                fixed (char* pStr = str)
                {
                    int i = 0;
                    int j = pos+1;
                    int k = 0;
                    // copy first part
                    for (k = 0; k < j; k++)
                        num[k] = pStr[k];
                    
                    while(j < str.Length)
                    {
                        pStr[i] = pStr[j];
                        j++; i++;
                    }
                    pStr[i] = dot;
                    i++;
                    k = 0;
                    // insert first part
                    while (i < str.Length)
                    {
                        pStr[i] = num[k];
                        i++; k++;
                    }
                }
            }
        }

        static char[] num = new char[MAX_STR];
        const int MAX_STR = 8192;   // 8K
    }
}
