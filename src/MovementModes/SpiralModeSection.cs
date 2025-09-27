using MovingCursor.Utilities;
using System.Configuration;

namespace MovingCursor.MovementModes
{
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
}
