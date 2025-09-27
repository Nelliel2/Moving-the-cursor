using MovingCursor.Utilities;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;

namespace MovingCursor.MovementModes
{
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
        [TypeConverter(typeof(Utilities.PointConverter))]
        public Point Point1
        {
            get { return (Point)this["point1"]; }
            set { this["point1"] = value; }
        }
        [ConfigurationProperty(("point2"), IsRequired = true)]
        [TypeConverter(typeof(Utilities.PointConverter))]
        public Point Point2
        {
            get { return (Point)this["point2"]; }
            set { this["point2"] = value; }
        }
        [ConfigurationProperty(("point3"), IsRequired = true)]
        [TypeConverter(typeof(Utilities.PointConverter))]
        public Point Point3
        {
            get { return (Point)this["point3"]; }
            set { this["point3"] = value; }
        }
    }
}
