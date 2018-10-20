using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileCopier fileCopier = new FileCopier(5);
            string source_dir, target_dir;
            try
            {
                FileCopier.CheckDirNames(args, out source_dir, out target_dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            int f_counter = 0;
            f_counter = fileCopier.CopyFiles(source_dir, target_dir);
            Console.WriteLine(f_counter + " files copied");
            Console.ReadLine();
        }
    }
}
