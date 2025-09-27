using MovingCursor.Core;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;

namespace MovingCursor.Utilities
{
    internal class PointConverter : ConfigurationConverterBase
    {
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object data)
        {
            string? value = data as string;
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");
            string[] parts = value.Split(",");
            if (parts.Length != 2)
                throw new FormatException("Точки должны быть в формате 'X, Y'");
            if (int.TryParse(parts[0].Trim(), out int x) &&
                int.TryParse(parts[1].Trim(), out int y))
            {
                if (x < MonitorBounds.Left || x > MonitorBounds.Right ||
                    y < MonitorBounds.Top || y > MonitorBounds.Bottom)
                    throw new ArithmeticException($"Точка ({x}, {y}) выходит за пределы экрана монитора {MonitorBounds.Right}x{MonitorBounds.Bottom}");

                return new Point(x, y);
            }

            throw new FormatException("Точки должны быть в формате 'X, Y'");
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is Point point)
                return $"{point.X}, {point.Y}";

            throw new FormatException("Точки должны быть в формате 'X, Y'");
        }
    }
}
