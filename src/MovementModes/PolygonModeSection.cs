using MovingCursor.Utilities;
using System.Configuration;

namespace MovingCursor.MovementModes
{
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
}
