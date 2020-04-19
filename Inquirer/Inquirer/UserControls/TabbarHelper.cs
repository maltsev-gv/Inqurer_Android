using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace InquirerForAndroid.UserControls
{
    public class TabBarHelper
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.CreateAttached(
                propertyName: "IsVisible",
                returnType: typeof(bool?),
                declaringType: typeof(ShellContent),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnIsVisibleChanged);

        public static bool GetIsVisible(BindableObject shellContent)
        {
            return (bool)shellContent.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(BindableObject shellContent, bool value)
        {
            shellContent.SetValue(IsVisibleProperty, value);
        }

        private static void OnIsVisibleChanged(BindableObject shellContent, object oldValue, object newValue)
        {
            var sContent = (ShellContent) shellContent;
            if (!_shellContents.ContainsKey(sContent))
            {
                _shellContents[sContent] = new ShellContentInfo(sContent);
            }

            _shellContents[sContent].IsVisible = (bool) newValue;
        }

        private static Dictionary<ShellContent, ShellContentInfo> _shellContents = new Dictionary<ShellContent, ShellContentInfo>();

        public static readonly BindableProperty RegisterTabBarProperty =
            BindableProperty.CreateAttached(
                propertyName: "RegisterTabBar",
                returnType: typeof(bool),
                declaringType: typeof(TabBar),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnRegisterTabBarChanged);

        public static bool GetRegisterTabBar(BindableObject tabBar)
        {
            return (bool)tabBar.GetValue(RegisterTabBarProperty);
        }

        public static void SetRegisterTabBar(BindableObject tabBar, bool value)
        {
            tabBar.SetValue(RegisterTabBarProperty, value);
        }

        private static bool _isInnerOperation = false;
        private static void OnRegisterTabBarChanged(BindableObject tabBar, object oldValue, object newValue)
        {
            var tBar = (TabBar)tabBar;

            tBar.DescendantAdded += (s, e) =>
            {
                if (_isInnerOperation)
                {
                    return;
                }

                if (e.Element.Parent is ShellContent shellContent)
                {
                    if (!_shellContents.ContainsKey(shellContent))
                    {
                        _shellContents[shellContent] = new ShellContentInfo(shellContent);
                    }

                    if (_shellContents[shellContent].TabBar == null)
                    {
                        _shellContents[shellContent].FindParents();
                    }
                }
                //_tabBars[tBar].Add((ShellSection) e.Element);

                _isInnerOperation = true;
                //    tBar.Items = _tabBars[tBar].Where(shellSection => !(shellSection is ShellContent ) || !_shellVisibilities.ContainsKey((ShellContent)shellSection))


                    _isInnerOperation = false;
            };
        }

        private class ShellContentInfo
        {
            public ShellContentInfo(ShellContent shellContent)
            {
                ShellContent = shellContent;
            }

            public TabBar TabBar { get; set; }
            public ShellSection ShellSection { get; set; }
            public int ShellSectionOrder { get; set; }
            public ShellContent ShellContent { get; set; }
            public int ShellContentOrder { get; set; }
            public bool IsVisible { get; set; }

            public void FindParents()
            {
                if (ShellContent?.Parent is ShellSection shellSection)
                {
                    ShellSection = shellSection;
                    ShellContentOrder = shellSection.Items.IndexOf(ShellContent);
                }

                if (ShellSection?.Parent is TabBar tabBar)
                {
                    TabBar = tabBar;
                    ShellSectionOrder = tabBar.Items.IndexOf(ShellSection);
                }
            }
        }

    }
}
