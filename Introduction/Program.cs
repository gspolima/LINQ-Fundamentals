using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Windows\System32";
            var headerInfo = $"Five largest files " +
                             BreakLine() +
                             $"Current directory >> {path}" +
                             BreakLine();

            Console.WriteLine(headerInfo);
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("--- LINQ ---");
            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            var query = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(5);
                        
            foreach (var file in query)
            {
                var result = FormatResult(file);
                Console.WriteLine(result);
            }
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            for (int i = 0; i < 5; i++)
            {
                var file = files[i];
                var result = FormatResult(file);
                Console.WriteLine(result);
            }
        }

        private static string FormatResult(FileInfo file)
        {
            var kbSize = file.Length / 1024;
            return ($"{file.Name,-25} : {kbSize,5:N0} KB");
        }

        private static string BreakLine()
        {
            return Environment.NewLine;                         
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
