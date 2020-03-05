using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inquirer_Android.Converters
{
    public class ConverterBase : IMarkupExtension, IValueConverter
    {
        public virtual object ProvideValue(IServiceProvider serviceProvider)
        {
            return null;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException($"{this.GetType()}: {nameof(Convert)}");
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException($"{this.GetType()}: {nameof(ConvertBack)}");
        }
    }
}
