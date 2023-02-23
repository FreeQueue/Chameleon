using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Chameleon
{
    public class LevelRestartEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LevelRestartEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LevelRestartEventArgs Create(object userData = null)
        {
            LevelRestartEventArgs levelRestartEventArgs = ReferencePool.Acquire<LevelRestartEventArgs>();
            levelRestartEventArgs.UserData = userData;
            return levelRestartEventArgs;
        }

        public override void Clear()
        {
        }
    }

}

