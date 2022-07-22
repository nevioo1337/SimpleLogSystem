using System;
using System.IO;

namespace SimpleLogSystem
{
    internal class Log
    {
        public static string path = @"logs\";
        public static bool createLogs = true;
        public static bool deleteLogs = true;
        public static int deleteAfterDays = 30;

        public static void WriteLine(string Content)
        {
            if (createLogs)
            {
                var dt = DateTime.Now;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.AppendAllText(path + dt.ToString("yyyy_MM_dd") + ".log", Content + "\n");
            }
        }

        public static void CheckValidity()
        {
            if (deleteLogs)
            {
                var dt = DateTime.Now;
                dt = DateTime.Parse(dt.ToString("dd.MM.yyyy"));

                string[] Files = Directory.GetFiles(path, "*.log");
                foreach (var item in Files)
                {
                    var FileDateTime = item.Replace(path, "");
                    FileDateTime = FileDateTime.Replace(".log", "");

                    DateTime FileDT = new DateTime();
                    FileDT = DateTime.Parse(DateTime.ParseExact(FileDateTime, "yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy"));

                    if (dt >= FileDT.AddDays(deleteAfterDays))
                    {
                        File.Delete(item);
                    }
                }
            }
        }
    }
}
