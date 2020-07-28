﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace salonfrSource.Log
{
    public static class SLogToFile
    {
        public static void SaveInfoInFile(string text)
        {
           string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.AppendAllText(PathDBFile+ @"\salonFrLog.txt", text);
        }
        public static void SaveDataTebleInToFile(string tabelName,string text)
        {
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            File.AppendAllText(PathDBFile + @"\"+tabelName+"_log.txt", text);
        }

    }
}
