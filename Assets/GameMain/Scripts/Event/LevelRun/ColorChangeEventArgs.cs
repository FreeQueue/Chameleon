using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Chameleon
{
    public class ColorChangeEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ColorChangeEventArgs).GetHashCode();

        public ColorChangeEventArgs()
        {
            Color = EnumColor.Orange;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public EnumColor Color
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static ColorChangeEventArgs Create(EnumColor color, object userData = null)
        {
            ColorChangeEventArgs colorChangeEventArgs = ReferencePool.Acquire<ColorChangeEventArgs>();
            colorChangeEventArgs.Color = color;
            colorChangeEventArgs.UserData = userData;
            return colorChangeEventArgs;
        }

        public override void Clear()
        {
            Color = EnumColor.Orange;
        }
    }
}