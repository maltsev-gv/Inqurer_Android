using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InquirerForAndroid.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchEntry : ContentView
    {
        public SearchEntry()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SearchEntry), null, BindingMode.TwoWay);

        public string Prompt
        {
            get => (string)GetValue(PromptProperty);
            set => SetValue(PromptProperty, value);
        }

        public static readonly BindableProperty PromptProperty =
            BindableProperty.Create(nameof(Prompt), typeof(string), typeof(SearchEntry), null, BindingMode.TwoWay);

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public static readonly BindableProperty SearchCommandProperty =
            BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(SearchEntry));

        public object SearchCommandParameter
        {
            get => (object)GetValue(SearchCommandParameterProperty);
            set => SetValue(SearchCommandParameterProperty, value);
        }

        public static readonly BindableProperty SearchCommandParameterProperty =
            BindableProperty.Create(nameof(SearchCommandParameter), typeof(object), typeof(SearchEntry));

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(SearchEntry), Keyboard.Text, BindingMode.TwoWay, OnKeyboardChanged);

        private static bool OnKeyboardChanged(BindableObject bindable, object value)
        {
            return true;
        }

        private void ImageButton_OnPressed(object sender, EventArgs e)
        {
            var pos = entry.CursorPosition;
            entry.IsPassword = false;
            entry.CursorPosition = pos;
        }
    }
}
