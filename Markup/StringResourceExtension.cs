using Microsoft.UI.Xaml.Markup;
using System;
using Windows.ApplicationModel.Resources;

namespace WinUIHelpers.Markup
{
    /// <summary>
    /// A markup extension to use strings from resource files
    /// </summary>
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    [ContentProperty(Name = nameof(Name))]
    public sealed partial class StringResourceExtension : MarkupExtension
    {
        public string? Name { get; set; }

        protected override object ProvideValue()
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }

            return StringResources.GetString(Name);
        }

        public StringResourceExtension() { }
    }

    public static class StringResources
    {
        private static readonly ResourceLoader resourceLoader = ResourceLoader.GetForViewIndependentUse();

        public static string GetString(string name)
        {
            return resourceLoader.GetString(name);
        }
    }
}
