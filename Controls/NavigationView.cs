using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace WinUIHelpers.Controls
{
    public class NavigationView : Microsoft.UI.Xaml.Controls.NavigationView
    {
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(
                nameof(To),
                typeof(Frame),
                typeof(SelectorBar),
                new PropertyMetadata(null)
            );
        public Frame To
        {
            get => (Frame)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public NavigationView()
        {
            SelectionChanged += OnSelectionChanged_WhenToIsSet;
        }

        private void OnSelectionChanged_WhenToIsSet(
            Microsoft.UI.Xaml.Controls.NavigationView s,
            NavigationViewSelectionChangedEventArgs e
        )
        {
            if (To == null)
            {
                return;
            }

            NavigationView sender = (NavigationView)s;
            SelectorBarItem selectedItem = (SelectorBarItem)sender.SelectedItem;
            int currentSelectedIndex = sender.MenuItems.IndexOf(selectedItem);

            if (sender.To != null && selectedItem != null && selectedItem.To != null)
            {
                _ = sender.To.Navigate(
                    selectedItem.To,
                    null,
                    new SlideNavigationTransitionInfo { Effect = SlideNavigationTransitionEffect.FromBottom }
                );
            }
        }
    }
}
