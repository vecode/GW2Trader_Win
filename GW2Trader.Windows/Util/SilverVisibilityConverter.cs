using System;
using System.Windows;
using System.Windows.Data;

namespace GW2Trader.Desktop.Util
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class SilverVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int price = (int)value;
            return (Math.Abs(price) > 99)? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
