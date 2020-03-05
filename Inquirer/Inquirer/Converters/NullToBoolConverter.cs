using System;
using System.Globalization;
using Xamarin.Forms;

namespace XMobileGarage.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = value != null;
            if (parameter is string strParam && strParam.ToLower().StartsWith("r"))
            {
                return !result;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
