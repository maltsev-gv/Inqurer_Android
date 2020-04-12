using Xamarin.Forms;

namespace InquirerForAndroid.Services
{
    public static class TreeExtensions
    {
        public static T GetParentOfType<T>(this Element element) where T : Element
        {
            var parent = element.Parent;
            while (parent != null && parent.GetType() != typeof(T))
            {
                parent = parent.Parent;
            }

            return (T)parent;
        }
    }
}
