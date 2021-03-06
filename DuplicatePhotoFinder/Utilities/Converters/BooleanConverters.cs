﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace DuplicatePhotoFinder
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class OppositeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, false))
                return true;
            if (Equals(value, true))
                return false;
            return null;
        }
    }
}
