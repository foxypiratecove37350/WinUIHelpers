using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUIHelpers.Windowing
{
    [TypeConverter(typeof(WindowPositionTypeConverter))]
    public struct WindowPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static WindowPosition Default => new WindowPosition(-1, 0);
        public static WindowPosition Center => new WindowPosition(-1, -1);

        public WindowPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            if (X == -1 && Y == 0)
            {
                return "Default";
            }
            else if (X == -1 && Y == -1)
            {
                return "Center";
            }

            return $"{X}, {Y}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is WindowPosition other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(WindowPosition a, WindowPosition b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(WindowPosition a, WindowPosition b)
        {
            return !(a == b);
        }
    }

    public class WindowPositionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }
        public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            if (value is string s)
            {
                if (s.Equals("Default", StringComparison.OrdinalIgnoreCase))
                {
                    return WindowPosition.Default;
                }
                else if (s.Equals("Center", StringComparison.OrdinalIgnoreCase))
                {
                    return WindowPosition.Center;
                }
                else
                {
                    string[] parts = s.Split(',');
                    if (parts.Length == 2
                        && int.TryParse(parts[0].Trim(), out int x)
                        && int.TryParse(parts[1].Trim(), out int y))
                    {
                        return new WindowPosition(x, y);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
