using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Service
{
    public class Migrate
    {
        public List<string> GetCriminalNumberList()
        {
            List<string> list = new List<string>();
            var folders = new List<string>(Directory.GetDirectories(FileManager.GetPath()));
            folders = folders.Where(x => x != "Data").ToList();
            return folders;
        }

        public string GetDescription(string _path)
        {
            string result = "";
            string path = Path.Combine(FileManager.GetPath(), _path);
            path = Path.Combine(path, "Data/description.ini");
            using(StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadLine();
            }
            return result;
        }

        public void FolderCopy(string _path)
        {
            string path = Path.Combine(FileManager.GetPath(), _path);
            path = Path.Combine(path, "Data");
            var folders = Directory.GetDirectories(path);
            foreach(var folder in folders)
            {
                var files = new List<string>(Directory.GetFiles(folder));
                if (files.Count > 0)
                {
                    foreach(var file in files)
                    {
                        try
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            File.Copy(file, Path.Combine(Path.Combine(FileManager.GetPath(), _path), fileInfo.Name));
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }

        public void FolderDelete(string _path)
        {
            string path = Path.Combine(FileManager.GetPath(), _path);
            path = Path.Combine(path, "Data");
            try
            {
                Directory.Delete(path, true);
            }
            catch
            {

            }
        }
    }
}
