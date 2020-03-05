using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace XMobileGarage.Converters
{
    public class EnumerableToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int minimumCount = 0;
            if (parameter is string strPar)
            {
                int.TryParse(strPar, out minimumCount);
            }
            if (value is IEnumerable<object> enumerable)
            {
                return enumerable.Count() > minimumCount;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
