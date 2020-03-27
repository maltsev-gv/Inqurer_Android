using System;
using System.Globalization;
using InquirerForAndroid.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InquirerForAndroid.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue && bool.TryParse(strValue, out var result))
            {
                if (parameter is string strParameter && strParameter.ToLower().StartsWith("r"))
                {
                    return !result;
                }
                return result;
            }
            throw new ArgumentOutOfRangeException(
                $"{nameof(StringToBoolConverter)}: value is not valid bool {value}");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
