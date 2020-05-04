using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class WrapperViewModel : ViewModelBase
    {
        public ContentView ActiveView
        {
            get => GetVal<ContentView>();
            set => SetVal(value);
        }
    }
}
