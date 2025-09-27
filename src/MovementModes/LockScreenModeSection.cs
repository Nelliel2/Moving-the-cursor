using MovingCursor.Utilities;
using System.Configuration;

namespace MovingCursor.MovementModes
{
    internal class LockScreenModeSection : ConfigurationSection
    {
        [ConfigurationProperty(("intervalMs"), IsRequired = true)]
        public int IntervalMs
        {
            get { return Validator.ValidateInt(this["intervalMs"], value => value > 0, "Значение должно быть больше нуля", "intervalMs"); }
            set { this["intervalMs"] = value; }
        }

    }
}
