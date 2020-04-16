using System.Collections.Generic;
using System.Linq;

namespace NavtehAssignment.src._utils
{
    public static class ListUtil
    {

        public static List<string> OrderByLength(List<string> stringList)
        {
            var sortedStringList = stringList
            .OrderByDescending(n => n.Length)
            .ToList();

            return sortedStringList;
        }

        public static List<string> RemoveOverlappedStrings(List<string> stringList)
        {
            List<string> listWithoutOverlap = new List<string>(stringList);
            foreach (var item in stringList)
            {
                var strDupList = stringList.Where(str => item.Contains(str) && !str.Equals(item));
                listWithoutOverlap = listWithoutOverlap.Except(strDupList).ToList();
            }
            return listWithoutOverlap;  
        }
    }
}
