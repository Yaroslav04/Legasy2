using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Service
{
    public static class Migrate2
    {
        public static List<CaseMClass> LoadData()
        {
            List<CaseMClass> list = new List<CaseMClass>();
            var directories = Directory.GetDirectories(@"Q:\LegasyDB\CriminalCases\");
            if (directories.Length > 0)
            {
                foreach (var folder in directories)
                {
                    CaseMClass caseClass = new CaseMClass();
                    caseClass.Name = folder.Replace(@"Q:\LegasyDB\CriminalCases\", "").Replace("\\", "");
                    caseClass.Path = folder;
                    caseClass.Decsription = GetDescriptionFromCase(folder);
                    list.Add(caseClass);
                }              
            }

            return list;
        }

        public static DescriptionClass GetDescriptionFromCase(string _path)
        {
            DescriptionClass descriptionClass = new DescriptionClass();

            foreach (var directory in Directory.GetDirectories(_path))
            {
                if (directory.Replace(_path, "") == @"\Data")
                {
                    if (File.Exists(Path.Combine(_path, "Data", "description.ini")))
                    {
                        using (StreamReader sr = new StreamReader(Path.Combine(_path, "Data", "description.ini")))
                        {
                            return ConvertTextToDescription(sr.ReadToEnd());
                        }
                    }
                }
                else
                {
                    return descriptionClass;
                }
            }

            return null;
        }

        public static DescriptionClass ConvertTextToDescription(string _text)
        {
            DescriptionClass _descriptionClass = new DescriptionClass();
            var array = _text.Split("\t");
            if (array.Length >= 2)
            {
                _descriptionClass.Header = array[0];
                _descriptionClass.Qualification = array[1];
            }
            return _descriptionClass;
        }
    }

    public class CaseMClass : IEquatable<CaseMClass>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DescriptionClass Decsription { get; set; }

        public CaseMClass()
        {
            Name = "";
            Path = "";
            Decsription = new DescriptionClass();
        }

        public bool Equals(CaseMClass ersr)
        {
            //Check whether the compared object is null.  
            if (Object.ReferenceEquals(ersr, null)) return false;

            //Check whether the compared object references the same data.  
            if (Object.ReferenceEquals(this, ersr)) return true;

            //Check whether the UserDetails' properties are equal.  
            return Name.Equals(ersr.Name);
        }

        // If Equals() returns true for a pair of objects   
        // then GetHashCode() must return the same value for these objects.  

        public override int GetHashCode()
        {

            //Get hash code for the UserName field if it is not null.  
            int hashN = Name == null ? 0 : Name.GetHashCode();

            //Calculate the hash code for the GPOPolicy.  
            return hashN;
        }
    }

    public class DescriptionClass
    {
        public string Header { get; set; }
        public string Qualification { get; set; }

        public DescriptionClass()
        {
            Header = "";
            Qualification = "";
        }
    }


}
