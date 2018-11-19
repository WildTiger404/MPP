using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Projects\git\labs\dotnetlab-2018q3\multi-layer architecture\Task\MLA_task\packages\SimpleInjector.4.4.0\lib\net45\SimpleInjector.dll";
            /*try
            {
                AssemblyInfo.CheckPathArg(args, out path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType() +": "+ e.Message);
                Console.ReadLine();
                return;
            }*/
            try
            {
                string[] typesList = AssemblyInfo.LoadandListPublicTypes(path);
                foreach (string type in typesList)
                {
                    Console.WriteLine(type);
                }
            }
            catch (FileLoadException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
