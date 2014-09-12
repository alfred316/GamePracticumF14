using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//changes will be made upon integration.
//
//I think read() should take our premade lua object in KLITest 
//as a parameter and then load the lines of the text file into it.
namespace LuaSave
{
    public static class LS
    {
        private static string luaPathString = @"c:\save";
        private static string luaFullPathString = @"c:\save\LuaFunction.lua";
	
	//.lua will be appended to the end of the filename
	public static void changeFileName(string filename) {
		luaFullPathString = @"c:\save\" + filename + ".lua";
	}
        //write to a text file here.
        public static void write(string line)
        {
            if (!(System.IO.File.Exists(luaFullPathString)))
            {
                System.IO.Directory.CreateDirectory(luaPathString);
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(luaFullPathString))
                {
				}
            }

	    
	    if (System.IO.File.Exists(luaFullPathString))
            {
                try
                {
                    //opens and automatically closes the file stream once the line is written
                    using (System.IO.StreamWriter fs = System.IO.File.AppendText(luaFullPathString))
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
