using NavtehAssignment.src._utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            }
            catch (Exception e)
            {
                if(e.InnerException is FileNotFoundException)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                Console.WriteLine(e.Message);                
            }          
        }

        private static void FromBiggestToSmallest()
        {
            var textFileLines = _textFileLines;
            var textFileContent = _textFileContent;

            List<string> substringList_line = new List<string>();
            List<List<string>> substringList = new List<List<string>>();
            string previousLine = String.Empty;

            foreach (var line in textFileLines)
            {                
                // remove uncecessary characters
                line.Trim();

                var lineSize = line.Length;
                var tempList = new List<string>();

                // we begin with the biggest substring length
                for (int substringLength = textFileContent.Length; substringLength > 0; substringLength--)
                {
                    // if smaller than 4 we go to next line and save all the substrigs
                    if (substringLength < 4)
                    {
                        previousLine = line;
                        substringList.Add(new List<string>(tempList));
                        tempList.Clear();
                        break;
                    }

                    // we check per line 
                    for (int i = 0; i < lineSize; i++)
                    {
                        // if the line size is smaller then the substring we break
                        // this usually mean we start at the end of the line when it passes
                        if (i + substringLength > lineSize) break;

                        // the beggining substring is the whole line
                        var substring = line.Substring(i, substringLength);

                        var textFileContentSubstring = textFileContent
                         .Remove(previousLine != null ? previousLine.Length + i : i, substringLength);


                        // if text contains substring at least once, then add
                        if (textFileContentSubstring.Contains(substring, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!tempList.Contains(substring))
                            {
                                substringList_line.Add(substring);
                                tempList.Add(substring);
                            }
                        }
                    }
                }
            }

            OutputResult(substringList, Search_type.BIG_TO_SMALL);

        }


        private static void FromSmallestToBiggest()
        {
            var textFileLines = _textFileLines;
            var textFileContent = _textFileContent;

            // init variables
            List<string> substringList_line = new List<string>();
            List<List<string>> substringList = new List<List<string>>();
            string previousLine = String.Empty;

            foreach (var line in textFileLines)
            {
                // empty the list
                substringList_line.Clear();
                // remove uncecessary characters
                line.Trim();

                var lineSize = line.Length;
                var tempList = new List<string>();

                for (int substringLength = 4; substringLength < textFileContent.Length; substringLength++)
                {

                    for (int i = 0; i < lineSize; i++)
                    {
                        if (i + substringLength >= lineSize) break;

                        var substring = line.Substring(i, substringLength);
                        // dodaj : če obstaja previous line, vzemi njegov size + i in začni od tukaj removat
                        var textFileContentSubstring = textFileContent
                            .Remove(previousLine != null ? previousLine.Length + i : i, substringLength);

                        // if text contains substring at least once, then add
                        if (textFileContentSubstring.Contains(substring, StringComparison.OrdinalIgnoreCase))
                        {
                            // check if already exists and add
                            if (!tempList.Contains(substring))
                                tempList.Add(substring);
                        }
                    }

                    // if the temp list is empty no substrings were found so we go to the new line
                    if (tempList.Count == 0)
                    {
                        previousLine = line;
                        break;
                    }

                    // add relevant substrings to list
                    substringList_line.AddRange(tempList);
                    // clear temp list
                    tempList.Clear();

                }

                // add the list to the list and clear
                substringList.Add(new List<string>(substringList_line));
                substringList_line.Clear();

                // end of initial loop
            }

            OutputResult(substringList, Search_type.SMALL_TO_BIG);
        }

        private static void OutputResult(List<List<string>> substringList, Search_type sType)
        {
            List<List<string>> resultList = new List<List<string>>();
            var sb = new StringBuilder();

            foreach (var listItem in substringList)
            {
                resultList.Add(new List<string>(ListUtil.RemoveOverlappedStrings(ListUtil.OrderByLength(listItem))));
            }


            switch (sType)
            {
                case Search_type.BIG_TO_SMALL: Console.WriteLine("Big to small"); break;
                case Search_type.SMALL_TO_BIG: Console.WriteLine("Small to big"); break;
                default: Console.WriteLine("Sword of a thousand truths"); break; 
            }

            foreach (var listItem in resultList)
            {
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
