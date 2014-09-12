using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//A singleton class that creates a text log for our LUA and C# scripts in unity.
namespace GameDevLog
{

    public static class GDLog
    {
        private static string pathString = @"c:\GameDevLog\log";
        private static bool isFirstRun = true;


        //the main function of GDLog. 
        //use by callign GDInstance.log("string"), and it will copy the string into the text file.
        public static void log(string line)
        {
            if (isFirstRun == true)
            {
                System.IO.Directory.CreateDirectory(pathString);
                pathString = pathString + @"\log.txt";
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(pathString))
                {
                    sw.WriteLine("Unity C#/Lua Script Log has begun: ");
                }
                isFirstRun = false;
            }
            //adds to the head to the line we would like to log
            line = "Unity Script Log " + DateTime.Now + ": " + line;
            if (!isFirstRun)
            {
                try
                {
                    //opens and automatically closes the file stream once the line is written
                    using (System.IO.StreamWriter fs = System.IO.File.AppendText(pathString))
                    {
                        fs.WriteLine(line);
                    }
                }

                catch
                {
                    //writes error if log cannot open the file correctly
                    Console.WriteLine("ERROR: log did not write");
                }
            }

        }
    }
}

