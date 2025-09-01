using Microsoft.UI.Xaml;
using System;

namespace WinUIHelpers.Controls
{
    public sealed class SelectorBarItem : Microsoft.UI.Xaml.Controls.SelectorBarItem
    {
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(
                nameof(To),
                typeof(Type),
                typeof(SelectorBarItem),
                new PropertyMetadata(null)
            );
        public Type To
        {
            get => (Type)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public SelectorBarItem() { }
    }
}
