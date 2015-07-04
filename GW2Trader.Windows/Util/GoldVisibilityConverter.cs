using System;
using System.Windows;
using System.Windows.Data;

namespace GW2Trader.Desktop.Util
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class GoldVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int val = (int)value;
            return (Math.Abs(val) > 9999)? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
