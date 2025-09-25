using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Dynamic;
using System.Globalization;

namespace CursorAutoMovement
{
    internal enum CursorMovementMode
    {
        Circle,
        LockScreen,
        Infinity,
        Spiral,
        Triangle,
        Quadrilateral,
        Polygon,
        RandomPoint
    }
    internal class LockScreenModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }

    }

    internal class CircleModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value > 0, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
        [ConfigurationProperty(("radius"), IsRequired = true)]
        public int Radius
        {
            get { return Validator.ValidateInt((int)this["radius"], value => value > 0, "Значение должно быть больше нуля", "radius"); }
            set { this["radius"] = value; }
        }
    }

    internal class InfinityModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value > 0, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
        [ConfigurationProperty(("radius"), IsRequired = true)]
        public int Radius
        {
            get { return Validator.ValidateInt((int)this["radius"], value => value > 0, "Значение должно быть больше нуля", "radius"); }
            set { this["radius"] = value; }
        }
    }

    internal class SpiralModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("startRadius"), IsRequired = true)]
        public int StartRadius
        {
            get { return Validator.ValidateInt((int)this["startRadius"], value => value >= 0, "Значение должно быть больше нуля", "startRadius"); }
            set { this["startRadius"] = value; }
        }
        [ConfigurationProperty(("stepRadius"), IsRequired = true)]
        public int StepRadius
        {
            get { return Validator.ValidateInt((int)this["stepRadius"], value => value > 0, "Значение должно быть больше нуля", "stepRadius"); }
            set { this["stepRadius"] = value; }
        }
        [ConfigurationProperty(("loopCount"), IsRequired = true)]
        public int LoopCount
        {
            get { return Validator.ValidateInt((int)this["loopCount"], value => value > 0, "Значение должно быть больше нуля", "loopCount"); }
            set { this["loopCount"] = value; }
        }
        [ConfigurationProperty(("isLoop"), IsRequired = true)]
        public bool IsLoop
        {
            get { return (bool)this["isLoop"]; }
            set { this["isLoop"] = value; }
        }
        [ConfigurationProperty(("isReverse"), IsRequired = true)]
        public bool IsReverse
        {
            get { return (bool)this["isReverse"]; }
            set { this["isReverse"] = value; }
        }
    }

    internal class TriangleModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value > 0, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
        [ConfigurationProperty(("point1"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point1
        {
            get { return (Point)this["point1"]; }
            set { this["point1"] = value; }
        }
        [ConfigurationProperty(("point2"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point2
        {
            get { return (Point)this["point2"]; }
            set { this["point2"] = value; }
        }
        [ConfigurationProperty(("point3"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point3
        {
            get { return (Point)this["point3"]; }
            set { this["point3"] = value; }
        }
    }

    internal class QuadrilateralModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value > 0, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
        [ConfigurationProperty(("point1"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point1
        {
            get { return (Point)this["point1"]; }
            set { this["point1"] = value; }
        }
        [ConfigurationProperty(("point2"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point2
        {
            get { return (Point)this["point2"]; }
            set { this["point2"] = value; }
        }
        [ConfigurationProperty(("point3"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point3
        {
            get { return (Point)this["point3"]; }
            set { this["point3"] = value; }
        }
        [ConfigurationProperty(("point4"), IsRequired = true)]
        [TypeConverter(typeof(PointConverter))]
        public Point Point4
        {
            get { return (Point)this["point4"]; }
            set { this["point4"] = value; }
        }
    }

    internal class PolygonModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value >= 1, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
        [ConfigurationProperty(("angleCount"), IsRequired = true)]
        public int AngleCount
        {
            get { return Validator.ValidateInt((int)this["angleCount"], value => value >= 3, "Значение должно быть больше двух", "angleCount"); }
            set { this["angleCount"] = value; }
        }
        [ConfigurationProperty(("radius"), IsRequired = true)]
        public int Radius
        {
            get { return Validator.ValidateInt((int)this["radius"], value => value > 0, "Значение должно быть больше нуля", "radius"); }
            set { this["radius"] = value; }
        }
    }

    internal class RandomPointModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }
        [ConfigurationProperty(("step"), IsRequired = true)]
        public int Step
        {
            get { return Validator.ValidateInt((int)this["step"], value => value > 0, "Значение должно быть больше нуля", "step"); }
            set { this["step"] = value; }
        }
    }

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

    internal class Validator
    {
        public static int ValidateInt(object data, Func<int, bool> predicate, string errorMessage, string parametrName)
        {
            if (data == null)
                throw new ArgumentNullException(parametrName, "Не заполнена обязательная переменная.");

            if (data is int value)
            {
                if (!predicate(value))
                    throw new FormatException($"Параметр {parametrName}: {errorMessage}");
                return value;
            }
            else
            {
                throw new InvalidCastException($"Параметр {parametrName}: Значение должно быть целочисленным.");
            }
        }
    }

    internal static class ConfigReader
    {
        private static CursorMovementMode GetCursorMovementMode()
        {
            var exceptionMessage = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var modeString = appSettings.Get("mode");

                if (Enum.TryParse<CursorMovementMode>(modeString, true, out CursorMovementMode parsedMode)
                    && Enum.IsDefined(typeof(CursorMovementMode), parsedMode))
                {
                    return parsedMode;
                }

                exceptionMessage = $"Мод {modeString} не существует в перечислении.";

                throw new Exception(exceptionMessage);
            }
            catch
            {
                exceptionMessage = exceptionMessage == "" ? "Ошибка при чтении App.Config" : exceptionMessage;
                throw new Exception(exceptionMessage);
            }
        }

        public static dynamic ReadConfig()
        {
            dynamic config = new ExpandoObject();
            config.mode = GetCursorMovementMode();

            switch (config.mode)
            {
                case CursorMovementMode.Circle:
                    var circleModeSection = ConfigurationManager.GetSection("CircleMode") as CircleModeSection;

                    if (circleModeSection != null)
                    {
                        config.intervalMs = circleModeSection.IntervalMs;
                        config.step = circleModeSection.Step;
                        config.radius = circleModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.LockScreen:
                    var lockScreenModeSection = ConfigurationManager.GetSection("LockScreenMode") as LockScreenModeSection;

                    if (lockScreenModeSection != null)
                    {
                        config.intervalMs = lockScreenModeSection.IntervalMs;
                    }
                    break;
                case CursorMovementMode.Infinity:
                    var infinityModeSection = ConfigurationManager.GetSection("InfinityMode") as InfinityModeSection;

                    if (infinityModeSection != null)
                    {
                        config.intervalMs = infinityModeSection.IntervalMs;
                        config.step = infinityModeSection.Step;
                        config.radius = infinityModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.Spiral:
                    var spiralModeSection = ConfigurationManager.GetSection("SpiralMode") as SpiralModeSection;

                    if (spiralModeSection != null)
                    {
                        config.intervalMs = spiralModeSection.IntervalMs;
                        config.stepRadius = spiralModeSection.StepRadius;
                        config.startRadius = spiralModeSection.StartRadius;
                        config.loopCount = spiralModeSection.LoopCount;
                        config.isLoop = spiralModeSection.IsLoop;
                        config.isReverse = spiralModeSection.IsReverse;
                    }
                    break;
                case CursorMovementMode.Triangle:
                    var triangleModeSection = ConfigurationManager.GetSection("TriangleMode") as TriangleModeSection;

                    if (triangleModeSection != null)
                    {
                        config.intervalMs = triangleModeSection.IntervalMs;
                        config.step = triangleModeSection.Step;
                        config.point1 = triangleModeSection.Point1;
                        config.point2 = triangleModeSection.Point2;
                        config.point3 = triangleModeSection.Point3;
                    }
                    break;
                case CursorMovementMode.Quadrilateral:
                    var quadrilateralModeSection = ConfigurationManager.GetSection("QuadrilateralMode") as QuadrilateralModeSection;

                    if (quadrilateralModeSection != null)
                    {
                        config.intervalMs = quadrilateralModeSection.IntervalMs;
                        config.step = quadrilateralModeSection.Step;
                        config.point1 = quadrilateralModeSection.Point1;
                        config.point2 = quadrilateralModeSection.Point2;
                        config.point3 = quadrilateralModeSection.Point3;
                        config.point4 = quadrilateralModeSection.Point4;
                    }
                    break;
                case CursorMovementMode.Polygon:
                    var polygonModeSection = ConfigurationManager.GetSection("PolygonMode") as PolygonModeSection;

                    if (polygonModeSection != null)
                    {
                        config.intervalMs = polygonModeSection.IntervalMs;
                        config.step = polygonModeSection.Step;
                        config.angleCount = polygonModeSection.AngleCount;
                        config.radius = polygonModeSection.Radius;
                    }
                    break;
                case CursorMovementMode.RandomPoint:
                    var randomPointModeSection = ConfigurationManager.GetSection("RandomPointMode") as RandomPointModeSection;

                    if (randomPointModeSection != null)
                    {
                        config.intervalMs = randomPointModeSection.IntervalMs;
                        config.step = randomPointModeSection.Step;
                    }
                    break;
                default:
                    throw new Exception($"Для мода {config.mode} не реализован Section в app Config");
            }

            return config;

        }

    }

}
