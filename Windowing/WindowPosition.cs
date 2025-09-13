using System;

namespace WinUIHelpers.Windowing
{
    public struct WindowPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public static WindowPosition Default => new(-1, 0);
        public static WindowPosition Center => new(-1, -1);

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
            if (this == WindowPosition.Default)
            {
                return "Default";
            }
            else if (this == WindowPosition.Center)
            {
                return "Center";
            }

            return $"{X},{Y}";
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

    public enum WindowPositionSpecialValues
    {
        Default,
        Center
    }
}
