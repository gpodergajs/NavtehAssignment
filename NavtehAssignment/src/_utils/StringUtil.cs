using System;
using System.Collections.Generic;
using System.Text;

namespace NavtehAssignment.src._utils
{
    public static class StringUtil
    {
        public static int CountSubstringOccurence(string source, string substring)
        {
            // counts all the occurences
            int count = 0;
            int i = 0;
            while ((i = source.IndexOf(substring, i)) != -1)
            {
                i += substring.Length;
                count++;
            }
            return count;
        }
    }
}
