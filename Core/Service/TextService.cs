using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legasy2.Core.Service
{
    public static class TextService
    {
        public static bool IsNumberValid(string _value)
        {
            return Regex.IsMatch(_value, @"^\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d");
        }
    }
}
