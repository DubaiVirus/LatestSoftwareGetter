﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace LatestSoftwareGetter.Converters
{
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool original = (bool?)value == true;
            return !original;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool original = (bool?)value == true;
            return !original;
        }
    }
}
