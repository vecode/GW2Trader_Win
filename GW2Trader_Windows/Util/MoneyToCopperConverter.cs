using System;
using System.Windows.Data;

namespace GW2Trader_Windows.Util
{
    public class MoneyToCopperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int price = (int) value;

            if (Math.Abs(price) >= 100)
            {
                price = Math.Abs(price);
            }            
            return (price) % 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
