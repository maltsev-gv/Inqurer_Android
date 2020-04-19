using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace InquirerForAndroid.Services
{
    public interface IVisualService
    {
        double MeasureTextSize(string text, double width, double fontSize, string fontName = null);
        T Cast<T>(Java.Lang.Object obj) where T : class;
        List<T> GetListViewVisibleItems<T>(Xamarin.Forms.ListView lv) where T : class;
        void ScrollListViewTo(Xamarin.Forms.ListView lv, int x, int y);
        void SignToListViewScrollEvent(Xamarin.Forms.ListView lv, Action<Point> OnScrollMethod);
    }
}