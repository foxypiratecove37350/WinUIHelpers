using Microsoft.UI.Xaml;
using System;

namespace WinUIHelpers.Controls
{
    public sealed class NavigationViewItem : Microsoft.UI.Xaml.Controls.NavigationViewItem
    {
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(
                nameof(To),
                typeof(Type),
                typeof(NavigationViewItem),
                new PropertyMetadata(null)
            );
        public Type To
        {
            get => (Type)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public NavigationViewItem() { }
    }
}
