using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Chameleon
{
    public class CollectEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CollectEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public EnumEntity Type
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static CollectEventArgs Create(EnumEntity enumEntity, object userData = null)
        {
            CollectEventArgs collectEventArgs = ReferencePool.Acquire<CollectEventArgs>();
            collectEventArgs.Type = enumEntity;
            collectEventArgs.UserData = userData;
            return collectEventArgs;
        }

        public override void Clear()
        {
        }
    }
}