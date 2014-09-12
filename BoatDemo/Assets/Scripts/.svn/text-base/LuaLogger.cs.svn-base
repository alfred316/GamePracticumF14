using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;
using GameDevLog;

//changes will be made upon integration.
//
//I think read() should take our premade lua object in KLITest 
//as a parameter and then load the lines of the text file into it.

namespace LuaLogger
{
    public static class LL
    {
	
        private static string luaDirectoryString = @"c:\LL\";
		private static string fullLuaPath = @"c:\LL\ll.txt";
      	private static List<string> luaLines = new List<string>();
		
		// These two variables were assigned but never used ...
		//private static string luaFileName = @"ll.txt";
        //private static bool isFirstRun = true;

	//write to a text file here.
        public static void log(string line)
        {

            if (!(System.IO.File.Exists(fullLuaPath)))
            {
               	System.IO.Directory.CreateDirectory(luaDirectoryString);
               	using (System.IO.StreamWriter sw = System.IO.File.CreateText(fullLuaPath))	
		{
	            //only here to create the file and close the stream
		    //if it ain't broke don't fix it
		    GDLog.log("Creating path for lua logger");
                }
	    }

            if (System.IO.File.Exists(fullLuaPath))
            {
		    GDLog.log("System Path exists for Lua Logger");
                try
                {
                    //opens and automatically closes the file stream once the line is written
                    using (System.IO.StreamWriter fs = System.IO.File.AppendText(fullLuaPath))
                    {
                        fs.WriteLine(line);
			GDLog.log("Supposedly I wrote a saved a lua command");
                    }
                }

                catch
                {
                    //writes error if log cannot open the file correctly
                    GDLog.log("ERROR: log did not write");
                }
            }

        }

        public static void read (Lua L)
        {
            
            using (System.IO.StreamReader sr = System.IO.File.OpenText(fullLuaPath))
            {
                string line;
		//while the reader is not at the end of text. load a lua line.
                while ((line = sr.ReadLine()) != null)
                {
		    luaLines.Add(line);
                    L.DoString(line);
		    GDLog.log(line);
                }
            }
	    luaLines.Clear();
	    using (System.IO.StreamWriter sw = System.IO.File.CreateText(fullLuaPath))	
		{
	            //only here to create the file and close the stream
		    //if it ain't broke don't fix it
		    GDLog.log("Erasing path for lua logger");
                }

        }
    }
}
