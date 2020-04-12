using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Inquirer.Droid.Services;
using InquirerForAndroid.Services;

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
	}
}