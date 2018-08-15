using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backuptool
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("please enter 2 arguments.");
                Console.WriteLine("backuptool.exe SOURCE_DIR DEST_DIR");
                return;
            }

            

            int arg_len = args.Length;
            

            string source_dir = args[0];
            string dst_dir = args[1];

            Console.WriteLine("FFFFrom ***** Source Directory: {0}", source_dir);
            Console.WriteLine("Toooooo ***** Dest Directory: {0}", dst_dir);

            Program backuptool = new Program();
            backuptool.Backup(source_dir, dst_dir);


   

            
        }

        public void Backup(string src, string dst)
        {
            DirectoryInfo srcdirinfo = new DirectoryInfo(src);
            DirectoryInfo destdirinfo = new DirectoryInfo(src);
            var files = srcdirinfo.EnumerateFiles();
            foreach(var fi in files)
            {
                FileInfo srcfileinfo = new FileInfo(fi.Name);
                FileInfo dstFileInfo = null;
                string srcfilename = src + @"\" + fi.Name;
                string dstfilename = dst + @"\" + fi.Name;
                bool bExist = File.Exists(dstfilename);
                if (bExist)
                {
                    dstFileInfo = new FileInfo(dstfilename);
                 //   Console.WriteLine("{0} lastWriteTime is {1}", srcfilename, File.GetLastWriteTime(srcfilename));
                 //   Console.WriteLine("{0} lastWriteTime is {1}", dstfilename, File.GetLastWriteTime(dstfilename));
                    int iret = DateTime.Compare(File.GetLastWriteTime(srcfilename), File.GetLastWriteTime(dstfilename));
                    if (iret > 0)
                    {
                        File.Copy(srcfilename, dstfilename, true);
                        Console.WriteLine("--> Copy {0} to {1}", srcfilename, dstfilename);
                    }
                    else
                    {
                        Console.WriteLine("{0} don't need update.", srcfilename);
                    }
                }
                else {

                    File.Copy(srcfilename, dstfilename);
                    Console.WriteLine("Copy {0} to {1}", srcfilename, dstfilename);
                }

            }

        }

        public void CompareTime(DateTime t1, DateTime t2)
        {

        }

        public void ShowDir(string dirPath)
        {
            Console.WriteLine("##### In {0} #####", dirPath);
            DirectoryInfo dirinfo = new DirectoryInfo(dirPath);
            var dirs = from dir in dirinfo.EnumerateDirectories() select new { dir };
            foreach(var di in dirs)
            {
                Console.WriteLine("Dir->{0}", di.dir.Name);
            }

            foreach(var fi in dirinfo.EnumerateFiles())
            {
                Console.WriteLine("File->{0}", fi.Name);
            }
        }
    }
}
