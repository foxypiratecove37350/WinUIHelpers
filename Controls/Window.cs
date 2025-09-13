using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using Windows.ApplicationModel;
using Windows.Graphics;
using WinUIHelpers.Windowing;

namespace WinUIHelpers.Controls
{
    /// <summary>
    /// A custom Window with a title bar and content area
    /// </summary>
    public class Window : Microsoft.UI.Xaml.Window
    {
        private readonly Grid _grid;
        private readonly ContentControl _contentControl;

        public readonly TitleBar TitleBar;
        public static string AppVersion
        {
            get
            {
                PackageVersion version = Package.Current.Id.Version;
                return $"{version.Major}.{version.Minor}.{version.Build}"
                    + (version.Revision != 0 ? $".{version.Revision}" : "");
            }
        }
        public new UIElement Content
        {
            get => base.Content; set => _contentControl.Content = value;
        }

        public new string Title
        {
            get => base.Title;
            set
            {
                base.Title = value;
                TitleBar.Title = value;
            }
        }
        public int Height
        {
            get => AppWindow.Size.Height; set => AppWindow.Resize(new SizeInt32(Width, value));
        }
        public int Width
        {
            get => AppWindow.Size.Width; set => AppWindow.Resize(new SizeInt32(value, Height));
        }

        public Uri IconUri { get; set; } = new Uri("ms-appx:///Images/StoreLogo.png");

        public WindowPosition Position
        {
            get
            {
                PointInt32 position = AppWindow.Position;
                return new WindowPosition(position.X, position.Y);
            }
            set
            {
                var (x, y) = value;
                if (value == WindowPosition.Center)
                {
                    RectInt32 workArea = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Nearest).WorkArea;
                    x = (workArea.Width - Width) / 2;
                    y = (workArea.Height - Height) / 2;
                }
                else if (value == WindowPosition.Default)
                {
                    return;
                }
                AppWindow.Move(new PointInt32(x, y));
            }
        }

        public WindowPositionSpecialValues StartPosition
        {
            set
            {
                if (value == WindowPositionSpecialValues.Center)
                {
                    Position = WindowPosition.Center;
                }
                else if (value == WindowPositionSpecialValues.Default)
                {
                    Position = WindowPosition.Default;
                }
            }
        }

        public int X
        {
            get => Position.X; set => Position = new WindowPosition(value, Y);
        }

        public int Y
        {
            get => Position.Y; set => Position = new WindowPosition(X, value);
        }

        public Window()
        {
            ExtendsContentIntoTitleBar = true;
            SystemBackdrop = new MicaBackdrop();

            _grid = new Grid();
            // TitleBar
            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            // ContentControl
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            TitleBar = new TitleBar()
            {
                Title = Title,
                Subtitle = "v" + AppVersion,
                IconSource = new ImageIconSource()
                {
                    ImageSource = new BitmapImage(IconUri)
                },
            };

            Grid.SetRow(TitleBar, 0);
            _grid.Children.Add(TitleBar);
            SetTitleBar(TitleBar);

            _contentControl = new ContentControl
            {
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };
            Grid.SetRow(_contentControl, 1);
            _grid.Children.Add(_contentControl);

            base.Content = _grid;
        }
    }
}
