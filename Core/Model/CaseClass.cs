using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Model
{
    public class CaseClass
    {
        [AutoIncrement]
        [PrimaryKey]
        [NotNull]
        public int N { get; set; }
        [Indexed(Name = "ListingID", Order = 1, Unique = true)]
        public string CriminalNumber { get; set; }
        public string Header { get; set; }
        public string Qualification { get; set; }
    }
}
