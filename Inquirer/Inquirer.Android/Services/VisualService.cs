using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Inquirer.Droid.Services;
using InquirerForAndroid.Services;
using Point = Xamarin.Forms.Point;

[assembly: Xamarin.Forms.Dependency(typeof(VisualService))]
namespace Inquirer.Droid.Services
{
    public class VisualService : IVisualService
    {
        private Typeface _textTypeface;

        public double MeasureTextSize(string text, double width, double fontSize, string fontName = null)
        {
            var textView = new TextView(Android.App.Application.Context)
            {
                Typeface = GetTypeface(fontName)
            };
            textView.SetText(text, TextView.BufferType.Normal);
            textView.SetTextSize(ComplexUnitType.Px, (float)fontSize);

            int widthMeasureSpec = View.MeasureSpec.MakeMeasureSpec((int)width, MeasureSpecMode.AtMost);
            int heightMeasureSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);

            textView.Measure(widthMeasureSpec, heightMeasureSpec);

            return (double)textView.MeasuredHeight;
        }

        private Typeface GetTypeface(string fontName)
        {
            if (fontName == null)
            {
                return Typeface.Default;
            }

            if (_textTypeface == null)
            {
                _textTypeface = Typeface.Create(fontName, TypefaceStyle.Normal);
            }

            return _textTypeface;
        }

        //need to cast the JavaObject to the desired C# class
        public T Cast<T>(Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }

        public void ScrollListViewTo(Xamarin.Forms.ListView lv, int x, int y)
        {
            Xamarin.Forms.Platform.Android.ListViewRenderer renderer =
                (Xamarin.Forms.Platform.Android.ListViewRenderer)Xamarin.Forms.Platform.Android.Platform.GetRenderer(lv);

            ListView nativeListView = renderer.Control;
            nativeListView.ScrollTo(x, y);
        }

        public List<T> GetListViewVisibleItems<T>(Xamarin.Forms.ListView lv) where T : class
        {
            List<T> visibleItems = new List<T>();

            Xamarin.Forms.Platform.Android.ListViewRenderer renderer =
                (Xamarin.Forms.Platform.Android.ListViewRenderer)Xamarin.Forms.Platform.Android.Platform.GetRenderer(lv);

            ListView nativeListView = renderer.Control;

            for (int i = 0; i < nativeListView.ChildCount; i++)
            {
                View view = nativeListView.GetChildAt(i);

                if (view.Visibility != ViewStates.Visible)
                    continue;

                int pos = nativeListView.GetPositionForView(view);

                if (pos < 0 || pos == AdapterView.InvalidPosition || pos >= nativeListView.Adapter.Count)
                    continue;

                var obj = nativeListView.Adapter.GetItem(pos);

                if (obj == null)
                    continue;

                T visibleElement = Cast<T>(obj);

                if (visibleElement != null && !visibleItems.Contains(visibleElement))
                    visibleItems.Add(visibleElement);
            }
            return visibleItems;
        }
    }
}