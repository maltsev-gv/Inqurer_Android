using System.Windows.Input;
using Xamarin.Forms;

namespace InquirerForAndroid.UserControls
{
    public partial class ContentButton : ContentView
    {
        private readonly TapGestureRecognizer _tapGestureRecognizer;

        public ContentButton()
        {
            _tapGestureRecognizer = new TapGestureRecognizer();
            GestureRecognizers.Add(_tapGestureRecognizer);
        }

        //protected override void OnChildAdded(Element child)
        //{
        //    base.OnChildAdded(child);
        //    if (child is View childview)
        //    {
        //        childview.GestureRecognizers.Add(_tapGestureRecognizer);
        //    }
        //}

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand),
            typeof(ContentButton), null, BindingMode.Default, null, CommandPropertyChanged);

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is ICommand command && bindable is ContentButton contentButton)
            {
                contentButton._tapGestureRecognizer.Command = command;
            }
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object),
            typeof(ContentButton), null, BindingMode.Default, null, CommandParameterPropertyChanged);

        private static void CommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ContentButton contentButton)
            {
                contentButton._tapGestureRecognizer.CommandParameter = newValue;
            }
        }

        public ICommand CommandParameter
        {
            get => (ICommand)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

    }
}