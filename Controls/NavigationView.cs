using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace WinUIHelpers.Controls
{
    public class NavigationView : Microsoft.UI.Xaml.Controls.NavigationView
    {
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(
                nameof(To),
                typeof(Frame),
                typeof(NavigationView),
                new PropertyMetadata(null)
            );
        public Frame To
        {
            get => (Frame)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static readonly DependencyProperty SettingsPageProperty =
            DependencyProperty.Register(
                nameof(SettingsPage),
                typeof(Type),
                typeof(NavigationView),
                new PropertyMetadata(null)
            );
        public Type SettingsPage
        {
            get => (Type)GetValue(SettingsPageProperty);
            set => SetValue(SettingsPageProperty, value);
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

            if (e.IsSettingsSelected && sender.To != null && SettingsPage != null)
            {
                sender.To.Navigate(
                    SettingsPage,
                    null,
                    new SlideNavigationTransitionInfo { Effect = SlideNavigationTransitionEffect.FromBottom }
                );
                return;
            }

            NavigationViewItem selectedItem = (NavigationViewItem)sender.SelectedItem;

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
