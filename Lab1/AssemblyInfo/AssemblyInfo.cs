using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfo
{
    class AssemblyInfo
    {
        public static void CheckPathArg(string[] args, out string path)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Wrong number of parameters. Usage: path to assembly.");
            }
            path = args[0];
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File {0} does not exist.", path);
            }
            string ext = Path.GetExtension(path);
            if (!(ext == ".exe" || ext == ".dll"))
            {
                throw new FormatException("Unknown extension: " + ext + ". Files with .exe and .dll extensions expected.");
            }
        }

        public static string[] LoadandListPublicTypes(string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            var pubTypes = assembly.GetTypes().Where(type => type.IsPublic).OrderBy(type => type.Namespace + type.Name);
            string[] typesNames = new string[pubTypes.Count()];
            int i = 0;
            foreach (var type in pubTypes)
            {
                typesNames[i] = type.FullName;
                i++;
            }
            return typesNames;
        }
    }
}
}
