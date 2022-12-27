using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Service
{
    public static class FileManager
    {
        public static string GetPath()
        {
            if (Directory.Exists(@"Q:\CriminalCases\"))
            {
                return @"Q:\CriminalCases\";
            }

            if (Directory.Exists(@"C:\CriminalCases\"))
            {
                return @"C:\CriminalCases\";
            }

            if (Directory.Exists(@"D:\CriminalCases\"))
            {
                return @"D:\CriminalCases\";
            }

            throw new Exception("Folder not exist");
        }

        public static string GetPath(string _path)
        {
            return Path.Combine(GetPath(), _path);
        }

        public static void OpenFolder(string _number)
        {
            Process.Start("explorer.exe", GetPath(_number));
        }

        public static void CreateNewDirectory(string _case)
        {
            if (!Directory.Exists(Path.Combine(FileManager.GetPath(), _case)))
            {
                Directory.CreateDirectory(Path.Combine(FileManager.GetPath(), _case));
            }
        }
        public static void DeleteDirectory(string _case)
        {
            if (Directory.Exists(Path.Combine(FileManager.GetPath(), _case)))
            {
                Directory.Delete(Path.Combine(FileManager.GetPath(), _case), true);
            }
        }
    }
}
