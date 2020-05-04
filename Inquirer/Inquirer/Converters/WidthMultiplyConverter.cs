using System;
using System.Globalization;
using Xamarin.Forms;

namespace InquirerForAndroid.Converters
{
    public class WidthMultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int marginLevel))
            {
                throw new ArgumentException($"{nameof(WidthMultiplyConverter)}: value must be int");
            }
            
            if (!(parameter is string strPar) || !int.TryParse(strPar, out var pixels))
            {
                throw new ArgumentException($"{nameof(WidthMultiplyConverter)}: parameter must be int");
            }

            return marginLevel * pixels;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
