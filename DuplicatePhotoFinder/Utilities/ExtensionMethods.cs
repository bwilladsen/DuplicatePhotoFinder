using System;
using System.Text.RegularExpressions;

namespace DuplicatePhotoFinder
{
    public static class ExtensionMethods
    {
        public static bool IsValidPath(this String path)
        {
            return Regex.IsMatch(path, @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");
        }
    }
}
