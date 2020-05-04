namespace InquirerForAndroid.Views
{
    public interface IScrollableView
    {
        bool IsScrolled { get; set; }
        void HideContent();
        void ShowContent();
    }
}
