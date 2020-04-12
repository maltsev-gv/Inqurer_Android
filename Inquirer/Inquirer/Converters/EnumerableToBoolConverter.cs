using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace InquirerForAndroid.Converters
{
    public class EnumerableToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int minimumCount = 0;
            bool isReverse = false;
            if (parameter is string strPar)
            {
                strPar = strPar.ToLower();
                isReverse = strPar.Contains("r");
                int.TryParse(strPar.Replace("r", ""), out minimumCount);
            }

            var res = false;
            if (value is IEnumerable<object> enumerable)
            {
                res = enumerable.Count() > minimumCount;
            }

            return isReverse ? !res : res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
