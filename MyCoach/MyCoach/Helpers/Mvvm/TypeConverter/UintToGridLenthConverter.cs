﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyCoach.Helpers.Mvvm.TypeConverter
{
    public class UintToGridLenthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new GridLength((uint)value, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
