﻿using System;
using System.Text.RegularExpressions;

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