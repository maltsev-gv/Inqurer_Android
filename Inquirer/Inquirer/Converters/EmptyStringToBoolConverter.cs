using System;
using System.Globalization;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inquirer_Android.Converters
{
    public class EmptyStringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = value is string strValue && !strValue.IsNullOrEmpty();
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
