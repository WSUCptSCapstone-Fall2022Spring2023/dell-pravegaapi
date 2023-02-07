using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pravega
{
    public class PathGen
    {
        public string ConfigDLLPath = "";
        public string CreateDllDirectory(string dll_name)
        {
            var cwd = Directory.GetCurrentDirectory();
            string code_base = "Project_Code_Base";
            int indexTo = cwd.IndexOf(code_base);
            string return_string;
            // If IndexOf could not find code_base String
            if (indexTo == -1)
            {
                return "";
            }
            return_string = cwd.Substring(0, indexTo + code_base.Length);
            return_string += @"\cSharpTest\PravegaCSharpLibrary\target\debug\deps\" + dll_name;

            return return_string;
        }

        public void GenerateAllDllPaths()
        {
            ConfigDLLPath = CreateDllDirectory("config_wrapper.dll");
        }
    }
}
