using System.Windows.Input;
using Rcn.Common;
using Xamarin.Forms;

namespace InquirerForAndroid.Models
{
    public class MenuItemInfo : ObservableObject
    {
        public string Text
        {
            get => GetVal<string>();
            set => SetVal(value);
        }

        public ImageSource ImageSource
        {
            get => GetVal<ImageSource>();
            set => SetVal(value);
        }

        public ICommand Command
        {
            get => GetVal<ICommand>();
            set => SetVal(value);
        }

        public object CommandParameter
        {
            get => GetVal<object>();
            set => SetVal(value);
        }
    }
}
