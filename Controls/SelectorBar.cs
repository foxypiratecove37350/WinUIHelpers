using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace WinUIHelpers.Controls
{
    public sealed class SelectorBar : Microsoft.UI.Xaml.Controls.SelectorBar
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
        private int _previousSelectedIndex = 0;

        public SelectorBar()
        {
            SelectionChanged += OnSelectionChanged_WhenToIsSet;
        }

        private void OnSelectionChanged_WhenToIsSet(
            Microsoft.UI.Xaml.Controls.SelectorBar s,
            SelectorBarSelectionChangedEventArgs e
        )
        {
            if (To == null)
            {
                return;
            }

            SelectorBar sender = (SelectorBar)s;
            SelectorBarItem selectedItem = (SelectorBarItem)sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);

            if (sender.To != null && selectedItem != null && selectedItem.To != null)
            {
                SlideNavigationTransitionEffect slideNavigationTransitionEffect =
                    currentSelectedIndex - _previousSelectedIndex > 0
                    ? SlideNavigationTransitionEffect.FromRight
                    : SlideNavigationTransitionEffect.FromLeft;

                _ = sender.To.Navigate(
                    selectedItem.To,
                    null,
                    new SlideNavigationTransitionInfo { Effect = slideNavigationTransitionEffect }
                );

                _previousSelectedIndex = currentSelectedIndex;
            }
        }
    }
}
