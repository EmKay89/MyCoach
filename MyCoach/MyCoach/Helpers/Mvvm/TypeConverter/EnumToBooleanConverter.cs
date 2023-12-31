﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MyCoach.Helpers.Mvvm.TypeConverter
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterString)
                || Enum.IsDefined(value.GetType(), value) == false)
            {
                return DependencyProperty.UnsetValue;
            }

            var parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterString)
                || Enum.IsDefined(targetType, parameterString) == false)
            {
                return DependencyProperty.UnsetValue;
            }

            if (value as bool? == false)
            {
                return Binding.DoNothing;
            }

            return Enum.Parse(targetType, parameterString);
        }
    }
}
