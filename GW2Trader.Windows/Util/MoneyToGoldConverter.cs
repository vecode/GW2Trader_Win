﻿using System;
using System.Windows.Data;

namespace GW2Trader.Desktop.Util
{
    [ValueConversion(typeof(int), typeof(int))]
    public class MoneyToGoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int result = (int)value / 10000;

            return result;
            //return (int) value/10000;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
