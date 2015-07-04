using System;
using System.Windows.Data;

namespace GW2Trader.Desktop.Util
{
    [ValueConversion(typeof(int), typeof(int))]
    public class MoneyToSilverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            int price = (int) value;

            if (Math.Abs(price) > 10000)
            {
                price = Math.Abs(price);
            }

            price = price % 10000;
            price = price / 100;
            return price;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
