using NavtehAssignment.src._utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NavtehAssignment
{
    class Program
    {
        private static string[] _textFileLines;
        private static string _textFileContent;


        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Enter file path: ");
                var input = Console.ReadLine();

                _textFileLines = FileUtil.ReadFileLines(input);
                _textFileContent = FileUtil.ReadFile(input).Trim();             
                
                FromBiggestToSmallest();
                FromSmallestToBiggest();
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }          
        }

        private static void FromBiggestToSmallest()
        {

            string[] textFileLines = new string[_textFileLines.Length];
       
            // string comparison is bugged, so we lowercase the strings
            for(int i = 0; i < textFileLines.Length; i++)
            {
                textFileLines[i] = _textFileLines[i].Trim().ToLowerInvariant();
            }           
       
            List<List<string>> substringList = new List<List<string>>();       

            foreach (var line in textFileLines)
            {
                var lineSize = line.Length;
                var tempList = new Dictionary<string, int>();               

                // we begin with the biggest substring length
                for (int substringLength = line.Length; substringLength > 0; substringLength--)
                {
                  

                    /// if the substring length is bigger than half of the line, we stop searching
                    /// substring cannot be bigger than main string
                    /// we continue, because we go from bigest to smallest
                    if (substringLength > line.Length+1 / 2) continue;

                    // if under the threshold - stop and go to next line
                    if (substringLength < 4)
                    {
    
                        substringList.Add(new List<string>(tempList.Keys));
                        tempList.Clear();
                        break;
                    }

                    // we check per line 
                    for (int i = 0; i < lineSize; i++)
                    {
                        // if the line size is smaller then the substring we break
                        // this usually means we start at the end of the line when it passes
                        if (i + substringLength > lineSize) break;

                        // the beginning substring is the whole line
                        var substring = line.Substring(i, substringLength);                        
                        // how many times the substring occurs in the line (or text)
                        var substringOccurence = StringUtil.CountSubstringOccurence(line, substring);

                        if (substringOccurence > 1)
                        {
                         
                            if (!tempList.Any(x => x.Key.Contains(substring)))
                                tempList.Add(substring, substringOccurence);
                         
                            // we create a new dictionary, that has items that have this substring
                            var substringOverlapDictionary = tempList.Where(x => x.Key.Contains(substring));

                            /// we go through all the items in the dictionary
                            /// we check if it occurs more times than any other item
                            /// if it occurs more times, we add
                            foreach (var kvPair in new Dictionary<string, int>(substringOverlapDictionary).OrderBy(x => x.Key.Length))
                            {
                                var key = kvPair.Key;
                                var value = kvPair.Value;

                                if (value < substringOccurence)
                                {
                                    // if we are at the last item
                                    if (key.Equals(substringOverlapDictionary.Last().Key))
                                    {
                                        tempList.Add(substring, substringOccurence);
                                    };
                                    break;
                                }
                                else continue;
                            }
                        }
                    }
                }
            }

            OutputResult(substringList, Search_type.BIG_TO_SMALL);

        }


        private static void FromSmallestToBiggest()
        {
            string[] textFileLines = new string[_textFileLines.Length];
            
            // string comparison is bugged, so we lowercase the strings
            for (int i = 0; i < textFileLines.Length; i++)
            {
                textFileLines[i] = _textFileLines[i].Trim().ToLowerInvariant();
            }                      
            
            List<List<string>> substringList = new List<List<string>>();            

            foreach (var line in textFileLines)
            {


                var lineSize = line.Length;
                var tempList = new Dictionary<string, int>();


                for (int substringLength = 4; substringLength < lineSize; substringLength++)
                {

                    /// if the substring length is bigger than half of the line, we stop searching
                    /// substring cannot be bigger than main string
                    /// we BREAK, because we go from smallest to biggest
                    if (substringLength > line.Length + 1 / 2) break;

                    // we check per line 
                    for (int i = 0; i < lineSize; i++)
                    {                      
                        if (i + substringLength > lineSize) break;                        
                        var substring = line.Substring(i, substringLength);                      
                        var substringOccurence = StringUtil.CountSubstringOccurence(line, substring);

                        if (substringOccurence > 1)
                            tempList.TryAdd(substring, substringOccurence);

                    }
                }

                foreach(var kvp in new Dictionary<string,int>(tempList).OrderBy(x => x.Key.Length))
                {
                    var key = kvp.Key;
                    var value = kvp.Value;

                    if (tempList.Any(x => x.Key.Contains(key) && x.Value >= value && x.Key != key))
                        tempList.Remove(key);
                }


                substringList.Add(new List<string>(tempList.Keys));
                tempList.Clear();
            }

            OutputResult(substringList, Search_type.SMALL_TO_BIG);
        }

        private static void OutputResult(List<List<string>> substringList, Search_type sType)
        {
            List<List<string>> resultList = new List<List<string>>();
            var sb = new StringBuilder();

            switch (sType)
            {
                case Search_type.BIG_TO_SMALL: Console.WriteLine("Big to small"); break;
                case Search_type.SMALL_TO_BIG: Console.WriteLine("Small to big"); break;
                default: Console.WriteLine("Sword of a thousand truths"); break; 
            }

            foreach (var listItem in substringList)
            {
                if (listItem.Count == 0) continue;

                sb.Append("[");                
                foreach (var item in listItem)
                {
                    if (listItem.Last() != item) sb.Append($"{item.ToString()},"); else sb.Append(item.ToString());
                }
                sb.Append("]\n");                
            }

            Console.WriteLine(sb);
        }


        enum Search_type
        {
            BIG_TO_SMALL,
            SMALL_TO_BIG
        }


    }
}
