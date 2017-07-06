using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DuplicatePhotoFinder
{
    public static class ExtensionMethods
    {
        public static bool IsValidPath(this String path)
        {
            // TODO:  Implement a better regex for path matching.
            return Regex.IsMatch(path, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");
        }
    }
}
