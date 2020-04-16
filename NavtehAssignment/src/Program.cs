using NavtehAssignment.src._utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NavtehAssignment
{
    class Program
    {


        static void Main(string[] args)
        {

            // get the text content
            var textFileLines = FileUtil.ReadFileLines("");
            var textFileContent = FileUtil.ReadFile("").Trim();

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

            // TODO remove overlapped strings 
            foreach(var listItem in substringList)
            {

            }



        }
    }
}
