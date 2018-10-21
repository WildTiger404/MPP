using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class FileCopier
    {
        private static TaskQueue _queue;
        public FileCopier(int maxThreads)
        {
            _queue = new TaskQueue(maxThreads);
        }

        public static void CheckDirNames(string[] args, out string source, out string dest)
        {
            if (args.Length < 2)
            {
                throw new Exception("Not enough arguments (" + args.Length + "). Usage: source directory name, target directory name.");
            }
            source = args[0];
            dest = args[1];
            if (source == dest)
            {
                throw new Exception("Source and destination directories should not coincide.");
            }
            if (!Directory.Exists(source))
            {
                throw new Exception("Source directory does not exist.");
            }
        }

        public int CopyFiles(string source_dir, string target_dir)
        {
            if (!Directory.Exists(target_dir))
            {
                Directory.CreateDirectory(target_dir);
            }
            List<TaskQueue.taskDelegate> list = new List<TaskQueue.taskDelegate>();
            string[] files_names = Directory.GetFiles(source_dir);
            List<FileInfo> fileInfos = new List<FileInfo>();
            string[] dirs_names = Directory.GetDirectories(source_dir);
            foreach (string file_name in files_names)
            {
                FileInfo file = new FileInfo(file_name);
                list.Add(() =>
                {
                    file.CopyTo(target_dir + file.Name, true);
                    Console.Write("{0} is copied ", file.Name);
                });

            }
            
            int files_num = files_names.Length;
            
            foreach (string dir in dirs_names)
            {
                string newsource_dir = Path.Combine(source_dir, dir);
                files_num += CopyFiles(newsource_dir, newsource_dir.Replace(source_dir, target_dir));
            }
            Parallel.WaitAll(list);
            return files_num;
        }
    }
}
