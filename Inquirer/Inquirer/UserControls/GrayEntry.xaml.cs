using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inquirer_Android.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrayEntry : ContentView
    {
        public GrayEntry()
        {
            InitializeComponent();
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(GrayEntry), null, BindingMode.TwoWay);

        public string Prompt
        {
            get => (string)GetValue(PromptProperty);
            set => SetValue(PromptProperty, value);
        }

        public static readonly BindableProperty PromptProperty =
            BindableProperty.Create(nameof(Prompt), typeof(string), typeof(GrayEntry), null, BindingMode.TwoWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(GrayEntry), null, BindingMode.TwoWay);

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(GrayEntry), Keyboard.Text, BindingMode.TwoWay, OnKeyboardChanged);

        private static bool OnKeyboardChanged(BindableObject bindable, object value)
        {
            return true;
        }
    }
}
