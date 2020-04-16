using System;
using System.IO;

namespace NavtehAssignment.src._utils
{
    public static class FileUtil
    {        

        public static string[] ReadFileLines(string path)
        {
            path = "C:\\Users\\gpode\\Desktop\\txt\\test.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();
                return File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static string ReadFile(string path)
        {
             path = "C:\\Users\\gpode\\Desktop\\txt\\test.txt";
             try
             {
                if (!File.Exists(path)) throw new FileNotFoundException();

                string fileText = File.ReadAllText(path);
                return fileText;

             }
             catch (Exception ex)
             {
                 throw ex;
             }
        }
    }
}
