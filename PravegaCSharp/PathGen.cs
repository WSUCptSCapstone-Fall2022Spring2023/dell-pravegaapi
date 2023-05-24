///
/// File: PathGen.cs
/// File Creator: Brandon Cook
/// Purpose: Contains methods for helping C# find Rust .dll files.
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pravega.Pathgen
{
    public class PathGen
    {
        public static string CreateDllPath()
        {
            var cwd = System.IO.Directory.GetCurrentDirectory();
            string code_base = "PravegaCSharp";
            int indexTo = cwd.IndexOf(code_base);
            string return_string;
            // If IndexOf could not find code_base String
            if (indexTo == -1)
            {
                return "";
            }
            return_string = cwd.Substring(0, indexTo);
            return_string += @"\target\debug\deps\";

            return return_string;
        }
    }
}
